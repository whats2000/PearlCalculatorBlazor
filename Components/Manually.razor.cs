using PearlCalculatorLib.Manually;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.Result;
using AntDesign;
using System.Threading.Tasks;
using System.Collections.Generic;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorBlazor.Components
{
    public partial class Manually
    {
        const string PublishKey = "Manually";
        bool _valueHasChanged = true;
        List<Entity> _calculateResult;

        private double ManuallyPearlPosX
        {
            get => Data.Pearl.Position.X;
            set
            {
                Data.Pearl.Position.X = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlMomentumX
        {
            get => Data.Pearl.Position.X;
            set
            {
                Data.Pearl.Position.X = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlPosY
        {
            get => Data.Pearl.Position.Y;
            set
            {
                Data.Pearl.Position.Y = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlMomentumY
        {
            get => Data.Pearl.Position.Y;
            set
            {
                Data.Pearl.Position.Y = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlPosZ
        {
            get => Data.Pearl.Position.Z;
            set
            {
                Data.Pearl.Position.Z = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyPearlMomentumZ
        {
            get => Data.Pearl.Position.Z;
            set
            {
                Data.Pearl.Position.Z = value;
                _valueHasChanged = true;
            }
        }

        private double ATNTX
        {
            get => Data.ATNT.X;
            set
            {
                Data.ATNT.X = value;
                _valueHasChanged = true;
            }
        }

        private double BTNTX
        {
            get => Data.BTNT.X;
            set
            {
                Data.BTNT.X = value;
                _valueHasChanged = true;
            }
        }

        private double ATNTY
        {
            get => Data.ATNT.Y;
            set
            {
                Data.ATNT.Y = value;
                _valueHasChanged = true;
            }
        }

        private double BTNTY
        {
            get => Data.BTNT.Y;
            set
            {
                Data.BTNT.Y = value;
                _valueHasChanged = true;
            }
        }

        private double ATNTZ
        {
            get => Data.ATNT.Z;
            set
            {
                Data.ATNT.Z = value;
                _valueHasChanged = true;
            }
        }

        private double BTNTZ
        {
            get => Data.BTNT.Z;
            set
            {
                Data.BTNT.Z = value;
                _valueHasChanged = true;
            }
        }

        private int ATNTAmount
        {
            get => Data.ATNTAmount;
            set
            {
                Data.ATNTAmount = value;
                _valueHasChanged = true;
            }
        }

        private int BTNTAmount
        {
            get => Data.BTNTAmount;
            set
            {
                Data.BTNTAmount = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyDestinationX
        {
            get => Data.Destination.X;
            set
            {
                Data.Destination.X = value;
                _valueHasChanged = true;
            }
        }

        private double ManuallyDestinationZ
        {
            get => Data.Destination.Z;
            set
            {
                Data.Destination.Z = value;
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

            var isSu = await Task.Run(() => Calculation.CalculateTNTAmount(Data.Destination, 100, out results));

            if (isSu)
                EventManager.Instance.PublishEvent(this, "calculate", new CalculateTNTAmuontArgs(PublishKey, results));
            
            loading.Start();
            
            if (!isSu)
                await NoticeWithIcon(NotificationType.Error);

        }

        private void ManuallyCalculatePearl(string key)
        {
            if (_valueHasChanged)
                _calculateResult = Calculation.CalculatePearl(Data.ATNTAmount, Data.BTNTAmount, 100);
            _valueHasChanged = false;

            EventManager.Instance.PublishEvent(this, key, new PearlSimulateArgs(PublishKey, _calculateResult));
        }
    }
}
