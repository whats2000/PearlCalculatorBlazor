using System;
using System.Net.Sockets;
using Microsoft.JSInterop;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;
using PearlCalculatorLib.Result;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents
{
    public partial class GeneralFTL_Advanced
    {
        private static GeneralFTL_Advanced _instance;
        private static SortMode _sortBy = SortMode.SortByWeightedDistance;

        private double PearlOffsetX
        {
            get => Data.PearlOffset.X;
            set => Data.PearlOffset = new Surface2D(value, PearlOffsetZ);
        }

        private double PearlOffsetZ
        {
            get => Data.PearlOffset.Z;
            set => Data.PearlOffset = new Surface2D(PearlOffsetX, value);
        }

        private double TNTWeight
        {
            get => Data.TNTWeight;
            set => Data.TNTWeight = (int)value;
        }
        
        private SortMode SelectSortMode
        {
            get => _sortBy;
            set
            {
                _sortBy = value;
                StateHasChanged();
            }
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
            bool isSortByWeightedDistance = SelectSortMode == SortMode.SortByWeightedDistance;
            EventManager.Instance.PublishEvent(this, isSortByWeightedDistance ? "sortByWeightedDistance" : "sortByWeightedTotal", new ButtonClickArgs("GFTL_Advanced"));
        }

        [JSInvokable]
        public static void ChangeTNTWeightJS()
        {
            _instance.ChangeTNTWeight();
        }

        protected override void OnInitialized()
        {
            TranslateText.OnLanguageChange += RefreshPage;
        }

        private void RefreshPage()
        {
            StateHasChanged();
        }
    }
    
    public enum SortMode
    {
        SortByWeightedDistance,
        SortByWeightedTotal
    }
}
