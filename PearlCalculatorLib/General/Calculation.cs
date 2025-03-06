using PearlCalculatorLib.Result;
using PearlCalculatorLib.PearlCalculationLib;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using PearlCalculatorLib.PearlCalculationLib.Entity;

namespace PearlCalculatorLib.General
{
    public static class Calculation
    {
        private static PearlEntity PearlSimulation(int redTNT, int blueTNT, int ticks, Direction direction)
        {
            //Simulating the pearl and return the end point
            PearlEntity pearlEntity = new PearlEntity(Data.Pearl).AddPosition(Data.PearlOffset);
            
            // Override the Y position of the pearl when Y Motion cancellation is enabled
            if (Data.PearlYMotionCancellation)
            {
                pearlEntity.Position.Y = Data.PearlYPositionOriginal;
            }

            CalculateTNTVector(direction, out Space3D redTNTVector, out Space3D blueTNTVector);

            pearlEntity.Motion += redTNT * redTNTVector + blueTNT * blueTNTVector;
            
            // Adjust the Y and cancel the Y motion of the pearl after the calculation of TNT vector
            if (Data.PearlYMotionCancellation)
            {
                pearlEntity.Position.Y = Data.PearlYPositionAdjusted;
                pearlEntity.Motion.Y = 0;
            }

            for (int i = 0; i < ticks; i++)
                pearlEntity.Tick();

            return pearlEntity;
        }

        /// <summary>
        /// Calculate all the suitable TNT combination
        /// <para>There are some other parameters you had to input in <see cref="Data"/> for it to work correctly</para>
        /// <para>See more detail in <see cref="Data"/></para>
        /// </summary>
        /// <param name="maxTicks">The maximum time the Ender Pearl allowed to travel</param>
        /// <param name="maxDistance">The maximum difference between Ender Pearl drop off and Destination in each axis</param>
        /// <returns>Returns a true or false value indicates whether the calculation is correctly executed or not
        /// <para>TNT combination result will be stored into <see cref="Data.TNTResult"/></para>
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool CalculateTNTAmount(int maxTicks, double maxDistance)
        {
            int redTNT, blueTNT;
            double trueRed, trueBlue;
            Direction direction;
            Space3D trueDistance, truePosition;

            double divider = 0;

            truePosition = Data.Pearl.Position + Data.PearlOffset.ToSpace3D();
            trueDistance = Data.Destination - truePosition;

            //trueDistance with zero in X and Z axis triggers DevidedByZeroEception
            if (trueDistance.ToSurface2D() == 0)
                return false;

            Data.TNTResult.Clear();
            direction = DirectionUtils.GetDirection(truePosition.WorldAngle(Data.Destination));
            CalculateTNTVector(direction, out Space3D redTNTVector, out Space3D blueTNTVector);
            //Calculate redTNT and blueTNT through simultaneous equations
            trueRed = (trueDistance.Z * blueTNTVector.X - trueDistance.X * blueTNTVector.Z) / (redTNTVector.Z * blueTNTVector.X - blueTNTVector.Z * redTNTVector.X);
            trueBlue = (trueDistance.X - trueRed * redTNTVector.X) / blueTNTVector.X;

            for (int tick = 1; tick <= maxTicks; tick++)
            {
                //Factorization trueDistance to make a easier calculation
                divider += Math.Pow(0.99, tick - 1);

                redTNT = Convert.ToInt32(trueRed / divider);
                blueTNT = Convert.ToInt32(trueBlue / divider);

                //Run through the loops to get a better result around the redTNT and blueTNT
                //It is not perfect dude to the round up
                for (int r = -5; r <= 5; r++)
                {

                    for (int b = -5; b <= 5; b++)
                    {
                        //Simulate the pearl and get it's end point
                        PearlEntity pearlEntity = PearlSimulation(redTNT + r, blueTNT + b, tick, direction);

                        //Only the pearl with a difference smaller than 5 will pass the check
                        if ((pearlEntity.Position - Data.Destination).ToSurface2D().Absolute() <= maxDistance)
                        {

                            TNTCalculationResult result = new TNTCalculationResult
                            {
                                Distance = pearlEntity.Position.Distance2D(Data.Destination),
                                Tick = tick,
                                Blue = blueTNT + b,
                                Red = redTNT + r,
                                TotalTNT = blueTNT + b + redTNT + r,
                                Pearl = pearlEntity
                            };

                            //Bypass MaxTNT check if it is set to 0
                            //If MaxTNT is greater than 0 , only add those result which is lesser than MaxTNT
                            //If both redTNT and blueTNT are not greater or equal to zero , ignore them
                            if ((Data.MaxTNT <= 0 || (result.Blue <= Data.MaxTNT && result.Red <= Data.MaxTNT)) && result.Blue >= 0 && result.Red >= 0)
                                Data.TNTResult.Add(result);

                            if (Data.MaxCalculateTNT < result.TotalTNT)
                                Data.MaxCalculateTNT = result.TotalTNT;

                            if (Data.MaxCalculateDistance < result.Distance)
                                Data.MaxCalculateDistance = result.Distance;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Calculate the trace of the Ender Pearl
        /// <para>There are some other parameters you had to input in the <see cref="Data"/> class for it to work correctly</para>
        /// <para>See more detail in <see cref="Data"/> class</para>
        /// </summary>
        /// <param name="redTNT">The Amount of Red TNT for Accelerating Ender Pearl</param>
        /// <param name="blueTNT">The Amount of Blue TNT for Accelerating Ender Pearl</param>
        /// <param name="ticks">The Maximum Tick the Ender Pearl Allowed to travel</param>
        /// <param name="direction">The accelerating Direction of the Ender Pearl. Only Allow North, South, East, West</param>
        /// <returns>Return A List of Entity contains the Motion and Position in each Ticks</returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<Entity> CalculatePearlTrace(int redTNT, int blueTNT, int ticks, Direction direction)
        {
            List<Entity> result = new List<Entity>(ticks + 1);
            PearlEntity pearl = new PearlEntity(Data.Pearl.AddPosition(Data.PearlOffset));
            
            // Override the Y position of the pearl when Y Motion cancellation is enabled
            if (Data.PearlYMotionCancellation)
            {
                pearl.Position.Y = Data.PearlYPositionOriginal;
            }

            CalculateTNTVector(direction, out Space3D redTNTVector, out Space3D blueTNTVector);
            pearl.Motion += redTNT * redTNTVector + blueTNT * blueTNTVector;
            
            // Adjust the Y and cancel the Y motion of the pearl after the calculation of TNT vector
            if (Data.PearlYMotionCancellation)
            {
                pearl.Position.Y = Data.PearlYPositionAdjusted;
                pearl.Motion.Y = 0;
            }
            
            result.Add(new PearlEntity(pearl));

            for (int i = 0; i < ticks; i++)
            {
                pearl.Tick();
                result.Add(new PearlEntity(pearl));
            }

            return result;
        }

        /// <summary>
        /// Calculate the combination of TNT
        /// </summary>
        /// <param name="redTNT">The amount of red TNT</param>
        /// <param name="blueTNT">The amount of blue TNT</param>
        /// <param name="tntCombination">Output the TNT combination</param>
        /// <returns>Returns a true or false value indicates whether the calculation is correctly executed or not</returns>
        public static bool CalculateTNTConfiguration(int redTNT, int blueTNT, out BitArray tntCombination)
        {
            // Total number of bits equals the count of red and blue configuration values combined.
            int totalCount = Data.RedTNTConfiguration.Count + Data.BlueTNTConfiguration.Count;
            tntCombination = new BitArray(totalCount);

            // If both configurations are empty, return false.
            if (Data.RedTNTConfiguration.Count == 0 && Data.BlueTNTConfiguration.Count == 0)
                return false;

            // Create BitArrays for red and blue configurations separately.
            // Their sizes correspond to the number of configuration items.
            BitArray redBitArray = new BitArray(Data.RedTNTConfiguration.Count);
            BitArray blueBitArray = new BitArray(Data.BlueTNTConfiguration.Count);

            // Create lists of anonymous objects to keep both value and original index.
            var redConfig = Data.RedTNTConfiguration
                .Select((value, index) => new { Value = value, Index = index })
                .ToList();
            var blueConfig = Data.BlueTNTConfiguration
                .Select((value, index) => new { Value = value, Index = index })
                .ToList();

            // Sort both lists in descending order by value.
            redConfig.Sort((a, b) => b.Value.CompareTo(a.Value));
            blueConfig.Sort((a, b) => b.Value.CompareTo(a.Value));

            // Process red TNT configuration
            foreach (var config in redConfig)
            {
                if (redTNT >= config.Value)
                {
                    redTNT -= config.Value;
                    redBitArray.Set(config.Index, true);
                }
                else
                {
                    redBitArray.Set(config.Index, false);
                }
            }

            // Process blue TNT configuration
            foreach (var config in blueConfig)
            {
                if (blueTNT >= config.Value)
                {
                    blueTNT -= config.Value;
                    blueBitArray.Set(config.Index, true);
                }
                else
                {
                    blueBitArray.Set(config.Index, false);
                }
            }

            // Combine red and blue BitArrays into the final tntCombination.
            // First, copy the red configuration bits.
            for (int i = 0; i < redBitArray.Count; i++)
            {
                tntCombination[i] = redBitArray[i];
            }
            // Then, copy the blue configuration bits to the correct offset.
            for (int i = 0; i < blueBitArray.Count; i++)
            {
                tntCombination[Data.RedTNTConfiguration.Count + i] = blueBitArray[i];
            }

            return true;
        }

        /// <summary>
        /// Calculate the accelerating vector created by Blue and Red TNT in a given Direction
        /// </summary>
        /// <param name="direction">The Acceleration Direction of the Ender Pearl</param>
        /// <param name="redTNTVector">Return an accelerating vector of Red TNT</param>
        /// <param name="blueTNTVector">Return an accelerating vector of Blue TNT</param>
        /// <exception cref="ArgumentException"></exception>
        public static void CalculateTNTVector(Direction direction, out Space3D redTNTVector, out Space3D blueTNTVector)
        {
            Space3D pearlPosition = Data.PearlOffset.ToSpace3D();

            redTNTVector = blueTNTVector = Space3D.Zero;
            pearlPosition.Y = Data.Pearl.Position.Y;

            if ((Data.DefaultBlueDuper | Data.DefaultRedDuper) != (Direction.NorthEast | Direction.SouthWest))
                throw new ArgumentException("Wrong value in Data.DefaultBlueDuper and Data,DefaultRedDuper");

            if ((direction & Data.DefaultBlueDuper) == 0)
            {
                //There is always a TNT that is already stand by
                //In this case, it is blue.
                blueTNTVector = VectorCalculation.CalculateMotion(pearlPosition, TNTDirectionToCoordinate(Data.DefaultBlueDuper));
                redTNTVector = VectorCalculation.CalculateMotion
                    (
                        pearlPosition,
                        TNTDirectionToCoordinate
                        (
                            // 0b1111 = Direction.NorthEast | Direction.SouthWest
                            (Direction)(((int)~(direction | Data.DefaultBlueDuper) & 0b1111) | (int)direction.Invert())
                        )
                    );
            }
            else
            {
                redTNTVector = VectorCalculation.CalculateMotion(pearlPosition, TNTDirectionToCoordinate(Data.DefaultRedDuper));
                blueTNTVector = VectorCalculation.CalculateMotion
                    (
                        pearlPosition,
                        TNTDirectionToCoordinate
                        (
                            (Direction)(((int)~(direction | Data.DefaultRedDuper) & 0b1111) | (int)direction.Invert())
                        )
                    );
            }
        }

        private static Space3D TNTDirectionToCoordinate(Direction coner)
        {
            return coner switch
            {
                Direction.NorthEast => Data.NorthEastTNT,
                Direction.NorthWest => Data.NorthWestTNT,
                Direction.SouthEast => Data.SouthEastTNT,
                Direction.SouthWest => Data.SouthWestTNT,
                _ => Space3D.Zero
            };
        }
    }
}
