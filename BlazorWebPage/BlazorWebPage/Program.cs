using BlazorWebPage.Client.Shared;
using BlazorWebPage.Components;
using BlazorWebPage.BL.Contracts.Calculator;
using BlazorWebPage.BL.Calculator;
using BlazorWebPage.Client.Services;
using CalculatorService = BlazorWebPage.BL.Calculator.CalculatorService;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


services.AddScoped<ICalculatorService, CalculatorService>()
        .AddScoped<ITokenizer, Tokenizer>()
        .AddScoped<IInterpreter, Interpreter>();

services.AddHttpClient<CatFactService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebPage.Client._Imports).Assembly);

app.Run();
