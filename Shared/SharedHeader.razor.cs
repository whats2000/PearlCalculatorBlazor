using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PearlCalculatorBlazor.Localizer;
using AntDesign;
    
namespace PearlCalculatorBlazor.Shared
{
    public enum Theme
    {
        Light,
        Dark
    }

    public partial class SharedHeader : ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        private Theme _currentTheme = Theme.Light;
        
        private RenderFragment LightThemeIcon => builder =>
        {
            builder.OpenComponent<Icon>(0);
            builder.AddAttribute(1, "Component", (RenderFragment)(builder2 =>
            {
                builder2.AddMarkupContent(0, @"
                <svg width=""14px"" height=""15.2px"" viewBox=""0 0 32 32"" xmlns=""http://www.w3.org/2000/svg"">
                    <g id=""Lager_94"" data-name=""Lager 94"" transform=""translate(0)"">
                        <path id=""Path_70"" data-name=""Path 70"" d=""M12.516,4.509A12,12,0,0,0,22.3,19.881,12.317,12.317,0,0,0,24,20a11.984,11.984,0,0,0,3.49-.514,12.1,12.1,0,0,1-9.963,8.421A12.679,12.679,0,0,1,16,28,12,12,0,0,1,12.516,4.509M16,0a16.5,16.5,0,0,0-2.212.15A16,16,0,0,0,16,32a16.526,16.526,0,0,0,2.01-.123A16.04,16.04,0,0,0,31.85,18.212,16.516,16.516,0,0,0,32,15.944,1.957,1.957,0,0,0,30,14a2.046,2.046,0,0,0-1.23.413A7.942,7.942,0,0,1,24,16a8.35,8.35,0,0,1-1.15-.08,7.995,7.995,0,0,1-5.264-12.7A2.064,2.064,0,0,0,16.056,0Z"" fill=""#040505""/>
                    </g>
                </svg>");
            }));
            builder.AddAttribute(2, "Style", "font-size: 32px;");
            builder.CloseComponent();
        };

        private RenderFragment DarkThemeIcon => builder =>
        {
            builder.OpenComponent<Icon>(0);
            builder.AddAttribute(1, "Component", (RenderFragment)(builder2 =>
            {
                builder2.AddMarkupContent(0,@"
                <svg width=""14px"" height=""15.2px"" viewBox=""0 0 36 36"" xmlns=""http://www.w3.org/2000/svg"">
                    <g id=""Lager_93"" data-name=""Lager 93"" transform=""translate(2 2)"">
                        <g id=""Sun_3_Brightness_3"" data-name=""Sun 3, Brightness 3"">
                            <path id=""Path_68"" data-name=""Path 68"" d=""M32,14H27.033c-2,1.769-.779,4,.967,4h4.967C34.966,16.231,33.746,14,32,14Z"" fill=""#040505""/>
                            <g id=""Path_69"" data-name=""Path 69"" fill=""none"" stroke-miterlimit=""10"">
                                <path d=""M17.172,10.111a6,6,0,1,0,4.715,4.715A6.01,6.01,0,0,0,17.172,10.111Z"" stroke=""none""/>
                                <path d=""M 15.99852275848389 13.99979972839355 C 15.40029907226563 13.99979972839355 14.83786392211914 14.26465797424316 14.45541763305664 14.72645950317383 C 14.18128776550293 15.05748176574707 13.88667678833008 15.62165832519531 14.04035758972168 16.43178939819336 C 14.1787109375 17.16349411010742 14.83581733703613 17.82003402709961 15.56771087646484 17.958740234375 C 15.71307563781738 17.98624801635742 15.85765266418457 18.00020027160645 15.99740505218506 18.00020027160645 C 16.59555816650391 18.00020027160645 17.15798187255859 17.73542404174805 17.54046440124512 17.27376556396484 C 17.81481742858887 16.94261169433594 18.1097583770752 16.37818908691406 17.95689964294434 15.57052993774414 C 17.81802749633789 14.83748245239258 17.1605224609375 14.17996406555176 16.42829895019531 14.041259765625 C 16.28293609619141 14.01375389099121 16.13835906982422 13.99979972839355 15.99860572814941 13.99979972839355 L 15.99852275848389 13.99979972839355 M 15.99860000610352 9.999795913696289 C 16.38235282897949 9.999801635742188 16.77459716796875 10.03580474853516 17.17200469970703 10.11100006103516 C 19.52100563049316 10.55599975585938 21.44199371337891 12.47699928283691 21.88699340820313 14.82600021362305 C 22.61180877685547 18.65568542480469 19.69624137878418 22.00020408630371 15.99740028381348 22.00020408630371 C 15.61366271972656 22.00020408630371 15.22141265869141 21.96419525146484 14.82400512695313 21.88899993896484 C 12.47600555419922 21.44400024414063 10.55400466918945 19.52299880981445 10.11000442504883 17.17499923706055 C 9.383377075195313 13.34440803527832 12.29961967468262 9.999755859375 15.99860000610352 9.999795913696289 Z"" stroke=""none"" fill=""#040505""/>
                            </g>
                            <rect id=""Rectangle_26"" data-name=""Rectangle 26"" width=""8"" height=""4"" rx=""1.993"" transform=""translate(26 14)"" fill=""#040505""/>
                            <rect id=""Rectangle_27"" data-name=""Rectangle 27"" width=""8"" height=""4"" rx=""1.993"" transform=""translate(18 26) rotate(90)"" fill=""#040505""/>
                            <rect id=""Rectangle_28"" data-name=""Rectangle 28"" width=""8"" height=""4"" rx=""1.993"" transform=""translate(18 -2) rotate(90)"" fill=""#040505""/>
                            <rect id=""Rectangle_29"" data-name=""Rectangle 29"" width=""8"" height=""4"" rx=""1.993"" transform=""translate(-2 14)"" fill=""#040505""/>
                            <g id=""Group_22"" data-name=""Group 22"">
                                <rect id=""Rectangle_30"" data-name=""Rectangle 30"" width=""6.925"" height=""3.766"" rx=""1.883"" transform=""translate(23.22 6.117) rotate(-45)"" fill=""#040505""/>
                            </g>
                            <g id=""Group_23"" data-name=""Group 23"">
                                <rect id=""Rectangle_31"" data-name=""Rectangle 31"" width=""3.766"" height=""6.925"" rx=""1.883"" transform=""matrix(0.707, -0.707, 0.707, 0.707, 23.22, 25.883)"" fill=""#040505""/>
                            </g>
                            <g id=""Group_24"" data-name=""Group 24"">
                                <rect id=""Rectangle_32"" data-name=""Rectangle 32"" width=""3.766"" height=""6.925"" rx=""1.883"" transform=""translate(1.22 3.883) rotate(-45)"" fill=""#040505""/>
                            </g>
                            <g id=""Group_25"" data-name=""Group 25"">
                                <rect id=""Rectangle_33"" data-name=""Rectangle 33"" width=""6.925"" height=""3.766"" rx=""1.883"" transform=""translate(1.22 28.117) rotate(-45)"" fill=""#040505""/>
                            </g>
                        </g>
                    </g>
                </svg>");
            }));
            builder.AddAttribute(2, "Style", "font-size: 32px;");
            builder.CloseComponent();
        };

        protected Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    UpdateTheme();
                }
            }
        }
        
        private RenderFragment GetThemeIcon() => _currentTheme switch
        {
            Theme.Light => LightThemeIcon,
            Theme.Dark => DarkThemeIcon,
            _ => LightThemeIcon,
        };

        private void ToggleTheme()
        {
            CurrentTheme = _currentTheme == Theme.Light ? Theme.Dark : Theme.Light;
        }

        private async void UpdateTheme()
        {
            // Alert message Soon™
            await Notice.Open(new NotificationConfig()
            {
                Message = "Notification",
                Description = "This feature is coming soon™",
                NotificationType = NotificationType.Info
            });
        }

        private async void OnClickChangeLanguage(string language)
        {
            // Load the selected language
            await TransText.LoadLanguageAsync(language);

            // Store the selected language in localStorage
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "PearlCalculatorBlazor_userLanguage", language);
        }

        protected override async void OnInitialized()
        {
            TranslateText.OnLanguageChange += RefreshPage;

            // Check if a language is stored in localStorage
            var storedLanguage = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "PearlCalculatorBlazor_userLanguage");

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