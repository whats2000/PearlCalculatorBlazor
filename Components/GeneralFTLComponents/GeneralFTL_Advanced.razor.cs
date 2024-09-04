﻿using Microsoft.JSInterop;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorBlazor.Managers;
using PearlCalculatorLib.General;
using PearlCalculatorLib.PearlCalculationLib.World;

namespace PearlCalculatorBlazor.Components.GeneralFTLComponents;

public enum SortMode
{
    SortByWeightedDistance,
    SortByWeightedTotal
}

public partial class GeneralFTL_Advanced
{
    private static GeneralFTL_Advanced _instance;
    private static SortMode _sortBy = SortMode.SortByWeightedDistance;

    public GeneralFTL_Advanced()
    {
        _instance = this;
    }

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

    private double TntWeight
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

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        ((IJSInProcessRuntime)JsRuntime).InvokeVoid("AddTNTWeightSliderEvent");
    }

    private void ChangeTntWeight()
    {
        var isSortByWeightedDistance = SelectSortMode == SortMode.SortByWeightedDistance;
        EventManager.Instance.PublishEvent(this,
            isSortByWeightedDistance ? "sortByWeightedDistance" : "sortByWeightedTotal",
            new ButtonClickArgs("GFTL_Advanced"));
    }

    [JSInvokable]
    public static void ChangeTntWeightJs()
    {
        _instance.ChangeTntWeight();
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