using PearlCalculatorLib.PearlCalculationLib.Utility;
using PearlCalculatorLib.PearlCalculationLib.World;
using System;

namespace PearlCalculatorLib.PearlCalculationLib.Entity
{
    [Serializable]
    public class TNTEntity : Entity
    {
        public override Space3D Size => new Space3D(0.98, 0.98, 0.98);

        public override void Tick(GameVersion version) => throw new NotImplementedException();
    }
}
