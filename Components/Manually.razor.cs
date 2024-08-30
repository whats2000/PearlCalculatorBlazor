using PearlCalculatorLib.Manually;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.Result;
using AntDesign;
using System.Threading.Tasks;
using System.Collections.Generic;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorBlazor.Components
{
    public partial class Manually
    {
        const string PublishKey = "Manually";

        bool _valueHasChanged = true;

        private List<Entity> _calculateResult;

        private ManuallyData _manuallyData;

        protected override void OnInitialized()
        {
            _manuallyData = new ManuallyData(0, 0, new Space3D(), new Space3D(), new Surface2D(), new PearlEntity());

            EventManager.Instance.AddListener<SetRTCountArgs>("tntAmountSetRTCount", (sender, args) =>
            {
                _manuallyData.ATNTAmount = args.Red;
                _manuallyData.BTNTAmount = args.Blue;

                StateHasChanged();
            });

            TranslateText.OnLanguageChange += RefreshPage;
        }

        private double ManuallyPearlPosX
        {
            get => _manuallyData.Pearl.Position.X;
            set
            {
                _manuallyData.Pearl.Position.X = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlMomentumX
        {
            get => _manuallyData.Pearl.Motion.X;
            set
            {
                _manuallyData.Pearl.Motion.X = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlPosY
        {
            get => _manuallyData.Pearl.Position.Y;
            set
            {
                _manuallyData.Pearl.Position.Y = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlMomentumY
        {
            get => _manuallyData.Pearl.Motion.Y;
            set
            {
                _manuallyData.Pearl.Motion.Y = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlPosZ
        {
            get => _manuallyData.Pearl.Position.Z;
            set
            {
                _manuallyData.Pearl.Position.Z = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlMomentumZ
        {
            get => _manuallyData.Pearl.Motion.Z;
            set
            {
                _manuallyData.Pearl.Motion.Z = value;
                _valueHasChanged = true;
            }
        }

        private double ATNTX
        {
            get => _manuallyData.ATNT.X;
            set
            {
                _manuallyData.ATNT.X = value;
                _valueHasChanged = true;
            }
        }

        private double BTNTX
        {
            get => _manuallyData.BTNT.X;
            set
            {
                _manuallyData.BTNT.X = value;
                _valueHasChanged = true;
            }
        }

        private double ATNTY
        {
            get => _manuallyData.ATNT.Y;
            set
            {
                _manuallyData.ATNT.Y = value;
                _valueHasChanged = true;
            }
        }

        private double BTNTY
        {
            get => _manuallyData.BTNT.Y;
            set
            {
                _manuallyData.BTNT.Y = value;
                _valueHasChanged = true;
            }
        }

        private double ATNTZ
        {
            get => _manuallyData.ATNT.Z;
            set
            {
                _manuallyData.ATNT.Z = value;
                _valueHasChanged = true;
            }
        }

        private double BTNTZ
        {
            get => _manuallyData.BTNT.Z;
            set
            {
                _manuallyData.BTNT.Z = value;
                _valueHasChanged = true;
            }
        }

        private int ATNTAmount
        {
            get => _manuallyData.ATNTAmount;
            set
            {
                _manuallyData.ATNTAmount = value;
                _valueHasChanged = true;
            }
        }

        private int BTNTAmount
        {
            get => _manuallyData.BTNTAmount;
            set
            {
                _manuallyData.BTNTAmount = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyDestinationX
        {
            get => _manuallyData.Destination.X;
            set
            {
                _manuallyData.Destination.X = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyDestinationZ
        {
            get => _manuallyData.Destination.Z;
            set
            {
                _manuallyData.Destination.Z = value;
                _valueHasChanged = true;
            }
        }

        private async Task NoticeWithIcon(NotificationType type)
        {
            await Notice.Open(new NotificationConfig()
            {
                Message = "Notification",
                Description = "The current input value is not calculable",
                NotificationType = type
            });
        }

        private async void ManuallyCalculateTNTAmount()
        {
            MessageConfig mc = new()
            {
                Content = "calculating...",
                Duration = 0
            };

            var loading = AntMessage.Loading(mc);
            await Task.Delay(200);

            List<TNTCalculationResult> results = null;

            var isSu = await Task.Run(() => Calculation.CalculateTNTAmount(_manuallyData, 100, 10, out results));

            if (isSu)
                EventManager.Instance.PublishEvent(this, "calculate", new CalculateTNTAmuontArgs(PublishKey, _manuallyData, results));

            loading.Start();

            if (!isSu)
                await NoticeWithIcon(NotificationType.Error);

        }

        private void ManuallyCalculatePearl(string key)
        {
            if (_valueHasChanged)
                _calculateResult = Calculation.CalculatePearlTrace(_manuallyData, 100);
            _valueHasChanged = false;

            EventManager.Instance.PublishEvent(this, key, new PearlSimulateArgs(PublishKey, _manuallyData.Pearl, _calculateResult));
        }

        public void RefreshPage()
        {
            StateHasChanged();
        }
    }
}
