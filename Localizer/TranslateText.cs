
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PearlCalculatorBlazor.Localizer
{
    public class TranslateText
    {
        private static string Language = "en";

        public static string GetLanguage() 
        {
            return Language;
        }

        private static Dictionary<string, string> ToDictionary(string jsonSrting)
        {
            var jsonObject = JObject.Parse(jsonSrting);
            var jTokens = jsonObject.Descendants().Where(p => !p.Any());
            var tmpKeys = jTokens.Aggregate(new Dictionary<string, string>(),
                (properties, jToken) =>
                {
                    properties.Add(jToken.Path, jToken.ToString());
                    return properties;
                });
            return tmpKeys;
        }

        private static async void ReadLanguageJson()
        {
            Dictionary<string, string> Text = new Dictionary<string, string>();

            var http = new HttpClient();

            await http.GetStringAsync("http://localhost:5001/resources/assets/i18n/" + Language + ".json");

            Text = ToDictionary(System.IO.File.ReadAllText("http://localhost:5001/resources/assets/i18n/" + Language + ".json"));

            foreach (KeyValuePair<string, string> item in Text)
            {
                Console.WriteLine("Key: {0}, Value: {1}",
                item.Key, item.Value);
            }
        }

        public static void ChnageLanguage(string language) 
        {
            Language = language;
            ReadLanguageJson();
        }
    }
}