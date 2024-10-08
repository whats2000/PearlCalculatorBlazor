﻿using System;

namespace PearlCalculatorLib.PearlCalculationLib.World
{
    [Serializable]
    public struct Surface2D
    {
        public static readonly Surface2D Zero = new Surface2D();

        public double X;
        public double Z;

        public double Length => Math.Sqrt(X * X + Z * Z);

        public Surface2D Normalized => Length == 0 ? Zero : this / Length;

        public Surface2D(double x, double z)
        {
            X = x;
            Z = z;
        }

        public Space3D ToSpace3D() => new Space3D(X, 0, Z);

        public override string ToString() => $"Coordinate : {X} , {Z}";

        public bool IsNorth(Surface2D position2) => position2.X > X;

        public bool IsSouth(Surface2D position2) => position2.X < X;

        public bool IsEast(Surface2D position2) => position2.Z < Z;

        public bool IsWest(Surface2D position2) => position2.Z > Z;

        public bool IsClockWise(Surface2D vector2) => vector2.Transform(Normalized, Zero) > 0;

        public bool IsCounterClockWise(Surface2D vector2) => vector2.Transform(Normalized, Zero) < 0;

        public double Angle(Surface2D position2) => Math.Atan((position2.Z - Z) / (position2.X - X)) / Math.PI * 180;

        public bool IsOrigin() => X == 0 && Z == 0;

        public bool IsInside(Surface2D southEastConer, Surface2D northWestConer)
        {
            return X < southEastConer.X && Z < southEastConer.Z && X > northWestConer.X && Z > northWestConer.Z;
        }

        public double AngleInRad(Surface2D position2) => Math.Atan(position2.Z - Z / position2.X - X);

        public static Space3D FromPolarCoordinate(double lenght, double radinat) => new Space3D
        {
            X = lenght * Math.Sin(radinat),
            Z = lenght * Math.Cos(radinat)
        };

        public Surface2D Absolute() => new Surface2D(Math.Abs(X), Math.Abs(Z));

        public double Distance(Surface2D position2) => Math.Sqrt(Math.Pow(position2.X - X, 2) + Math.Pow(position2.Z - Z, 2));

        public override bool Equals(object obj) => obj is Surface2D s && Equals(s);

        public Surface2D Transform(Surface2D iHat, Surface2D jHat) => X * iHat + Z * jHat;

        public bool AxialDistanceLessThan(double distance) => Math.Abs(X) < distance && Math.Abs(Z) < distance;

        public bool AxialDistanceLargerThan(double distance) => Math.Abs(X) > distance && Math.Abs(Z) > distance;

        public bool AxialDistanceEqualTo(double distance) => Math.Abs(X) == distance && Math.Abs(Z) == distance;

        public bool AxialDistanceLessOrEqualTo(double distance) => AxialDistanceLessThan(distance) || AxialDistanceEqualTo(distance);

        public bool AxialDistanceLargerOrEqualTo(double distance) => AxialDistanceLargerThan(distance) || AxialDistanceEqualTo(distance);

        public Chunk ToChunk() => new Chunk((int)Math.Floor(X / 16), (int)Math.Floor(Z / 16));

        public Surface2D Round() => new Surface2D(Math.Round(X), Math.Round(Z));

        public override int GetHashCode() => X.GetHashCode() ^ Z.GetHashCode();

        public static Surface2D operator +(Surface2D left, Surface2D right) => new Surface2D(left.X + right.X, left.Z + right.Z);

        public static Surface2D operator -(Surface2D left, Surface2D right) => new Surface2D(left.X - right.X, left.Z - right.Z);

        public static Surface2D operator *(Surface2D left, double right) => new Surface2D(left.X * right, left.Z * right);

        public static Surface2D operator *(double left, Surface2D right) => new Surface2D(left * right.X, left * right.Z);

        public static Surface2D operator /(Surface2D left, double right) => new Surface2D(left.X / right, left.Z / right);

        public static bool operator <(Surface2D left, double right) => left.X < right && left.Z < right;

        public static bool operator >(Surface2D left, double right) => left.X > right && left.Z > right;

        public static bool operator <(Surface2D left, int right) => left.X < right && left.Z < right;

        public static bool operator >(Surface2D left, int right) => left.X > right && left.Z > right;

        public static bool operator <=(Surface2D left, double right) => left.X <= right && left.Z <= right;

        public static bool operator >=(Surface2D left, double right) => left.X >= right && left.Z >= right;

        public static bool operator <=(Surface2D left, int right) => left.X <= right && left.Z <= right;

        public static bool operator >=(Surface2D left, int right) => left.X >= right && left.Z >= right;

        public static bool operator ==(Surface2D left, double right) => left.X == right && left.Z == right;
        public static bool operator !=(Surface2D left, double right) => !(left == right);

        public static bool operator ==(Surface2D left, int right) => left.X == right && left.Z == right;
        public static bool operator !=(Surface2D left, int right) => !(left == right);
    }
}
