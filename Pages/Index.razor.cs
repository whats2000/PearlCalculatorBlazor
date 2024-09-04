using PearlCalculatorBlazor.Localizer;

namespace PearlCalculatorBlazor.Pages;

public partial class Index
{
    protected override void OnInitialized()
    {
        TranslateText.OnLanguageChange += RefreshPage;
    }

    private void RefreshPage()
    {
        StateHasChanged();
    }
}