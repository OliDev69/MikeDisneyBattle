using DisneyBattle.Front.Models;
using DisneyBattle.Front.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MudBlazor;

namespace DisneyBattle.Front.Components.UI
{
    public partial class RegisterUser
    { 
        private MudForm registerForm; 
        private bool registerSuccess;
        private bool isLoading = false; 
        private RegisterModel registerModel = new();
        [Inject]
        AuthenticationStateProvider authStateProvider { get; set; }

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

         

         
        protected async override Task OnInitializedAsync()
        { 

            var user = (await authStateProvider.GetAuthenticationStateAsync()).User;
            string pseudo = user.Identity.Name;
            registerModel.Pseudo = pseudo;
            string email = user.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            registerModel.Email = email;
            StateHasChanged();
        }


        private string PasswordMatch(string confirmPassword)
        {
            if (confirmPassword != registerModel.MotDePasse)
                return "Les mots de passe ne correspondent pas!";
            return null;
        }

      

        private async Task SubmitRegister()
        {
            await registerForm.Validate();
            if (registerForm.IsValid)
            {
                // Implement your registration logic here
                try
                {
                    isLoading = true;
                    AuthResult ar = await _AuthService.Register(registerModel);
                    Snackbar.Add("Enregistrement réussi", Severity.Success);
                    isLoading = false;
                }
                catch (Exception)
                {
                    Snackbar.Add("Une erreur est survenue lors de la connexion", Severity.Error);
                    isLoading = false;
                }
            }
        }


    }
}
