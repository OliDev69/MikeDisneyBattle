using DisneyBattle.Front;
using DisneyBattle.Front.Infrastructure;
using DisneyBattle.Front.Infrastructure.Interfaces;
using DisneyBattle.Front.Models;
using DisneyBattle.Front.Services;
using DisneyBattle.Front.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();
 

// Configuration de l'authentification
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ISessionStorageService, SessionStorageService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configuration MSAL
builder.Services.AddMsalAuthentication(
    options =>
    {
        builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
        options.ProviderOptions.DefaultAccessTokenScopes
            .Add("https://graph.microsoft.com/User.Read");
        options.ProviderOptions.LoginMode = "redirect";        
        options.ProviderOptions.Authentication.PostLogoutRedirectUri = "/"; 
    });


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy("UserOnly", policy =>
       policy.RequireRole("User"));
});

// Configuration HTTP
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<AuthenticationHeaderHandler>();

builder.Services.AddHttpClient<ICustomHttpClient, CustomHttpClient>("DisneyBattleApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7104");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddHttpMessageHandler<AuthenticationHeaderHandler>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.SetMinimumLevel(LogLevel.Debug);
await builder.Build().RunAsync();
