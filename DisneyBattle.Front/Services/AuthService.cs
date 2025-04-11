using DisneyBattle.Front.Infrastructure;
using DisneyBattle.Front.Models;
using DisneyBattle.Front.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Diagnostics;
using Microsoft.JSInterop;

namespace DisneyBattle.Front.Services
{
    
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage; 
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavigationManager navigationManager;
        //Pour MSAL
        private readonly IAccessTokenProvider _msalTokenProvider;
        private readonly IJSRuntime jS;
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private bool _isAuthenticating = false;
        private string _apiToken;
        public AuthService(
            HttpClient httpClient,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService localStorage, IHttpClientFactory ClientFactory, IAccessTokenProvider msalTokenProvider, IJSRuntime JS, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _httpClientFactory = ClientFactory;
            _msalTokenProvider = msalTokenProvider;
            jS = JS;
            this.navigationManager = navigationManager;
        }

        public async Task<AuthResult> Login(LoginModel loginModel)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("DisneyBattleApi");
                var response = await httpClient.PostAsJsonAsync("Account/Login", loginModel);
                 AuthResult result = await response.Content.ReadFromJsonAsync<AuthResult>();
                
                if (response.IsSuccessStatusCode)
                {
                    await _localStorage.SetItemAsync("Response", true);
                    await _localStorage.SetItemAsync("authToken", result.Access_Token);
                    await _localStorage.SetItemAsync("refreshToken", result.Refresh_Token);

                    ((CustomAuthStateProvider)_authStateProvider)
                        .NotifyUserAuthentication(result.Access_Token);

                    return result;
                }

                return new AuthResult
                {
                    Success = false,
                    Message = result?.Message ?? "Échec de connexion",
                    Errors = result?.Errors ?? new[] { "Une erreur est survenue lors de la connexion" }
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Une erreur est survenue",
                    Errors = new[] { ex.Message }
                };
            }
        }

        public async Task<AuthResult> Register(RegisterModel registerModel)
        {
            try
            {

                HttpClient httpClient = _httpClientFactory.CreateClient("DisneyBattleApi");
                var response = await httpClient.PostAsJsonAsync("Account/Register", registerModel);
                var result = await response.Content.ReadFromJsonAsync<AuthResult>();

                if (response.IsSuccessStatusCode && result.Success)
                {
                    // Optionnel : connexion automatique après inscription
                    //if (!string.IsNullOrEmpty(result.Access_Token))
                    //{
                    //    await _localStorage.SetItemAsync("authToken", result.Access_Token);
                    //    await _localStorage.SetItemAsync("refreshToken", result.Refresh_Token);

                    //    ((CustomAuthStateProvider)_authStateProvider)
                    //        .NotifyUserAuthentication(result.Access_Token);
                    //}

                    return result;
                }

                return new AuthResult
                {
                    Success = false,
                    Message = result?.Message ?? "Échec de l'inscription",
                    Errors = result?.Errors ?? new[] { "Une erreur est survenue lors de l'inscription" }
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Une erreur est survenue",
                    Errors = new[] { ex.Message }
                };
            }
        }

        public async Task Logout()
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("DisneyBattleApi");
                await httpClient.PostAsync("Account/logout", null);
            }
            catch
            {
                // Même en cas d'échec de l'appel API, on nettoie côté client
            }
            finally
            {
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("refreshToken");
                _httpClient.DefaultRequestHeaders.Authorization = null;
                ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
            }
        }

        public async Task<bool> RefreshToken()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
                    return false;

                var refreshRequest = new
                {
                    Token = token,
                    RefreshToken = refreshToken
                };
                HttpClient httpClient = _httpClientFactory.CreateClient("DisneyBattleApi");
                
                var response = await httpClient.PostAsJsonAsync("Account/Refresh", refreshRequest);

                if (!response.IsSuccessStatusCode)
                    return false;

                var result = await response.Content.ReadFromJsonAsync<AuthResult>();

                if (!result.Success)
                    return false;

                await _localStorage.SetItemAsync("authToken", result.Access_Token);
                await _localStorage.SetItemAsync("refreshToken", result.Refresh_Token);

                ((CustomAuthStateProvider)_authStateProvider)
                    .NotifyUserAuthentication(result.Access_Token);

                return true;
            }
            catch
            {
                return false;
            }
        }


        #region MSAL
        
        public async Task<AuthType> CheckAuthenticationType()
        {
            AccessTokenResult accessTokenResult = await _msalTokenProvider.RequestAccessToken();
            if (accessTokenResult != null)
            {
                if (accessTokenResult.TryGetToken(out AccessToken accessToken))
                {
                    return AuthType.MicrosoftBearer;
                }
                else
                {
                    return AuthType.ApiBearer;
                }

            }
            else
            {
                return AuthType.ApiBearer;
            }
        }
        public async Task HandleMsalAuthentication()
        {
            try
            {
                 await _semaphore.WaitAsync();
                if (_isAuthenticating)
                {
                    //"Authentication already in progress";
                    return;
                }

                _isAuthenticating = true;

                var user = (await _authStateProvider.GetAuthenticationStateAsync()).User;
                if (user.Identity is null || !user.Identity.IsAuthenticated)
                {
                    await jS.InvokeVoidAsync("console.log", "Utilisateur non authentifié !");
                }
                // Obtenir le token MSAL
                AccessTokenResult tokenResult =await _msalTokenProvider.RequestAccessToken(
                        new AccessTokenRequestOptions
                        {
                            ReturnUrl = "/"
                        }).ConfigureAwait(false);

                if (tokenResult.Status != AccessTokenResultStatus.Success)
                {
                    await jS.InvokeVoidAsync("console.log", $"Erreur token: {tokenResult.Status}");
                    return;
                }
                await jS.InvokeVoidAsync("console.log", $"tokenresult : {tokenResult}");
                if (tokenResult.TryGetToken(out var msalToken))
                {

                    await jS.InvokeVoidAsync("console.log", $"MsalToken : {msalToken.Value}");
                    // Appeler votre API pour échanger le token MSAL contre un JWT 
                    AuthResult token = new AuthResult() { Access_Token = msalToken.Value, Refresh_Token="" };
                    HttpClient httpClient = _httpClientFactory.CreateClient("DisneyBattleApi");
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync<AuthResult>("Account/exchange", token);
                    if (response.IsSuccessStatusCode)
                    {
                        AuthResult result = await response.Content.ReadFromJsonAsync<AuthResult>();
                        await _localStorage.SetItemAsync("MicrosoftauthToken", msalToken);  
                        await _localStorage.SetItemAsync("Response", true);
                        await _localStorage.SetItemAsync("authToken", result.Access_Token);
                        await _localStorage.SetItemAsync("refreshToken", result.Refresh_Token);

                        ((CustomAuthStateProvider)_authStateProvider)
                            .NotifyUserAuthentication(result.Access_Token);

                        return;

                    }
                    else
                    {
                        if(response.StatusCode== System.Net.HttpStatusCode.RedirectKeepVerb)
                        {
                            navigationManager.NavigateTo("/RegisterAfterMicrosoftAuthentication", true);
                        }
                    }
                }

                await jS.InvokeVoidAsync("console.log", $"MsalToken : FIN");
            }
            finally
            {
                 
                    _isAuthenticating = false;
                 
            }
        }
        public async Task<string> GetApiToken()
        {
            if (string.IsNullOrEmpty(_apiToken))
            {
                await HandleMsalAuthentication();
            }
            return _apiToken;
        }
        #endregion
    }
}
