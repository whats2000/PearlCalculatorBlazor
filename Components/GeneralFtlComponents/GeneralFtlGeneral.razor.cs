using System;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;

namespace PearlCalculatorBlazor.Components.GeneralFtlComponents;

public partial class GeneralFtlGeneral
{
    private const string PublishKey = "GeneralFtlGeneral";

    private static readonly SettingsJsonConverter JsonConverter = new();

    private static JsonSerializerOptions _writeSerializerOptions = new()
    {
        Converters = { JsonConverter },
        WriteIndented = true
    };

    private static JsonSerializerOptions _readSerializerOptions = new()
    {
        Converters = { JsonConverter }
    };

    private double PearlX
    {
        get => Data.Pearl.Position.X;
        set => Data.Pearl.Position.X = value;
    }

    private double PearlZ
    {
        get => Data.Pearl.Position.Z;
        set => Data.Pearl.Position.Z = value;
    }

    private double DestinationX
    {
        get => Data.Destination.X;
        set
        {
            var destination = Data.Destination;
            destination.X = value;
            Data.Destination = destination;
        }
    }

    private double DestinationZ
    {
        get => Data.Destination.Z;
        set
        {
            var destination = Data.Destination;
            destination.Z = value;
            Data.Destination = destination;
        }
    }

    private int MaxTnt
    {
        get => Data.MaxTNT;
        set => Data.MaxTNT = value;
    }

    private Direction Direction
    {
        get => Data.Direction;
        set => Data.Direction = value;
    }

    private Direction DefaultRedTntDirection
    {
        get => Data.DefaultRedDuper;
        set => Data.DefaultRedDuper = value;
    }

    private Direction DefaultBlueTntDirection
    {
        get => Data.DefaultBlueDuper;
        set => Data.DefaultBlueDuper = value;
    }

    private uint RedTnt
    {
        get => (uint)Data.RedTNT;
        set => Data.RedTNT = (int)value;
    }

    private uint BlueTnt
    {
        get => (uint)Data.BlueTNT;
        set => Data.BlueTNT = (int)value;
    }

    private async Task NoticeWithIcon(NotificationType type)
    {
        await Notice.Open(new NotificationConfig
        {
            Message = "Notification",
            Description = "The current input value is not calculable",
            NotificationType = type
        });
    }

    protected override void OnInitialized()
    {
        EventManager.Instance.AddListener<SetRtCountArgs>("tntAmountSetRTCount", (_, args) =>
        {
            Direction = DirectionUtils.GetDirection(Data.Pearl.Position.WorldAngle(Data.Destination));
            RedTnt = (uint)args.Red;
            BlueTnt = (uint)args.Blue;
            StateHasChanged();
        });

        TranslateText.OnLanguageChange += RefreshPage;
    }

    private async void CalculateTntAmount()
    {
        MessageConfig mc = new()
        {
            Content = "calculating...",
            Duration = 0
        };

        var loading = AntMessage.Loading(mc);
        await Task.Delay(200);

        var isSu = false;

        await Task.Run(() =>
        {
            if (Calculation.CalculateTNTAmount(100, 10))
            {
                Data.TNTResult.SortByWeightedDistance(new TNTResultSortByWeightedArgs(Data.TNTWeight,
                    Data.MaxCalculateTNT, Data.MaxCalculateDistance));
                isSu = true;
            }
        });

        if (isSu)
            EventManager.Instance.PublishEvent(this, "calculate", new ButtonClickArgs(PublishKey));

        loading.Start();

        if (!isSu)
            await NoticeWithIcon(NotificationType.Error);
    }

    private void PearlSimulate()
    {
        var pearlTraceList = Calculation.CalculatePearlTrace((int)RedTnt, (int)BlueTnt, 100, Direction);
        EventManager.Instance.PublishEvent(this, "simulate",
            new PearlSimulateArgs(PublishKey, Data.Pearl, pearlTraceList));
    }

    private async void ImportSettings_OnClick()
    {
        await JsRuntime.InvokeVoidAsync("ResetStateInJs");
        await JsRuntime.InvokeAsync<string>("ImportSettings");
    }

    private async void ExportSettings_OnClick()
    {
        var json = JsonSerializer.Serialize(Settings.CreateSettingsFormData(), _writeSerializerOptions);
        await JsRuntime.InvokeAsync<string>("ExportSettings", json);
    }

    private async void ImportHandleFileSelected(InputFileChangeEventArgs e)
    {
        await using var sr = e.File.OpenReadStream();
        var buffer = new byte[e.File.Size];
        var readAsync = await sr.ReadAsync(buffer.AsMemory(0, buffer.Length));

        if (readAsync == 0)
        {
            await AntMessage.Error("File is empty");
            return;
        }

        LoadJson(buffer.AsSpan(0, buffer.Length));
    }

    private void LoadJson(ReadOnlySpan<byte> reader)
    {
        try
        {
            var settings = JsonSerializer.Deserialize<Settings>(reader, _readSerializerOptions);

            Data.NorthWestTNT = settings.NorthWestTNT;
            Data.NorthEastTNT = settings.NorthEastTNT;
            Data.SouthWestTNT = settings.SouthWestTNT;
            Data.SouthEastTNT = settings.SouthEastTNT;

            Data.Pearl = settings.Pearl;

            Data.RedTNT = settings.RedTNT;
            Data.BlueTNT = settings.BlueTNT;
            Data.MaxTNT = settings.MaxTNT;

            Data.Destination = settings.Destination;
            Data.PearlOffset = settings.Offset;

            Data.Direction = settings.Direction;

            Data.DefaultRedDuper = settings.DefaultRedTNTDirection;
            Data.DefaultBlueDuper = settings.DefaultBlueTNTDirection;

            Data.PearlYMotionCancellation = settings.PearlYMotionCancellation;
            Data.PearlYPositionOriginal = settings.PearlYPositionOriginal;
            Data.PearlYPositionAdjusted = settings.PearlYPositionAdjusted;

            PearlSimulate();
        }
        catch (Exception e)
        {
            AntMessage.Error(e.GetType().ToString());
            AntMessage.Error(e.Message);
        }

        StateHasChanged();
    }

    private void RefreshPage()
    {
        StateHasChanged();
    }
}