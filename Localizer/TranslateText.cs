using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace PearlCalculatorBlazor.Localizer
{
    public class TranslateText
    {
        private const string FallbackLanguage = "en";       
        private string _currentLanguage = string.Empty;

        private static HttpClient _httpClient = new HttpClient();

        private static Dictionary<string, string> _displayText = new Dictionary<string, string>();
        private static Dictionary<string, string> _fallbackDisplayText = new Dictionary<string, string>();
        
        public static event Action OnLanguageChange;

        public async Task Init(HttpClient httpClient) 
        {
            var jsonString = await httpClient.GetStringAsync($"{httpClient.BaseAddress.AbsoluteUri}assets/i18n/{FallbackLanguage}.json");
            _fallbackDisplayText = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            _httpClient = httpClient;
        }

        public async Task LoadLanguageAsync(string language)
        {
            if (language == _currentLanguage)
                return;

            var jsonString = await _httpClient.GetStringAsync($"{_httpClient.BaseAddress.AbsoluteUri}assets/i18n/{language}.json");
            _displayText = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            _currentLanguage = language;

            OnLanguageChange();
        }

        public static string GetTranslateText(string key)
        {
            if (_displayText.ContainsKey(key))
                return _displayText[key];
            else
                return _fallbackDisplayText[key];
        }
    }
}