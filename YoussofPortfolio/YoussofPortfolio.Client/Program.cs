using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using YoussofPortfolio.Client.Models.Session;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Uri BaseAddress;
if (builder.HostEnvironment.IsDevelopment())
{
    BaseAddress = new Uri("https://localhost:7074");
}
else
{
    BaseAddress = new Uri("https://api.youssofkhawaja.com");
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = BaseAddress });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<SessionStorage>();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 1000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
    config.SnackbarConfiguration.BackgroundBlurred = true;
});

await builder.Build().RunAsync();
