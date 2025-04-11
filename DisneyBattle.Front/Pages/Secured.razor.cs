using DisneyBattle.Front.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace DisneyBattle.Front.Pages
{

    public partial class Secured
    {
        [Inject]
        IAuthService _AuthService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        
        AuthType typeAuth { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            typeAuth = await _AuthService.CheckAuthenticationType();
        }

        private async Task Logout()
        {
            await _AuthService.Logout();
            NavigationManager.NavigateToLogout("/login");
        }

        private async Task LogoutWithAzure()
        {
            await _AuthService.Logout();
            NavigationManager.NavigateToLogout("authentication/logout");
        }
    }
}
