using Microsoft.JSInterop;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_Advanced
    {
        private static GeneralFTL_Advanced _instance;

        public double PearlOffsetX
        {
            get => Data.PearlOffset.X;
            set => Data.PearlOffset = new Surface2D(value, PearlOffsetZ);            
        }

        public double PearlOffsetZ
        {
            get => Data.PearlOffset.Z;
            set => Data.PearlOffset = new Surface2D(PearlOffsetX, value);
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
            EventManager.Instance.PublishEvent(this, "resortResult", new ButtonClickArgs("GFTL_Advanced"));
        }

        [JSInvokable]
        public static void ChangeTNTWeightJS()
        {
            _instance.ChangeTNTWeight();
        }

    }
}
