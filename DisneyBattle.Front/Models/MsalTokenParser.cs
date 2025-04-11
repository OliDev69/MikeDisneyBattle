using System.IdentityModel.Tokens.Jwt;

namespace DisneyBattle.Front.Models
{
    public static class MsalTokenParser
    {
        public static MsalTokenInfo ParseToken(string accessToken, string idToken = null)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenInfo = new MsalTokenInfo
            {
                AccessToken = accessToken,
                IdToken = idToken
            };

            if (handler.CanReadToken(accessToken))
            {
                var jwtToken = handler.ReadJwtToken(accessToken);
                tokenInfo.Claims = jwtToken.Claims;
                tokenInfo.ExpiresOn = jwtToken.ValidTo;

                // Extraction des claims standards
                tokenInfo.TenantId = jwtToken.Claims.FirstOrDefault(c => c.Type == "tid")?.Value;
                tokenInfo.ObjectId = jwtToken.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
                tokenInfo.Scope = jwtToken.Claims.FirstOrDefault(c => c.Type == "scp")?.Value;

                // Si un ID token est fourni, on extrait les informations supplémentaires
                if (!string.IsNullOrEmpty(idToken) && handler.CanReadToken(idToken))
                {
                    var idJwtToken = handler.ReadJwtToken(idToken);
                    tokenInfo.Name = idJwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                    tokenInfo.Email = idJwtToken.Claims.FirstOrDefault(c =>
                        c.Type == "preferred_username" ||
                        c.Type == "upn" ||
                        c.Type == "email")?.Value;
                }
            }

            return tokenInfo;
        }

        public static Dictionary<string, string> GetAllClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var claims = new Dictionary<string, string>();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                foreach (var claim in jwtToken.Claims)
                {
                    claims[claim.Type] = claim.Value;
                }
            }

            return claims;
        }

        public static bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                return DateTime.UtcNow >= jwtToken.ValidTo;
            }
            return true;
        }
    }
}
