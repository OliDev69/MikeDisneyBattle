using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace DisneyBattle.Front.Services.Interfaces
{
    public interface ISessionStorageService
    {
        Task<string> GetAccessTokenAsync();
        Task SetAccessTokenAsync(AccessToken token);
    }
}