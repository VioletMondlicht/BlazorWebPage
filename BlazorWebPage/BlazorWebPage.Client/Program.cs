using BlazorWebPage.Client;
using BlazorWebPage.Client.Services;
using BlazorWebPage.Client.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
var services = builder.Services;

// Register HttpClient the simple way
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register your service
services.AddScoped<CatFactService>()
        .AddScoped<ICalculatorService, CalculatorService>();

await builder.Build().RunAsync();