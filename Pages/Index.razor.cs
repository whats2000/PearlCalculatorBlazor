using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PearlCalculatorBlazor.Localizer;

namespace PearlCalculatorBlazor.Pages
{
    public partial class Index
    {
        [Inject] IJSRuntime JSRuntime { get; set; }

        private async void OnClickChangeLanguage(string language)
        {
            // Load the selected language
            await TransText.LoadLanguageAsync(language);

            // Store the selected language in localStorage
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "userLanguage", language);
        }

        protected override async void OnInitialized()
        {
            TranslateText.OnLanguageChange += RefreshPage;

            // Check if a language is stored in localStorage
            var storedLanguage = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "userLanguage");

            if (!string.IsNullOrEmpty(storedLanguage))
            {
                // Load the stored language
                await TransText.LoadLanguageAsync(storedLanguage);
            }
        }

        public void RefreshPage()
        {
            StateHasChanged();
        }
    }
}
