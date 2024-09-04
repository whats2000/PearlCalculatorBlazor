using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PearlCalculatorBlazor.Localizer;

public class TranslateText
{
    private const string FallbackLanguage = "en";

    public static readonly Dictionary<string, string> LanguageDict = new()
    {
        { "en", "English" },
        { "es_ES", "Español" },
        { "zh_cn", "简体中文" },
        { "zh_tw", "繁體中文" }
    };

    private static HttpClient _httpClient;

    private static Dictionary<string, string> _displayText = new();
    private static Dictionary<string, string> _fallbackDisplayText = new();
    private string _currentLanguage = FallbackLanguage;

    public static event Action OnLanguageChange;

    public async Task Init(HttpClient httpClient)
    {
        if (httpClient.BaseAddress != null)
        {
            var jsonString =
                await httpClient.GetStringAsync(
                    $"{httpClient.BaseAddress.AbsoluteUri}assets/i18n/{FallbackLanguage}.json");
            _fallbackDisplayText = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
        }

        _httpClient = httpClient;
    }

    public async Task LoadLanguageAsync(string language)
    {
        if (language == _currentLanguage)
            return;

        if (_httpClient.BaseAddress != null)
        {
            var jsonString =
                await _httpClient.GetStringAsync($"{_httpClient.BaseAddress.AbsoluteUri}assets/i18n/{language}.json");
            _displayText = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
        }

        _currentLanguage = language;

        OnLanguageChange?.Invoke();
    }

    public static string GetTranslateText(string key)
    {
        return _displayText.TryGetValue(key, out var translateText)
            ? translateText
            : _fallbackDisplayText.GetValueOrDefault(key, key);
    }
}