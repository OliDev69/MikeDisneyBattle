using DisneyBattle.API.Infrastructure;
using DisneyBattle.API.Models;
using DisneyBattle.BLL.Interfaces;
using DisneyBattle.BLL.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DisneyBattle.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly JwtOptions _jwtOption;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(JwtOptions jwtoption, IUserService userService, IMapper mapper )
        {
            _jwtOption = jwtoption;
            _userService = userService;
            this._mapper = mapper;
        }




        // POST: api/<AccountController>/login
        /// <summary>
        /// Get Token for the Disney Battle App
        /// </summary>
        /// <param name="lm">a <see cref="LoginModel"/> with pseudo and password</param>
        /// <returns>an <see cref="JwtResponse"/></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Account/Login		      
        ///
        /// </remarks>
        /// <response code="200">Return a JsonResult with Token And RefreshToken</response>
        /// <response code="404">If Login failed, 404 with <see cref="LoginModel"/> filled with data transmitted </response> 
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]         
        public IActionResult Login(LoginModel lm)
        {
            if (lm == null)
            {
                return new NotFoundResult();
            }
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            UtilisateurModel? u = _userService.Authenticate(lm.Pseudo, lm.Password);
            if (u == null)
            {
                return new BadRequestObjectResult(lm);
            }

            UserDto user = _mapper.Map<UserDto>(u);
            string leTokenArenvoyer = JwtManager.GenerateToken(_jwtOption, user);
             

            //Sauvegarder dans La DB
            _userService.UpdateAsync(_mapper.Map<UtilisateurModel>(user));

            

            return Ok(new JwtResponse { Access_Token = leTokenArenvoyer, Refresh_Token = user.RefreshToken });

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForm lm)
        {
            if (lm == null)
            {
                return new NotFoundResult();
            }
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            try
            {
                //Sauvegarder dans La DB
                await _userService.AddAsync(_mapper.Map<UtilisateurModel>(lm));

                return NoContent();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Impossible de traiter la demande.");
            }

        }
        [HttpPost("logout")]
        [Authorize] 
        public async Task<IActionResult> Logout()
        {
            try
            {
                // Récupérer l'ID de l'utilisateur depuis les claims
                var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Utilisateur non identifié");
                }

                // Récupérer l'utilisateur
                var user = await _userService.GetByIdAsync(int.Parse(userId));
                if (user == null)
                {
                    return NotFound("Utilisateur non trouvé");
                }

                // Invalider le refresh token
                user.RefreshToken = null;
                user.AccessToken = null;

                // Mettre à jour l'utilisateur dans la base de données
                await _userService.UpdateAsync(user);

                return Ok(new { message = "Déconnexion réussie" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur est survenue lors de la déconnexion");
            }
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(JwtResponse tokenResponse)
        {
            List<Claim> mesClaims = null;
            //Récupération des claims du user (idéalement dans une db car normalement l'user n'existe plus si j'ai besoin 
            // de faire un refresh
            if (User != null)
            {
                mesClaims = User.Claims.ToList();

            }
            else
            {
                //rechercher les rôles dans
            }

            //Vérification si le token correspond
            if (_userService.Checkrefresh(tokenResponse.Access_Token, tokenResponse.Refresh_Token))
            {
                string newAccessToken = JwtManager.GenerateAccessTokenFromRefreshToken(tokenResponse.Refresh_Token, _jwtOption, mesClaims);

                JwtResponse response = new JwtResponse
                {
                    Access_Token = newAccessToken,
                    Refresh_Token = JwtManager.GenerateRefreshToken() // Return the same refresh token
                };
                //On doit ici resauvegarder en db les infos (ici ce n'est pas fait car pas de db mais une liste statique)

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }


        }


        /// <summary>
        /// Permet d'échanger le token Ad en token API
        /// </summary>
        /// <returns></returns>
        [HttpPost("exchange")]
        public async Task<IActionResult> ExchangeToken(JwtResponse msal)
        {
            // Validez le token MSAL
            var msalToken = msal.Access_Token;// Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            //Vérification si le token n'a pas expiré
           if(MsalTokenParser.IsTokenExpired(msalToken))
            {
                return new UnauthorizedObjectResult($"Token expiré: {msalToken}");
            }
           
           MsalTokenInfo msinfo = MsalTokenParser.ParseToken(msalToken);
            //Vérification si c'est la bonne application qui a trasnmis le token
            if(msinfo.Claims.FirstOrDefault(c=>c.Type== "app_displayname" && c.Value== "DisneyBattelBlaz")==null)
            {
                return new UnauthorizedObjectResult($"Mauvaise application Azure");
            }
            //Récupération des infos du token pour authentifier le user
            string email = msinfo.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            if(email==null)
            {
                return new UnauthorizedObjectResult($"Pas de mail disponible");
            }

            //Jwt associé
            UtilisateurModel? u = _userService.GetByEmail(email);
            if (u == null)
            {
                return new   StatusCodeResult(307); //BadRequestObjectResult(u);
            }

            UserDto user = _mapper.Map<UserDto>(u);
            string leTokenArenvoyer = JwtManager.GenerateToken(_jwtOption, user);
             

            return Ok(new JwtResponse { Access_Token = leTokenArenvoyer, Refresh_Token = "" });
        }

        

    }
    public class MsalToken
    {
        public string Token { get;set; } 
    }

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
