using PearlCalculatorBlazor.Localizer;
using System.Collections.Generic;

namespace PearlCalculatorBlazor.Components
{
    public partial class GeneralFTL
    {
        class Array
        {
            public string DisplayName { get; set; }
            public string ActiveKey { get; set; }
        }

        private List<Array> _selectList;

        protected override void OnInitialized()
        {
            _selectList = new List<Array>
            {
                new Array { ActiveKey="GFTL_General", DisplayName = TranslateText.GetTranslateText("GFTL_General") },
                new Array { ActiveKey="GFTL_Advanced", DisplayName = TranslateText.GetTranslateText("GFTL_Advanced") },
                new Array { ActiveKey="GFTL_Settings", DisplayName = TranslateText.GetTranslateText("GFTL_Settings") }
            };

            TranslateText.OnLanguageChange += RefreshPage;
        }

        public void RefreshPage()
        {
            foreach (var pair in _selectList)
                pair.DisplayName = TranslateText.GetTranslateText(pair.ActiveKey);

            StateHasChanged();
        }
    }
}
