using System;
using System.Collections.Generic;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.PearlCalculationLib.Utility;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorLib.Settings
{
    [Serializable]
    public class CannonSettings : IDeepCloneable<CannonSettings>
    {
        public string CannonName { get; set; }

        public int MaxTNT { get; set; }

        public Direction DefaultRedDirection { get; set; }
        public Direction DefaultBlueDirection  { get; set; }
        
        public Space3D NorthWestTNT  { get; set; }
        public Space3D NorthEastTNT  { get; set; }
        public Space3D SouthWestTNT { get; set; }
        public Space3D SouthEastTNT { get; set; }
        
        public Surface2D Offset { get; set; }
        public PearlEntity Pearl { get; set; }

        public List<int> RedTNTConfiguration { get; set; }
        public List<int> BlueTNTConfiguration { get; set; }
        
        public bool PearlYMotionCancellation { get; set; }
        public double PearlYPositionOriginal { get; set; }
        public double PearlYPositionAdjusted { get; set; }
        public GameVersion gameVersion { get; set; }
        
        public CannonSettings DeepClone()
        {
            List<int> redTNTConfiguration = new List<int>(RedTNTConfiguration.Count);
            List<int> blueTNTConfiguration = new List<int>(BlueTNTConfiguration.Count);
            
            redTNTConfiguration.AddRange(RedTNTConfiguration);
            blueTNTConfiguration.AddRange(BlueTNTConfiguration);

            CannonSettings result = new CannonSettings
            {
                CannonName           = $"{CannonName}(clone)",
                MaxTNT               = MaxTNT,
                DefaultRedDirection  = DefaultRedDirection,
                DefaultBlueDirection = DefaultBlueDirection,
                NorthWestTNT         = NorthWestTNT,
                NorthEastTNT         = NorthEastTNT,
                SouthWestTNT         = SouthWestTNT,
                SouthEastTNT         = SouthEastTNT,
                Offset               = Offset,
                Pearl                = Pearl.DeepClone(),
                RedTNTConfiguration  = redTNTConfiguration,
                BlueTNTConfiguration = blueTNTConfiguration,
                PearlYMotionCancellation = PearlYMotionCancellation,
                PearlYPositionOriginal = PearlYPositionOriginal,
                PearlYPositionAdjusted = PearlYPositionAdjusted,
                gameVersion = gameVersion
            };

            return result;
        }
        
        public void SyncWithData()
        {
            MaxTNT                   = Data.MaxTNT;
            DefaultRedDirection      = Data.DefaultRedDuper;
            DefaultBlueDirection     = Data.DefaultBlueDuper;
            NorthWestTNT             = Data.NorthWestTNT;
            NorthEastTNT             = Data.NorthEastTNT;
            SouthWestTNT             = Data.SouthWestTNT;
            SouthEastTNT             = Data.SouthEastTNT;
            Offset                   = Data.PearlOffset;
            Pearl                    = Data.Pearl.DeepClone();
            RedTNTConfiguration      = Data.RedTNTConfiguration;
            BlueTNTConfiguration     = Data.BlueTNTConfiguration;
            PearlYMotionCancellation = Data.PearlYMotionCancellation;
            PearlYPositionOriginal   = Data.PearlYPositionOriginal;
            PearlYPositionAdjusted   = Data.PearlYPositionAdjusted;
            gameVersion              = Data.gameVersion;
        }

        object IDeepCloneable.DeepClone() => DeepClone();
    }
}
