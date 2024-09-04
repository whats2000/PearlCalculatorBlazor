using System.Collections.Generic;
using PearlCalculatorBlazor.Localizer;

namespace PearlCalculatorBlazor.Components;

public partial class GeneralFTL
{
    private List<Array> _selectList;

    protected override void OnInitialized()
    {
        _selectList = new List<Array>
        {
            new() { ActiveKey = "GFTL_General", DisplayName = TranslateText.GetTranslateText("GFTL_General") },
            new() { ActiveKey = "GFTL_Advanced", DisplayName = TranslateText.GetTranslateText("GFTL_Advanced") },
            new() { ActiveKey = "GFTL_Settings", DisplayName = TranslateText.GetTranslateText("GFTL_Settings") }
        };

        TranslateText.OnLanguageChange += RefreshPage;
    }

    private void RefreshPage()
    {
        foreach (var pair in _selectList)
            pair.DisplayName = TranslateText.GetTranslateText(pair.ActiveKey);

        StateHasChanged();
    }

    private class Array
    {
        public string DisplayName { get; set; }
        public string ActiveKey { get; set; }
    }
}