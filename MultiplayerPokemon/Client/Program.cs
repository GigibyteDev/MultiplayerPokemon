using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MultiplayerPokemon.Client;
using MultiplayerPokemon.Client.Settings;
using MultiplayerPokemon.Client.AuthState;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

AppSettings.RoomHubName = builder.HostEnvironment.BaseAddress + builder.Configuration.GetSection("AppSettings:RoomHubName").Value;


builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly)
    .UseReduxDevTools();
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

await builder.Build().RunAsync();
