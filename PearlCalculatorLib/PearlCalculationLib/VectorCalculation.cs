using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.PearlCalculationLib.MathLib;
using System;

namespace PearlCalculatorLib.PearlCalculationLib
{
    public static class VectorCalculation
    {
        /// <summary>
        /// Calculate the accelerating vector of the TNT
        /// </summary>
        /// <param name="pearlPosition">The Gobal Coordinate of the Ender Pearl(Might Occur Error when it is too far away from the TNT</param>
        /// <param name="tntPosition">The Gobal Coordinate of the TNT(Might Occur Error when it is too far away from the TNT</param>
        /// <param name="gameVersion">The version of the game</param>
        /// <returns>Return the accelerating vector of the TNT</returns>
        public static Space3D CalculateMotion(Space3D pearlPosition , Space3D tntPosition, GameVersion gameVersion)
        {
            tntPosition.Y += 0.98F * 0.0625D;
            Space3D distance = pearlPosition - tntPosition;
            
            
            // double distanceSqrt = gameVersion == GameVersion.Version111To1211 ? MathHelper.Sqrt(distance.DistanceSq()) : Math.Sqrt(distance.DistanceSq());
            // double d12 = distanceSqrt / (gameVersion == GameVersion.Version111To1211 ? 8 : 8.0f);
            // Space3D vector = new Space3D(distance.X , pearlPosition.Y + (0.85F * 0.25F) - tntPosition.Y , distance.Z);
            // double d13 = gameVersion == GameVersion.Version111To1211 ? MathHelper.Sqrt(vector.DistanceSq()) : Math.Sqrt(vector.DistanceSq());

            if (gameVersion == GameVersion.Version111To1211)
            {
                double distanceSqrt = MathHelper.Sqrt(distance.DistanceSq());
                double d12 = distanceSqrt / 8;
                Space3D vector = new Space3D(distance.X , pearlPosition.Y + (0.85F * 0.25F) - tntPosition.Y , distance.Z);
                double d13 = MathHelper.Sqrt(vector.DistanceSq());
                
                vector /= d13;
                double d11 = (1.0D - d12);
                return new Space3D(vector * d11);
            }
            else
            {   
                double distanceSqrt = Math.Sqrt(distance.DistanceSq());
                double d12 = distanceSqrt / (8.0f);
                Space3D vector = new Space3D(distance.X , pearlPosition.Y + (0.85F * 0.25F) - tntPosition.Y , distance.Z);
                double d13 = Math.Sqrt(vector.DistanceSq());
            
                vector /= d13;
                double d11 = (1.0D - d12);
                return new Space3D(vector * d11);
            }
        }
    }
}
