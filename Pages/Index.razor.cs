using PearlCalculatorBlazor.Localizer;

namespace PearlCalculatorBlazor.Pages
{
    public partial class Index
    {
        private void OnClickChangeLanguage(string language)
        {
            Managers.EventManager.Instance.PublishEvent(this, "SetLanguage", new SetLanguageArgs("Index", language));
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
