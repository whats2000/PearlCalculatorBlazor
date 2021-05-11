using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_Settings
    {
        private double NorthWestTntX
        {
            get => Data.NorthWestTNT.X;
            set => Data.NorthWestTNT.X = value;
        }

        private double NorthWestTntY
        {
            get => Data.NorthWestTNT.Y;
            set => Data.NorthWestTNT.Y = value;
        }

        private double NorthWestTntZ
        {
            get => Data.NorthWestTNT.Z;
            set => Data.NorthWestTNT.Z = value;
        }

        private double NorthEastTntX
        {
            get => Data.NorthEastTNT.X;
            set => Data.NorthEastTNT.X = value;
        }

        private double NorthEastTntY
        {
            get => Data.NorthEastTNT.Y;
            set => Data.NorthEastTNT.Y = value;
        }

        private double NorthEastTntZ
        {
            get => Data.NorthEastTNT.Z;
            set => Data.NorthEastTNT.Z = value;
        }

        private double SouthWestTntX
        {
            get => Data.SouthWestTNT.X;
            set => Data.SouthWestTNT.X = value;
        }

        private double SouthWestTntY
        {
            get => Data.SouthWestTNT.Y;
            set => Data.SouthWestTNT.Y = value;
        }

        private double SouthWestTntZ
        {
            get => Data.SouthWestTNT.Z;
            set => Data.SouthWestTNT.Z = value;
        }

        private double SouthEastTntX
        {
            get => Data.SouthEastTNT.X;
            set => Data.SouthEastTNT.X = value;
        }

        private double SouthEastTntY
        {
            get => Data.SouthEastTNT.Y;
            set => Data.SouthEastTNT.Y = value;
        }

        private double SouthEastTntZ
        {
            get => Data.SouthEastTNT.Z;
            set => Data.SouthEastTNT.Z = value;
        }

        private double PearlYCoordinate
        {
            get => Data.Pearl.Position.Y;
            set => Data.Pearl.Position.Y = value;
        }

        private double PearlYMomentum
        {
            get => Data.Pearl.Motion.Y;
            set => Data.Pearl.Motion.Y = value;
        }

        private string DefaultRedTNTPosition
        {
            get => Data.DefaultRedDuper.ToString();
            set => Data.DefaultRedDuper = Enum.Parse<Direction>(value);
        }

        private string DefaultBlueTNTPosition
        {
            get => Data.DefaultBlueDuper.ToString();
            set => Data.DefaultBlueDuper = Enum.Parse<Direction>(value);
        }

        private void ResetToDefault_OnClick()
        {
            PearlCalculatorLib.General.Data.Reset();
        }
    }
}
