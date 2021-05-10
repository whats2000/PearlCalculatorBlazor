using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_Settings
    {
        private static double NorthWestTntX
        {
            get => Data.NorthWestTNT.X;
            set => Data.NorthWestTNT.X = value;
        }

        private static double NorthWestTntY
        {
            get => Data.NorthWestTNT.Y;
            set => Data.NorthWestTNT.Y = value;
        }

        private static double NorthWestTntZ
        {
            get => Data.NorthWestTNT.Z;
            set => Data.NorthWestTNT.Z = value;
        }

        private static double NorthEastTntX
        {
            get => Data.NorthEastTNT.X;
            set => Data.NorthEastTNT.X = value;
        }

        private static double NorthEastTntY
        {
            get => Data.NorthEastTNT.Y;
            set => Data.NorthEastTNT.Y = value;
        }

        private static double NorthEastTntZ
        {
            get => Data.NorthEastTNT.Z;
            set => Data.NorthEastTNT.Z = value;
        }

        private static double SouthWestTntX
        {
            get => Data.SouthWestTNT.X;
            set => Data.SouthWestTNT.X = value;
        }

        private static double SouthWestTntY
        {
            get => Data.SouthWestTNT.Y;
            set => Data.SouthWestTNT.Y = value;
        }

        private static double SouthWestTntZ
        {
            get => Data.SouthWestTNT.Z;
            set => Data.SouthWestTNT.Z = value;
        }

        private static double SouthEastTntX
        {
            get => Data.SouthEastTNT.X;
            set => Data.SouthEastTNT.X = value;
        }

        private static double SouthEastTntY
        {
            get => Data.SouthEastTNT.Y;
            set => Data.SouthEastTNT.Y = value;
        }

        private static double SouthEastTntZ
        {
            get => Data.SouthEastTNT.Z;
            set => Data.SouthEastTNT.Z = value;
        }

        private static double PearlYCoordinate
        {
            get => Data.Pearl.Position.Y;
            set => Data.Pearl.Position.Y = value;
        }

        private static double PearlYMomentum
        {
            get => Data.Pearl.Motion.Y;
            set => Data.Pearl.Motion.Y = value;
        }

        private static string DefaultRedTNTPosition
        {
            get => Data.DefaultRedDuper.ToString();
            set => Data.DefaultRedDuper = SetDefaultPosition(value);
        }

        private static string DefaultBlueTNTPosition
        {
            get => Data.DefaultBlueDuper.ToString();
            set => Data.DefaultBlueDuper = SetDefaultPosition(value);
        }

        private static Direction SetDefaultPosition(string value)
        {
            return value switch
            {
                "NorthWest" => Direction.NorthWest,
                "NorthEast" => Direction.NorthEast,
                "SouthWest" => Direction.SouthWest,
                "SouthEast" => Direction.SouthEast,
                _ => Direction.None,
            };
        }

        private static void ResetToDefault_OnClick()
        {
            PearlCalculatorLib.General.Data.Reset();
        }
    }
}
