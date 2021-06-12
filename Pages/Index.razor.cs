using PearlCalculatorBlazor.Localizer;

namespace PearlCalculatorBlazor.Pages
{
    public partial class Index
    {
        private async void OnClickChangeLanguage(string language)
        {
            await TransText.LoadLanguageAsync(language);
        }

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
