using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.Charts;
using AntDesign.TableModels;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;

namespace PearlCalculatorBlazor.Components;

public partial class ResultView
{
    private static string _resultDirection = string.Empty;
    private static string _resultAngle = string.Empty;

    private readonly LineConfig _lineConfig = new()
    {
        Padding = "auto",
        AutoFit = true,
        XField = "tick",
        YField = "value",
        SeriesField = "axis"
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
                // Red, Blue
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

    private int _pageIndex = 1;
    private int _pageSize = 50;

    private object[] PearlTraceData => GetEntityWrapperData(PearlTrace);
    private object[] PearlMotionData => GetEntityWrapperData(PearlMotion);

    private object[] AmountResultStackedBarData => GetAmountResultStackedBarData();
    private object[] AmountResultLineData => GetAmountResultLineData();

    private ShowResultMode ShowMode { get; set; } = ShowResultMode.Amount;

    private int AmountTotal => AmountResult?.Count ?? 0;
    private int PearlTotal => PearlTrace?.Count ?? 0;
    private int MotionTotal => PearlMotion?.Count ?? 0;

    private List<TNTCalculationResult> AmountResult { get; set; } = new();

    private List<EntityWrapper> PearlTrace { get; set; } = new();
    private List<EntityWrapper> PearlMotion { get; set; } = new();

    private object[] GetEntityWrapperData(List<EntityWrapper> data)
    {
        var selectedData = data.Skip((_pageIndex - 1) * _pageSize).Take(_pageSize).ToList();
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


    private static void ShowDirectionResult(Space3D pearlPos, Space3D destination)
    {
        var angle = pearlPos.WorldAngle(destination);

        if (Math.Abs(Math.Abs(angle) - 370) < 0.01)
            return;

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
        EventManager.Instance.AddListener<ButtonClickArgs>("calculate", (_, _) =>
        {
            _pageIndex = 1;

            AmountResult = Data.TNTResult;

            ShowMode = ShowResultMode.Amount;

            ShowDirectionResult(Data.Pearl.Position, Data.Destination);

            StateHasChanged();
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

            StateHasChanged();
        });

        EventManager.Instance.AddListener<ButtonClickArgs>("sortByWeightedDistance", async (_, _) =>
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

            StateHasChanged();
        });

        EventManager.Instance.AddListener<ButtonClickArgs>("sortByWeightedTotal", async (_, _) =>
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

            StateHasChanged();
        });

        EventManager.Instance.AddListener<CalculateTntAmountArgs>("calculate", (_, args) =>
        {
            _pageIndex = 1;

            AmountResult = args.Results;
            ShowMode = ShowResultMode.Amount;

            ShowDirectionResult(args.ManuallyData.Pearl.Position, args.ManuallyData.Destination.ToSpace3D());

            StateHasChanged();
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

            StateHasChanged();
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

            StateHasChanged();
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

    private void RefreshPage()
    {
        StateHasChanged();
    }

    private enum ShowResultMode
    {
        Amount,
        Trace,
        Momentum
    }
}

public class EntityWrapper
{
    public double XCoor { get; set; }
    public double YCoor { get; set; }
    public double ZCoor { get; set; }
    public int Tick { get; set; }
}