using DisneyBattle.DAL.Entities;
using BCrypt.Net; 
using Microsoft.EntityFrameworkCore; 
namespace DisneyBattle.DAL.Repositories
{
    public class UtilisateurRepository : BaseRepository<UtilisateurModel>
    {
        public UtilisateurRepository(DbContext context) : base(context) { }

        public override async Task AddAsync(UtilisateurModel entity)
        {
            entity.DateInscription = DateTime.Now;
            entity.MotDePasse =BCrypt.Net.BCrypt.HashPassword(entity.MotDePasse);
            await base.AddAsync(entity);
        }

        public override Task UpdateAsync(UtilisateurModel user)
        { 
            UtilisateurModel? entity = _dbSet.SingleOrDefault(u=>u.Id == user.Id);
            if (entity == null) { throw new InvalidDataException("No user to update"); } 
            entity.Pseudo= user.Pseudo;
            entity.Email= user.Email??entity.Email;
            entity.RefreshToken = user.RefreshToken;
            entity.AccessToken= user.AccessToken;
            entity.MotDePasse = entity.MotDePasse;
            return base.UpdateAsync(entity);
        }
        public UtilisateurModel? Authenticate(string username, string password)
        {
            UtilisateurModel? user = _context.Utilisateurs.SingleOrDefault(x => x.Pseudo == username);

            if (user == null)
            {
                return null; // L'utilisateur n'existe pas
            }

            // Vérification du mot de passe avec BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.MotDePasse);

            return isPasswordValid ? user : null;
        }
        public bool Checkrefresh(string access_Token, string refresh_Token)
        {
            var user = _context.Utilisateurs.FirstOrDefault(uu => uu.RefreshToken == refresh_Token && uu.AccessToken == access_Token);

            return user != null;
        }

        public UtilisateurModel? GetByEmail(string email)
        {
            UtilisateurModel? user = _context.Utilisateurs.SingleOrDefault(x => x.Email == email);

            return user;
        }
    }
        
}
