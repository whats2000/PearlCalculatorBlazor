using PearlCalculatorLib.PearlCalculationLib.World;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    [Serializable]
    public abstract class Entity
    {
        public Space3D Motion;
        public Space3D Position;

        public abstract Space3D Size { get; }

        public abstract void Tick();
    }
}
