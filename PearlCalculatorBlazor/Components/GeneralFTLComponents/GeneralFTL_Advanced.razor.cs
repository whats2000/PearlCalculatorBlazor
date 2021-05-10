using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.Result;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_Advanced
    {
        private static double PearlOffSetX 
        {
            get => Data.PearlOffset.X;
            set => Data.PearlOffset.X = value;
        }

        private static double PearlOffSetZ
        {
            get => Data.PearlOffset.Z;
            set => Data.PearlOffset.Z = value;
        }

        private static double TNTWeight
        {
            get => Data.TNTWeight;
            set => Data.TNTWeight = (int)value;
        }

        private void CalculateTNTAmount()
        {
            Data.TNTResult.SortByWeightedDistance(new(Data.TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
            EventManager.Instance.PublishEvent(this, "calculate", new ButtonClickArgs("GFTL_General"));
        }
    }
}
