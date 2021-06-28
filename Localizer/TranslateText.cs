using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace PearlCalculatorBlazor.Localizer
{
    public class TranslateText
    {
        public static readonly Dictionary<string, string> LanguageDict = new Dictionary<string, string> 
        { 
            { "en", "English" }, 
            { "es_ES", "Español" },
            { "zh_cn", "简体中文" }, 
            { "zh_tw", "繁體中文" }          
        };

        private const string FallbackLanguage = "en";       
        private string _currentLanguage = FallbackLanguage;

        private static HttpClient _httpClient;

        private static Dictionary<string, string> _displayText = new();
        private static Dictionary<string, string> _fallbackDisplayText = new();
        
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
            else if (_fallbackDisplayText.ContainsKey(key))
                return _fallbackDisplayText[key];
            else
                return string.Empty;
        }
    }
}