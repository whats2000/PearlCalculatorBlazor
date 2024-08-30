using PearlCalculatorLib.Manually;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;
using System.Collections.Generic;

namespace PearlCalculatorBlazor
{
    public abstract class EventArgs
    {
        public readonly string PublishKey;

        public EventArgs(string publishKey)
        {
            this.PublishKey = publishKey;
        }
    }

    public class ButtonClickArgs : EventArgs
    {
        public ButtonClickArgs(string publishKey) : base(publishKey)
        {
        }
    }

    public class PearlSimulateArgs : EventArgs
    {
        public readonly PearlEntity Pearl = new();
        public readonly List<Entity> Trace;

        public PearlSimulateArgs(string publishKey, PearlEntity pearl, List<Entity> trace) : base(publishKey)
        {
            this.Pearl = pearl;
            this.Trace = trace;
        }
    }

    public class SetRTCountArgs : EventArgs
    {
        public readonly int Red;
        public readonly int Blue;

        public SetRTCountArgs(string publishKey, int red, int blue) : base(publishKey)
        {
            this.Red = red;
            this.Blue = blue;
        }
    }

    public class CalculateTNTAmuontArgs : EventArgs
    {
        public readonly ManuallyData ManuallyData;
        public readonly List<TNTCalculationResult> Results;

        public CalculateTNTAmuontArgs(string publishKey, ManuallyData manuallyData, List<TNTCalculationResult> results) : base(publishKey)
        {
            this.ManuallyData = manuallyData;
            this.Results = results;
        }
    }
}
