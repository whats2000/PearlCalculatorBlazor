using PearlCalculatorLib.Manually;

namespace PearlCalculatorBlazor.Components
{
    public partial class Manually
    {
        string PublishKey = "Manually";

        private double ManuallyPearlPosX
        {
            get => Data.Pearl.Position.X;
            set => Data.Pearl.Position.X = value;
        }

        private double ManuallyPearlMomentumX
        {
            get => Data.Pearl.Position.X;
            set => Data.Pearl.Position.X = value;
        }

        private double ManuallyPearlPosY
        {
            get => Data.Pearl.Position.Y;
            set => Data.Pearl.Position.Y = value;
        }

        private double ManuallyPearlMomentumY
        {
            get => Data.Pearl.Position.Y;
            set => Data.Pearl.Position.Y = value;
        }

        private double ManuallyPearlPosZ
        {
            get => Data.Pearl.Position.Z;
            set => Data.Pearl.Position.Z = value;
        }

        private double ManuallyPearlMomentumZ
        {
            get => Data.Pearl.Position.Z;
            set => Data.Pearl.Position.Z = value;
        }

        private double ATNTX
        {
            get => Data.ATNT.X;
            set => Data.ATNT.X = value;
        }

        private double BTNTX
        {
            get => Data.BTNT.X;
            set => Data.BTNT.X = value;
        }

        private double ATNTY
        {
            get => Data.ATNT.Y;
            set => Data.ATNT.Y = value;
        }

        private double BTNTY
        {
            get => Data.BTNT.Y;
            set => Data.BTNT.Y = value;
        }

        private double ATNTZ
        {
            get => Data.ATNT.Z;
            set => Data.ATNT.Z = value;
        }

        private double BTNTZ
        {
            get => Data.BTNT.Z;
            set => Data.BTNT.Z = value;
        }

        private int ATNTAmount
        {
            get => Data.ATNTAmount;
            set => Data.ATNTAmount = value;
        }

        private int BTNTAmount
        {
            get => Data.BTNTAmount;
            set => Data.BTNTAmount = value;
        }

        private double ManuallyDestinationX
        {
            get => Data.Destination.X;
            set => Data.Destination.X = value;
        }

        private double ManuallyDestinationZ
        {
            get => Data.Destination.Z;
            set => Data.Destination.Z = value;
        }

        private void ManuallyCalculateTNTAmount()
        { 
            
        }

        private void ManuallyCalculatePearlTrace()
        {

        }

        private void ManuallyCalculatePearlMomentum()
        {

        }
    }
}
