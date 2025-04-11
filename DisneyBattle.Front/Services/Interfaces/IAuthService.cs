using DisneyBattle.Front.Models;

namespace DisneyBattle.Front.Services.Interfaces
{
    public enum AuthType
    {
        ApiBearer,
        MicrosoftBearer
    }
    public interface IAuthService
    {
        Task<AuthResult> Login(LoginModel loginModel);
        Task<AuthResult> Register(RegisterModel registerModel);
        Task Logout();
        Task<bool> RefreshToken();

        /*Pour MSAL*/
        Task<string> GetApiToken();
        Task HandleMsalAuthentication();
        Task<AuthType> CheckAuthenticationType();
    }
}
