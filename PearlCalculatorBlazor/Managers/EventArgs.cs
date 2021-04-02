using PearlCalculatorLib.PearlCalculationLib.Entity;
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
        public readonly List<Entity> Trace;

        public PearlSimulateArgs(string publishKey, List<Entity> trace) : base(publishKey)
        {
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
}
