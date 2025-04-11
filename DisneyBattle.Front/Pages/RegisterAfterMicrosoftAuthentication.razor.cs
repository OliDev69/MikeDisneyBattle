using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace DisneyBattle.Front.Pages
{
    [Authorize]
    public partial class RegisterAfterMicrosoftAuthentication
    {
        [Inject]
        AuthenticationStateProvider authStateProvider { get; set; }
        [Inject]
        IAccessTokenProvider msalTokenProvider { get; set; }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            var user = (await authStateProvider.GetAuthenticationStateAsync()).User;

        }
    }
}
