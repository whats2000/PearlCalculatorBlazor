using PearlCalculatorBlazor.Localizer;
using System.Collections.Generic;

namespace PearlCalculatorBlazor.Components
{
    public partial class GeneralFTL
    {
        class Array
        {
            public string Key { get; set; }
            public string DisplayName { get; set; }
            public string ActiveKey { get; set; }
        }
        
        private List<Array> _selectList;

        protected override void OnInitialized()
        {
            _selectList = new List<Array>
            {
                new Array { ActiveKey="GFTL_General", Key = "GeneralFTLGeneralHeader", DisplayName = TranslateText.GetTranslateText("GeneralFTLGeneralHeader")},
                new Array { ActiveKey="GFTL_Advanced", Key = "GeneralFTLAdvancedHeader", DisplayName = TranslateText.GetTranslateText("GeneralFTLAdvancedHeader")},
                new Array { ActiveKey="GFTL_Settings", Key = "GeneralFTLSettingsHeader", DisplayName = TranslateText.GetTranslateText("GeneralFTLSettingsHeader")}
            };

            TranslateText.OnLanguageChange += RefreshPage;
        }

        public void RefreshPage()
        {
            foreach (var pair in _selectList)
                pair.DisplayName = TranslateText.GetTranslateText(pair.Key);

            StateHasChanged();
        }
    }
}
