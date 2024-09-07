using System.Collections.Generic;
using PearlCalculatorLib.Manually;
using PearlCalculatorLib.PearlCalculationLib.Entity;
using PearlCalculatorLib.Result;

namespace PearlCalculatorBlazor.Managers;

public abstract class EventArgs
{
    public readonly string PublishKey;

    protected EventArgs(string publishKey)
    {
        PublishKey = publishKey;
    }
}

public class BaseEventArgs : EventArgs
{
    public BaseEventArgs(string publishKey) : base(publishKey)
    {
    }
}

public class PearlSimulateArgs : EventArgs
{
    public readonly PearlEntity Pearl;
    public readonly List<Entity> Trace;

    public PearlSimulateArgs(string publishKey, PearlEntity pearl, List<Entity> trace) : base(publishKey)
    {
        Pearl = pearl;
        Trace = trace;
    }
}

public class PearlSimulateManuallyArgs : EventArgs
{
    public readonly ManuallyData ManuallyData;
    public readonly List<Entity> Trace;

    public PearlSimulateManuallyArgs(string publishKey, ManuallyData manuallyData, List<Entity> trace) :
        base(publishKey)
    {
        ManuallyData = manuallyData;
        Trace = trace;
    }
}

public class SetRtCountArgs : EventArgs
{
    public readonly int Blue;
    public readonly int Red;

    public SetRtCountArgs(string publishKey, int red, int blue) : base(publishKey)
    {
        Red = red;
        Blue = blue;
    }
}

public class CalculateTntAmountArgs : EventArgs
{
    public readonly ManuallyData ManuallyData;
    public readonly List<TNTCalculationResult> Results;

    public CalculateTntAmountArgs(string publishKey, ManuallyData manuallyData, List<TNTCalculationResult> results) :
        base(publishKey)
    {
        ManuallyData = manuallyData;
        Results = results;
    }
}