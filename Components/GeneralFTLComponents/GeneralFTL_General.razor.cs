using AntDesign;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_General
    {
        const string PublishKey = "GFTL_General";

        private static readonly SettingsJsonConverter JsonConverter = new SettingsJsonConverter();

        private static JsonSerializerOptions WriteSerializerOptions = new JsonSerializerOptions
        {
            Converters = { JsonConverter },
            WriteIndented = true
        };

        private static JsonSerializerOptions ReadSerializerOptions = new JsonSerializerOptions
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
            set => Data.Destination.X = value;
        }

        private double DestinationZ
        {
            get => Data.Destination.Z;
            set => Data.Destination.Z = value;
        }

        private int MaxTNT
        {
            get => Data.MaxTNT;
            set => Data.MaxTNT = value;
        }

        private Direction Direction
        {
            get => Data.Direction;
            set => Data.Direction = value;
        }

        private uint RedTNT
        {
            get => (uint)Data.RedTNT;
            set => Data.RedTNT = (int)value;
        }

        private uint BlueTNT
        {
            get => (uint)Data.BlueTNT;
            set => Data.BlueTNT = (int)value;
        }

        private async Task NoticeWithIcon(NotificationType type)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = "Notification",
                Description = "The current input value is not calculable",
                NotificationType = type
            });
        }

        protected override void OnInitialized()
        {
            EventManager.Instance.AddListener<SetRTCountArgs>("tntAmountSetRTCount", (sender, args) =>
            {
                Direction = Data.Pearl.Position.Direction(Data.Pearl.Position.WorldAngle(Data.Destination));
                RedTNT = (uint)args.Red;
                BlueTNT = (uint)args.Blue;
                StateHasChanged();
            });
        }

        private async void CalculateTNTAmount()
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
                    Data.TNTResult.SortByWeightedDistance(new(Data.TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
                    isSu = true;
                }
            });

            if (isSu)
                EventManager.Instance.PublishEvent(this, "calculate", new ButtonClickArgs(PublishKey));
            else
                await NoticeWithIcon(NotificationType.Error);

            loading.Start();
        }

        private void PearlSimulate()
        {
            var pearlTraceList = Calculation.CalculatePearlTrace((int)RedTNT, (int)BlueTNT, 100, Direction);
            EventManager.Instance.PublishEvent(this, "simulate", new PearlSimulateArgs(PublishKey, pearlTraceList));
        }

        private async void ImportSettings_OnClick()
        {
            await JSRuntime.InvokeAsync<string>("ImportSettings");
        }

        private async void ExportSettings_OnClick()
        {
            var json = JsonSerializer.Serialize(Settings.CreateSettingsFormData(), WriteSerializerOptions);
            await JSRuntime.InvokeAsync<string>("ExportSettings", json);
        }

        private async void ImportHandleFileSelected(InputFileChangeEventArgs e)
        {
            using var sr = e.File.OpenReadStream();
            var buffer = new byte[e.File.Size];
            await sr.ReadAsync(buffer.AsMemory(0, buffer.Length));

            LoadJson(buffer.AsSpan(0, buffer.Length));
        }

        private void LoadJson(ReadOnlySpan<byte> reader)
        {
            try
            {
                var settings = JsonSerializer.Deserialize<Settings>(reader, ReadSerializerOptions);

                Data.Pearl = settings.Pearl;
                Data.PearlOffset = settings.Offset;
                Data.Destination = settings.Destination;

                Data.NorthWestTNT = settings.NorthWestTNT;
                Data.NorthEastTNT = settings.NorthEastTNT;
                Data.SouthWestTNT = settings.SouthWestTNT;
                Data.SouthEastTNT = settings.SouthEastTNT;

                Data.RedTNT = settings.RedTNT;
                Data.BlueTNT = settings.BlueTNT;
                Data.MaxTNT = settings.MaxTNT;
                Data.Direction = settings.Direction;
            }
            catch (Exception e)
            {
                AntMessage.Error(e.GetType().ToString());
                AntMessage.Error(e.Message);
            }

            StateHasChanged();
        }
    }
}
