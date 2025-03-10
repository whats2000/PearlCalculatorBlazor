﻿using System.Collections.Generic;
using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorLib.General
{
    public static class Data
    {
        /// <summary>
        /// The Local Coordinate of the North West TNT
        /// <para>Origin is set to the middle of the lava pool</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Space3D NorthWestTNT { get; set; } = new Space3D(-0.884999990463257, 170.5, -0.884999990463257);

        /// <summary>
        /// The Local Coordinate of the North East TNT
        /// <para>Origin is set to the middle of the lava pool</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Space3D NorthEastTNT { get; set; } = new Space3D(+0.884999990463257, 170.5, -0.884999990463257);

        /// <summary>
        /// The Local Coordinate of the South West TNT
        /// <para>Origin is set to the middle of the lava pool</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Space3D SouthWestTNT { get; set; } = new Space3D(-0.884999990463257, 170.5, +0.884999990463257);

        /// <summary>
        /// The Local Coordinate of the South East TNT
        /// <para>Origin is set to the middle of the lava pool</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Space3D SouthEastTNT { get; set; } = new Space3D(+0.884999990463257, 170.5, +0.884999990463257);



        /// <summary>
        /// The Gobal Coordinate of the Destination
        /// <para>Note : Y coordinate can be ignored. It does not take part in any calculation</para>
        /// <para>Required for <see cref="Calculation.CalculateTNTAmount(int, double)"/></para>
        /// </summary>
        public static Space3D Destination { get; set; } = new Space3D();



        /// <summary>
        /// The Gobal Coordinate(Only Y Axis is related to pearl) and motion of the Ender Pearl
        /// <para>Note : X and Z should be the gobal coordinate of the lava pool center</para>
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static PearlEntity Pearl { get; set; } = new PearlEntity().WithPosition(0, 170.34722638929412, 0).WithMotion(0, 0.2716278719434352, 0);


        /// <summary>
        /// The Flag determine whether the pearl alignment is using Y Motion cancellation technique
        /// <para>Note : The default is without Y Motion cancellation</para>
        /// </summary>
        public static bool PearlYMotionCancellation { get; set; } = false;

        /// <summary>
        /// The Y position of the pearl before Y Motion cancellation, which is the position of the pearl taking TNT Motion into account
        /// <para>Note : To get the Y position of the pearl, you should remove the piston which is used to cancel the Y motion and get the Y position of the pearl at explosion tick</para>
        /// <para>Required when <see cref="Data.UseYMotionCancellation"/> is set to true</para>
        /// </summary>
        public static double PearlYPositionOriginal { get; set; } = 170.34722638929412;

        /// <summary>
        /// The Y position of the pearl after Y Motion cancellation, which is set after the explosion takes place.
        /// <para>Note : To get the Y position of the pearl, just place the piston which is used to cancel the Y motion and get the Y position at the end of the tick</para>
        /// <para>Required when <see cref="Data.UseYMotionCancellation"/> is set to true</para>
        /// </summary>
        public static double PearlYPositionAdjusted { get; set; } = 170.0;
        
        /// <summary>
        /// The Offset between Ender Pearl X and Z coordinate and lava pool center coordinate
        /// <para>Required for All Calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Surface2D PearlOffset
        {
            get => _PearlOffset;
            set
            {
                if (value.IsInside(SouthEastTNT.ToSurface2D(), NorthWestTNT.ToSurface2D()))
                    _PearlOffset = value;
            }
        }
        private static Surface2D _PearlOffset = new Surface2D(0, 0);



        /// <summary>
        /// The number of Blue TNT for accelerating the Ender Pearl
        /// <para>Note : Not bound to any calculation. You can store Blue acceleration TNT here or ignore this </para>
        /// </summary>
        public static int BlueTNT { get; set; }

        /// <summary>
        /// The number of Red TNT for accelerating the Ender Pearl
        /// <para>Note : Not bound to any calculation. You can store Red acceleration TNT here or ignore this </para>
        /// </summary>
        public static int RedTNT { get; set; }



        /// <summary>
        /// The weight value for sorting the result
        /// <para>Note : Value Must be a Natural Number which is not larger than 100</para>
        /// <para>Required for <see cref="TNTCalculationResultSortComparerByWeighted"/></para>
        /// </summary>
        public static int TNTWeight { get; set; }



        /// <summary>
        /// The Max number of TNT from each side
        /// <para>Required for <see cref="Calculation.CalculateTNTAmount(int, double)"/></para>
        /// </summary>
        public static int MaxTNT { get; set; }



        public static int MaxCalculateTNT { get; internal set; }



        public static double MaxCalculateDistance { get; internal set; }



        /// <summary>
        /// The acceleration direction of the Ender Pearl
        /// <para>Note : Not bound to any calculation. You can store your acceleration direction here or ignore this </para>
        /// <para>Note : Only Allow For North, South, East, West</para>
        /// </summary>
        public static Direction Direction { get; set; } = Direction.North;



        /// <summary>
        /// The default position in the lava pool. Should be opposite to Blue
        /// <para>Note : Only Allow For NorthWest, NorthEast, SouthWest, SouthEast</para>
        /// <para>Required for all calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Direction DefaultRedDuper
        {
            get => _DefaultRedDuper;
            set
            {
                if ((value.IsNorth() || value.IsSouth()) && (value.IsEast() || value.IsWest()))
                    _DefaultRedDuper = value;
            }
        }

        private static Direction _DefaultRedDuper = Direction.SouthEast;



        /// <summary>
        /// The default position in the lava pool. Should be opposite to Red
        /// <para>Note : Only Allow For NorthWest, NorthEast, SouthWest, SouthEast</para>
        /// <para>Required for all calculation in <see cref="Calculation"/></para>
        /// </summary>
        public static Direction DefaultBlueDuper
        {
            get => _DefaultBlueDuper;
            set
            {
                if ((value.IsNorth() || value.IsSouth()) && (value.IsEast() || value.IsWest()))
                    _DefaultBlueDuper = value;
            }
        }

        private static Direction _DefaultBlueDuper = Direction.NorthWest;



        /// <summary>
        /// The Result of the <see cref="Calculation.CalculateTNTAmount(int, double)"/>
        /// <para>Note : It is a Data Output</para>
        /// </summary>
        public static List<TNTCalculationResult> TNTResult { get; set; } = new List<TNTCalculationResult>();


        /// <summary>
        /// A list of TNT configuration. 
        /// </summary>
        public static List<int> RedTNTConfiguration { get; set; } = new List<int>();

        /// <summary>
        /// A list if TNT configuration
        /// </summary>
        public static List<int> BlueTNTConfiguration { get; set; } = new List<int>();

        /// <summary>
        /// The version of minecraft the calculation is for
        /// </summary>
        public static GameVersion GameVersion { get; set; } = GameVersion.Version111To1211;



        /// <summary>
        /// Reset the whole <see cref="Data"/> into default value
        /// </summary>
        public static void Reset()
        {
            NorthWestTNT = new Space3D(-0.884999990463257, 170.5, -0.884999990463257);
            NorthEastTNT = new Space3D(+0.884999990463257, 170.5, -0.884999990463257);
            SouthWestTNT = new Space3D(-0.884999990463257, 170.5, +0.884999990463257);
            SouthEastTNT = new Space3D(+0.884999990463257, 170.5, +0.884999990463257);
            Pearl.Position = new Space3D(0, 170.34722638929412, 0);
            Pearl.Motion = new Space3D(0, 0.2716278719434352, 0);
            PearlYMotionCancellation = false;
            PearlYPositionOriginal = 170.34722638929412;
            PearlYPositionAdjusted = 170.0;
            DefaultRedDuper = Direction.SouthEast;
            DefaultBlueDuper = Direction.NorthWest;
            PearlOffset = new Surface2D();
            GameVersion = GameVersion.Version111To1211;
        }
    }
}
