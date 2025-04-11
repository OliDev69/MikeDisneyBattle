using DisneyBattle.Front.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;

namespace DisneyBattle.Front.Layout
{
    public partial class NavMenu : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        IAuthService AuthService { get; set; }

        private string userName;
        private AuthType typeAuth;
        protected override async Task OnInitializedAsync()
        {
           typeAuth= await AuthService.CheckAuthenticationType();

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if(authState.User.Identity.IsAuthenticated)
            {                 
                userName=authState.User.Claims.FirstOrDefault(m => m.Type == "preferred_username").Value;
            }
            else
            {
                userName = "Nobody";
            }
        }

        

       

        
    }
}
