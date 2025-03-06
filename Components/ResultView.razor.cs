using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.Charts;
using AntDesign.TableModels;
using Microsoft.JSInterop;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;
using Tooltip = AntDesign.Charts.Tooltip;

namespace PearlCalculatorBlazor.Components;

public partial class ResultView
{
    private static string _resultDirection = string.Empty;
    private static string _resultAngle = string.Empty;

    private readonly ColumnConfig _columnConfig = new()
    {
        IsGroup = true,
        XField = "index",
        YField = "value",
        SeriesField = "type",
        Tooltip = new Tooltip
        {
            Fields = new[] { "index", "value", "type" }
        },
        Color = new[] { "#FF7260", "#9BD7D5" }
    };

    private readonly LineConfig _lineConfig = new()
    {
        Padding = "auto",
        AutoFit = true,
        XField = "tick",
        YField = "value",
        SeriesField = "axis"
    };

    private readonly RadarConfig _radarConfig = new()
    {
        XField = "angle",
        YField = "value",
        Tooltip = new Tooltip
        {
            Fields = new[] { "angle", "currentAngle" },
            Shared = true
        }
    };

    private readonly ScatterConfig _scatterConfig = new()
    {
        XField = "xCoor",
        YField = "zCoor",
        ColorField = "tick",
        SizeField = "yCoor",
        Color = new[] { "#0000FF", "#ADD8E6" },
        RegressionLine = new RegressionLineConfig
        {
            Type = "linear"
        },
        Tooltip = new Tooltip
        {
            Fields = new[] { "tick", "xCoor", "yCoor", "zCoor" }
        },
        PointStyle = new GraphicStyle
        {
            LineWidth = 2
        }
    };

    private DualAxesConfig _dualAxesConfig = new()
    {
        XField = "index",
        YField = new[] { "value", "count" },
        GeometryOptions = new object[]
        {
            new
            {
                Geometry = "column",
                IsStack = true,
                seriesField = "type",
                Color = new[] { "#FF7260", "#9BD7D5" }
            },
            new
            {
                Geometry = "line",
                seriesField = "name",
                Color = new[] { "#129793", "#90AEC6" },
                LineStyle = new
                {
                    lineWidth = 1.5
                }
            }
        }
    };

    private bool _graphLoading = false;

    private int _pageIndex = 1;
    private int _pageSize = 50;

    private List<TntConfigurationResult> TntResults = new();

    private object[] PearlTraceData => GetEntityWrapperData(PearlTrace);
    private object[] PearlMotionData => GetEntityWrapperData(PearlMotion);

    private object[] AmountResultStackedBarData => GetAmountResultStackedBarData();
    private object[] AmountResultLineData => GetAmountResultLineData();

    private ShowResultMode ShowMode { get; set; } = ShowResultMode.Empty;

    private int AmountTotal => AmountResult?.Count ?? 0;
    private int PearlTotal => PearlTrace?.Count ?? 0;
    private int MotionTotal => PearlMotion?.Count ?? 0;

    private List<TNTCalculationResult> AmountResult { get; set; } = new();

    private List<EntityWrapper> PearlTrace { get; set; } = new();
    private List<EntityWrapper> PearlMotion { get; set; } = new();

    private object[] GetRadarChartData()
    {
        var radarData = new List<object>();
        var currentAngle = _resultAngle == string.Empty
            ? 0
            : (int)double.Parse(_resultAngle, CultureInfo.InvariantCulture);

        var closestAngle = (int)(Math.Round(currentAngle / 10.0) * 10);

        radarData.AddRange(Enumerable.Range(0, 36).Select(i => new
        {
            angle = GetDirectionLabel(i * 10 - 180),
            value = _resultAngle != string.Empty
                ? i * 10 - 180 == closestAngle ? 1 : 0
                : 0,
            currentAngle = _resultAngle
        }));

        return radarData.ToArray();
    }

    private string GetDirectionLabel(int angle)
    {
        return angle switch
        {
            0 => TranslateText.GetTranslateText("DirectionSouth"),
            90 => TranslateText.GetTranslateText("DirectionWest"),
            180 or -180 => TranslateText.GetTranslateText("DirectionNorth"),
            -90 => TranslateText.GetTranslateText("DirectionEast"),
            _ => $"{angle}°"
        };
    }

    private List<EntityWrapper> FilterEntityWrappers(List<EntityWrapper> data)
    {
        return data.Skip((_pageIndex - 1) * _pageSize).Take(_pageSize).ToList();
    }

    private object[] GetEntityWrapperData(List<EntityWrapper> data)
    {
        var selectedData = FilterEntityWrappers(data);
        return selectedData.Select(r => new
            {
                tick = r.Tick,
                value = r.XCoor,
                axis = "X"
            })
            .Concat(selectedData.Select(r => new
            {
                tick = r.Tick,
                value = r.YCoor,
                axis = "Y"
            }))
            .Concat(selectedData.Select(r => new
            {
                tick = r.Tick,
                value = r.ZCoor,
                axis = "Z"
            })).ToArray<object>();
    }

    private object[] GetAmountResultStackedBarData()
    {
        return AmountResult
            .Select((r, index) => new
            {
                index,
                value = r.Red,
                type = TranslateText.GetTranslateText("DisplayRed")
            })
            .Concat(AmountResult.Select((r, index) => new
            {
                index,
                value = r.Blue,
                type = TranslateText.GetTranslateText("DisplayBlue")
            }))
            .ToArray<object>();
    }

    private object[] GetAmountResultLineData()
    {
        return AmountResult.Select((r, index) => new
            {
                index,
                count = r.Distance,
                name = TranslateText.GetTranslateText("DisplayDistance")
            })
            .Concat(AmountResult.Select((r, index) => new
            {
                index,
                count = (double)r.Tick,
                name = TranslateText.GetTranslateText("DisplayTicks")
            }))
            .ToArray<object>();
    }

    private object[] GetTntEncodingData()
    {
        // Group the TNT results by the TNT value.
        var groupedResults = TntResults.GroupBy(r => r.TntValue);

        // For each unique TNT value, create two entries: one for red and one for blue.
        // The value is the total count of active occurrences within that group.
        var data = groupedResults.SelectMany(g => new[]
        {
            new
            {
                index = g.Key.ToString(),
                type = TranslateText.GetTranslateText("DisplayRed"),
                value = g.Count(r => r.RedIsUsed)
            },
            new
            {
                index = g.Key.ToString(),
                type = TranslateText.GetTranslateText("DisplayBlue"),
                value = g.Count(r => r.BlueIsUsed)
            }
        }).ToArray();

        return data;
    }

    private async void RefreshGraph()
    {
        _graphLoading = true;
        StateHasChanged();
        await Task.Delay(50);
        _graphLoading = false;
        StateHasChanged();
    }

    private static void ShowDirectionResult(Space3D pearlPos, Space3D destination)
    {
        var angle = pearlPos.WorldAngle(destination);

        if (Math.Abs(Math.Abs(angle) - 370) < 0.01)
        {
            _resultDirection = string.Empty;
            _resultAngle = string.Empty;
            return;
        }

        _resultDirection = DirectionUtils.GetDirection(angle).ToString();

        _resultAngle = angle.ToString(CultureInfo.InvariantCulture);
    }

    private async Task NoticeWithIcon(NotificationType type, string msg)
    {
        await Notice.Open(new NotificationConfig
        {
            Message = "Notification",
            Description = msg,
            NotificationType = type
        });
    }

    protected override void OnInitialized()
    {
        EventManager.Instance.AddListener<BaseEventArgs>("calculate", (_, _) =>
        {
            _pageIndex = 1;

            AmountResult = Data.TNTResult;

            ShowMode = ShowResultMode.Amount;

            ShowDirectionResult(Data.Pearl.Position, Data.Destination);

            RefreshPage();
        });

        EventManager.Instance.AddListener<PearlSimulateArgs>("simulate", async (_, args) =>
        {
            _pageIndex = 1;

            PearlTrace = await Task.FromResult(Enumerable.Range(0, args.Trace.Count).Select(index =>
            {
                ref var p = ref args.Trace[index].Position;
                return new EntityWrapper
                {
                    XCoor = p.X,
                    YCoor = p.Y,
                    ZCoor = p.Z,
                    Tick = index
                };
            }).ToList());

            ShowDirectionResult(args.Pearl.Position,
                new Space3D(PearlTrace[1].XCoor, PearlTrace[1].YCoor, PearlTrace[1].ZCoor));

            ShowMode = ShowResultMode.Trace;

            RefreshPage();
        });

        EventManager.Instance.AddListener<BaseEventArgs>("sortByWeightedDistance", async (_, _) =>
        {
            if (ShowMode != ShowResultMode.Amount || AmountResult is null) return;

            try
            {
                AmountResult.SortByWeightedDistance(new TNTResultSortByWeightedArgs(Data.TNTWeight,
                    Data.MaxCalculateTNT, Data.MaxCalculateDistance));
            }
            catch (Exception e)
            {
                await NoticeWithIcon(NotificationType.Error, e.ToString());
            }

            RefreshPage();
        });

        EventManager.Instance.AddListener<BaseEventArgs>("sortByWeightedTotal", async (_, _) =>
        {
            if (ShowMode != ShowResultMode.Amount || AmountResult is null) return;

            try
            {
                AmountResult.SortByWeightedTotal(new TNTResultSortByWeightedArgs(Data.TNTWeight, Data.MaxCalculateTNT,
                    Data.MaxCalculateDistance));
            }
            catch (Exception e)
            {
                await NoticeWithIcon(NotificationType.Error, e.ToString());
            }

            RefreshPage();
        });

        EventManager.Instance.AddListener<CalculateTntAmountArgs>("calculate", (_, args) =>
        {
            _pageIndex = 1;

            AmountResult = args.Results;
            ShowMode = ShowResultMode.Amount;

            ShowDirectionResult(args.ManuallyData.Pearl.Position, args.ManuallyData.Destination.ToSpace3D());

            RefreshPage();
        });

        EventManager.Instance.AddListener<PearlSimulateManuallyArgs>("manuallyPearlTrace", async (_, args) =>
        {
            _pageIndex = 1;

            PearlTrace = await Task.FromResult(Enumerable.Range(0, args.Trace.Count).Select(index =>
            {
                ref var p = ref args.Trace[index].Position;
                return new EntityWrapper
                {
                    XCoor = p.X,
                    YCoor = p.Y,
                    ZCoor = p.Z,
                    Tick = index
                };
            }).ToList());

            ShowDirectionResult(args.ManuallyData.Pearl.Position,
                new Space3D(PearlTrace[1].XCoor, PearlTrace[1].YCoor, PearlTrace[1].ZCoor));

            ShowMode = ShowResultMode.Trace;

            RefreshPage();
        });

        EventManager.Instance.AddListener<PearlSimulateManuallyArgs>("manuallyPearlMotion", async (_, args) =>
        {
            _pageIndex = 1;

            PearlMotion = await Task.FromResult(Enumerable.Range(0, args.Trace.Count).Select(index =>
            {
                ref var p = ref args.Trace[index].Motion;
                return new EntityWrapper
                {
                    XCoor = p.X,
                    YCoor = p.Y,
                    ZCoor = p.Z,
                    Tick = index
                };
            }).ToList());

            var firstTickPos = args.ManuallyData.Pearl.Position + args.Trace[0].Motion;

            ShowDirectionResult(args.ManuallyData.Pearl.Position,
                new Space3D(firstTickPos.X, firstTickPos.Y, firstTickPos.Z));

            ShowMode = ShowResultMode.Momentum;

            RefreshPage();
        });

        EventManager.Instance.AddListener<BaseEventArgs>("calculateTntEncoding", async (_, _) =>
        {
            var success = Calculation.CalculateTNTConfiguration(Data.RedTNT, Data.BlueTNT, out var redTntEncoding, out var blueTntEncoding);

            if (!success)
            {
                await NoticeWithIcon(NotificationType.Error,
                    TranslateText.GetTranslateText("TNTCalculationFailedMessage"));
                return;
            }

            ShowMode = ShowResultMode.TntEncoding;

            TntResults.Clear();

            foreach (int n in Data.RedTNTConfiguration) {
                bool red = false, blue = false;
                if (redTntEncoding.Contains(n)) {
                    red = true;
                    redTntEncoding.Remove(n);
                }
                if (blueTntEncoding.Contains(n)) {
                    blue = true;
                    blueTntEncoding.Remove(n);
                }

                TntResults.Add(new TntConfigurationResult {
                    TntValue = n, RedIsUsed = red, BlueIsUsed = blue
                });
            }

            foreach (int n in Data.BlueTNTConfiguration.Where(n => !Data.RedTNTConfiguration.Contains(n))) {
                bool blue = false;
                if (blueTntEncoding.Contains(n)) {
                    blue = true;
                    blueTntEncoding.Remove(n);
                }

                TntResults.Add(new TntConfigurationResult {
                    TntValue = n, RedIsUsed = false, BlueIsUsed = blue
                });
            }

            RefreshPage();
        });

        TranslateText.OnLanguageChange += RefreshPage;
    }

    private async Task OnAmountRowClickAsync(RowData<TNTCalculationResult> res)
    {
        EventManager.Instance.PublishEvent(this, "tntAmountSetRTCount",
            new SetRtCountArgs("ResultView", res.Data.Red, res.Data.Blue));
        await NoticeWithIcon(NotificationType.Success,
            TranslateText.GetTranslateText("TNTCalculationSetSuccessMessage"));
    }

    private async Task OnTraceRowClickAsync(RowData<EntityWrapper> res)
    {
        await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText",
            $"/tp {res.Data.XCoor} {res.Data.YCoor} {res.Data.ZCoor}");
        await NoticeWithIcon(
            NotificationType.Success
            , TranslateText.GetTranslateText("CoordinateCopiedMessage"));
    }

    private void RefreshPage()
    {
        RefreshGraph();
    }

    private enum ShowResultMode
    {
        Empty,
        Amount,
        Trace,
        Momentum,
        TntEncoding
    }
}

public class EntityWrapper
{
    public double XCoor { get; set; }
    public double YCoor { get; set; }
    public double ZCoor { get; set; }
    public int Tick { get; set; }
}

public class TntConfigurationResult
{
    public int TntValue { get; set; }
    public bool RedIsUsed { get; set; }
    public bool BlueIsUsed { get; set; }
}