using Blazored.LocalStorage;
using MudBlazor;
using MudBlazor.Services;
using YoussofPortfolio.Client.Models.Session;
using YoussofPortfolio.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

Uri BaseAddress;
if (builder.Environment.IsDevelopment())
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

app.UseStatusCodePagesWithRedirects("/NotFound");
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(YoussofPortfolio.Client._Imports).Assembly);


app.Run();
