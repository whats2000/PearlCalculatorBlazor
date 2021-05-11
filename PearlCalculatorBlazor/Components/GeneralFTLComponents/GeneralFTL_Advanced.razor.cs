using Microsoft.JSInterop;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.Result;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_Advanced
    {
        private static GeneralFTL_Advanced _instance;

        private double PearlOffSetX 
        {
            get => Data.PearlOffset.X;
            set => Data.PearlOffset.X = value;
        }

        private double PearlOffSetZ
        {
            get => Data.PearlOffset.Z;
            set => Data.PearlOffset.Z = value;
        }

        private double TNTWeight
        {
            get => Data.TNTWeight;
            set => Data.TNTWeight = (int)value;
        }

        public GeneralFTL_Advanced()
        {
            _instance = this;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender) return;
            ((IJSInProcessRuntime)JSRuntime).InvokeVoid("AddTNTWeightSliderEvent");
        }

        private void ChangeTNTWeight()
        {
            Data.TNTResult.SortByWeightedDistance(new(Data.TNTWeight, Data.MaxCalculateTNT, Data.MaxCalculateDistance));
            EventManager.Instance.PublishEvent(this, "calculate", new ButtonClickArgs("GFTL_General"));
        }

        [JSInvokable]
        public static void ChangeTNTWeightJS()
        {
            _instance.ChangeTNTWeight();
        }
    }
}
