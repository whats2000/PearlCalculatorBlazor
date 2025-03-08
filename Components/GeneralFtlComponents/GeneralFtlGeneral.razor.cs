using System;
using System.Collections.Generic;
using System.Text;
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
using PearlCalculatorLib.Settings;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PearlCalculatorBlazor.Components.GeneralFtlComponents;

public partial class GeneralFtlGeneral
{
    private const string PublishKey = "GeneralFtlGeneral";
    private static readonly SettingsJsonConverter JsonConverter = new();

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
        TranslateText.OnLanguageChange += RefreshPage;

        EventManager.Instance.AddListener<SetRtCountArgs>("tntAmountSetRTCount", (_, args) =>
        {
            Direction = DirectionUtils.GetDirection(Data.Pearl.Position.WorldAngle(Data.Destination));
            RedTnt = (uint)args.Red;
            BlueTnt = (uint)args.Blue;
            StateHasChanged();
        });

        EventManager.Instance.AddListener<BaseEventArgs>("dataChanged", (_, _) => { RefreshPage(); });
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
            EventManager.Instance.PublishEvent(this, "calculate", new BaseEventArgs(PublishKey));

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

    private async void ImportSettingsOnClick()
    {
        await JsRuntime.InvokeVoidAsync("ResetStateInJs");
        await JsRuntime.InvokeAsync<string>("ImportSettings");
    }

    private async Task ExportSettingsOnClick()
    {
        // Sync the current cannon settings with the data before exporting
        SettingsManager.SelectedCannon.SyncWithData();
        var json = JsonUtility.Serialize(SettingsManager.CreateSettingsCollection());
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

    private void LoadJson(ReadOnlySpan<byte> jsonData)
    {
        try
        {
            var settingsCollection = JsonUtility.DeSerialize(Encoding.UTF8.GetString(jsonData));

            if (settingsCollection.CannonSettings is null)
            {
                var settings = JsonSerializer.Deserialize<Settings>(jsonData, _readSerializerOptions);

                Data.RedTNT = settings.RedTNT;
                Data.BlueTNT = settings.BlueTNT;
                Data.Direction = settings.Direction;
                Data.Destination = settings.Destination;

                var cannonSettings = new CannonSettings
                {
                    CannonName = "Default",
                    MaxTNT = settings.MaxTNT,
                    DefaultRedDirection = settings.DefaultRedTNTDirection,
                    DefaultBlueDirection = settings.DefaultBlueTNTDirection,
                    NorthWestTNT = settings.NorthWestTNT,
                    NorthEastTNT = settings.NorthEastTNT,
                    SouthWestTNT = settings.SouthWestTNT,
                    SouthEastTNT = settings.SouthEastTNT,
                    Offset = settings.Offset,
                    Pearl = settings.Pearl,
                    RedTNTConfiguration = new List<int>(),
                    BlueTNTConfiguration = new List<int>(),
                    PearlYMotionCancellation = settings.PearlYMotionCancellation,
                    PearlYPositionOriginal = settings.PearlYPositionOriginal,
                    PearlYPositionAdjusted = settings.PearlYPositionAdjusted,
                    GameVersion = settings.GameVersion
                };

                SettingsManager.SettingsList.Clear();
                SettingsManager.AddSettings(cannonSettings);
                SettingsManager.SelectCannon(0);
            }
            else
            {
                Data.RedTNT = settingsCollection.RedTNT;
                Data.BlueTNT = settingsCollection.BlueTNT;
                Data.TNTWeight = settingsCollection.TNTWeight;
                Data.Direction = settingsCollection.Direction;
                Data.Destination = settingsCollection.Destination.ToSpace3D();

                SettingsManager.SettingsList.Clear();
                foreach (var cannonSettings in settingsCollection.CannonSettings)
                    SettingsManager.AddSettings(cannonSettings);

                if (!string.IsNullOrEmpty(settingsCollection.SelectedCannon))
                {
                    var index = SettingsManager.SettingsList.FindIndex(x =>
                        x.CannonName == settingsCollection.SelectedCannon);
                    SettingsManager.SelectCannon(index);
                }
                else
                {
                    SettingsManager.SelectCannon(0);
                }
            }

            PearlSimulate();

            EventManager.Instance.PublishEvent(this, "importSettings", new BaseEventArgs(PublishKey));
        }
        catch (Exception e)
        {
            String errorMessage = "Error while importing settings: " + e.Message;
            AntMessage.Error(e.GetType().ToString());
            AntMessage.Error(errorMessage);
        }

        StateHasChanged();
    }

    private void RefreshPage()
    {
        StateHasChanged();
    }
}