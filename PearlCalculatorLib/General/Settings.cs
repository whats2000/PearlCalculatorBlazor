using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.Utility;

namespace PearlCalculatorLib.General
{
    [Serializable]
    public class Settings
    {
        public const string Version = "2.8";

        public Space3D NorthWestTNT;
        public Space3D NorthEastTNT;
        public Space3D SouthWestTNT;
        public Space3D SouthEastTNT;
        public Space3D Destination;
        public Surface2D Offset;
        public PearlEntity Pearl;
        public int RedTNT;
        public int BlueTNT;
        public int MaxTNT;
        public Direction Direction;
        public Direction DefaultRedTNTDirection;
        public Direction DefaultBlueTNTDirection;
        public bool PearlYMotionCancellation;
        public double PearlYPositionOriginal;
        public double PearlYPositionAdjusted;
        public GameVersion GameVersion;

        public static Settings CreateSettingsFormData() => new Settings()
        {
            NorthWestTNT = Data.NorthWestTNT,
            NorthEastTNT = Data.NorthEastTNT,
            SouthWestTNT = Data.SouthWestTNT,
            SouthEastTNT = Data.SouthEastTNT,

            Pearl = Data.Pearl,

            RedTNT = Data.RedTNT,
            BlueTNT = Data.BlueTNT,
            MaxTNT = Data.MaxTNT,

            Destination = Data.Destination,
            Offset = Data.PearlOffset,

            Direction = Data.Direction,

            DefaultRedTNTDirection = Data.DefaultRedDuper,
            DefaultBlueTNTDirection = Data.DefaultBlueDuper,
            
            PearlYMotionCancellation = Data.PearlYMotionCancellation,
            PearlYPositionOriginal = Data.PearlYPositionOriginal,
            PearlYPositionAdjusted = Data.PearlYPositionAdjusted,

            GameVersion = Data.gameVersion
        };
    }
}
