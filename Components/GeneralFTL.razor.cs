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
            new()
            {
                ActiveKey = "GeneralFtlGeneral", DisplayName = TranslateText.GetTranslateText("GeneralFtlGeneral")
            },
            new()
            {
                ActiveKey = "GeneralFtlAdvanced", DisplayName = TranslateText.GetTranslateText("GeneralFtlAdvanced")
            },
            new()
            {
                ActiveKey = "GeneralFtlSettings", DisplayName = TranslateText.GetTranslateText("GeneralFtlSettings")
            }
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