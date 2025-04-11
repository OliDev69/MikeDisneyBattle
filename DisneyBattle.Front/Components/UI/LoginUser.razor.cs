using DisneyBattle.Front.Models;
using DisneyBattle.Front.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace DisneyBattle.Front.Components.UI
{
    public partial class LoginUser
    {
        private MudForm loginForm;
        private bool loginSuccess;
        private bool isLoading = false;
        private LoginModel loginModel = new();

        [Inject]
        ISnackbar Snackbar { get; set; }

        [Inject]
        IAuthService _AuthService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "returnUrl")]
        public string ReturnUrl { get; set; }



        private async Task SubmitLogin()
        {
            try
            {
                isLoading = true;
                await loginForm.Validate();
                if (loginSuccess)
                {
                    try
                    {
                        AuthResult ar = await _AuthService.Login(loginModel);
                        if (string.IsNullOrEmpty(ReturnUrl))
                            NavigationManager.NavigateTo("/");
                        else
                            NavigationManager.NavigateTo(ReturnUrl);


                    }
                    catch (Exception ex)
                    {
                        Snackbar.Add("Une erreur est survenue lors de la connexion", Severity.Error);
                    }
                    finally
                    {
                        isLoading = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Une erreur est survenue lors de la connexion", Severity.Error);
                isLoading = false;
            }
        }
        private void LoginWithAzure()
        {
            NavigationManager.NavigateTo("/authentication/Login?returnUrl=/Profile");

        }
    }
}
