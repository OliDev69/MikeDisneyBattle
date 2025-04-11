using DisneyBattle.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyBattle.BLL.Interfaces
{
    public interface IUserService : IService<UtilisateurModel>
    {
        UtilisateurModel? Authenticate (string username, string password);
        bool Checkrefresh(string access_Token, string refresh_Token);
        UtilisateurModel? GetByEmail(string email);
    }
}
