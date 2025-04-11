using System.Security.Claims;

namespace DisneyBattle.Front.Models
{

    public class MsalTokenInfo
    {
        public string AccessToken { get; set; }
        public string IdToken { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string Scope { get; set; }
        public string TenantId { get; set; }
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
