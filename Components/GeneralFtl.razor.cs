using System.Collections.Generic;
using PearlCalculatorBlazor.Localizer;

namespace PearlCalculatorBlazor.Components;

public partial class GeneralFtl
{
    private string _activeKey = "GeneralFtlGeneral";
    private List<TabItem> _selectList;

    protected override void OnInitialized()
    {
        _selectList = new List<TabItem>
        {
            new()
            {
                ActiveKey = "GeneralFtlGeneral",
                DisplayName = TranslateText.GetTranslateText("GeneralFtlGeneral")
            },
            new()
            {
                ActiveKey = "GeneralFtlAdvanced",
                DisplayName = TranslateText.GetTranslateText("GeneralFtlAdvanced")
            },
            new()
            {
                ActiveKey = "GeneralFtlSettings",
                DisplayName = TranslateText.GetTranslateText("GeneralFtlSettings")
            },
            new()
            {
                ActiveKey = "GeneralFtlTntEncoding",
                DisplayName = TranslateText.GetTranslateText("GeneralFtlTntEncoding")
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

    private class TabItem
    {
        public string DisplayName { get; set; }
        public string ActiveKey { get; set; }
    }
}