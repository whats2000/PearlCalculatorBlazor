using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PearlCalculatorBlazor.Localizer;
using PearlCalculatorLib.Settings;

namespace PearlCalculatorBlazor;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        builder.Services.AddAntDesign();
        builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddScoped(_ => new TranslateText());

        builder.Services.AddSingleton<SettingsManager>();

        var host = builder.Build();

        var trans = host.Services.GetRequiredService<TranslateText>();
        await trans.Init(host.Services.GetRequiredService<HttpClient>());

        await host.RunAsync();
    }
}