
using DisneyBattle.Front.Services.Interfaces;
using DisneyBattle.Front.Services;
using Microsoft.AspNetCore.Components;
using static DisneyBattle.Front.Services.AuthService;

namespace DisneyBattle.Front.Components.UI
{
    
    public partial class JwtDisplay
    {
        private Dictionary<string, string>? claims;
        private string token { get; set; }
        [Inject]
        IJwtService JwtService { get; set; }
        [Inject]
        ILocalStorageService LocalStorage { get; set; }
        [Inject]
        IAuthService AuthService { get; set; }
        [Inject]
        ISessionStorageService SessionStorage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if((await AuthService.CheckAuthenticationType())==   AuthType.MicrosoftBearer)
            {
                await AuthService.HandleMsalAuthentication();
                
            }
            string jwtToken = await LocalStorage.GetItemAsync<string>("authToken");

            claims = JwtService.GetClaims(jwtToken);




        }
    }
}
