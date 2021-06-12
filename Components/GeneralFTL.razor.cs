using PearlCalculatorBlazor.Localizer;

namespace PearlCalculatorBlazor.Components
{
    public partial class GeneralFTL
    {
        protected override void OnInitialized()
        {
            TranslateText.OnLanguageChange += RefreshPage;
        }

        public void RefreshPage()
        {
            StateHasChanged();
        }
    }
}
