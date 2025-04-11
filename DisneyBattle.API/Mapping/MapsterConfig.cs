using DisneyBattle.API.Models;
using DisneyBattle.BLL.Models;
using DisneyBattle.DAL.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyBattle.API.Mapping
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<UserDto, UtilisateurModel>.NewConfig()
               .Map(dest => dest.Id, src => src.Id)

               .Map(dest => dest.Pseudo, src => src.Username)
               .Map(dest => dest.MotDePasse, src => src.Password)
               .Map(dest => dest.RefreshToken, src => src.RefreshToken)

               .Map(dest => dest.AccessToken, src => src.AccessToken);

            TypeAdapterConfig<UtilisateurModel, UserDto>.NewConfig()

                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Username, src => src.Pseudo)
               .Map(dest => dest.RefreshToken, src => src.RefreshToken)

               .Map(dest => dest.AccessToken, src => src.AccessToken);
            
            TypeAdapterConfig<UserForm, UtilisateurModel>.NewConfig()
                 
                .Map(dest => dest.Pseudo, src => src.Pseudo)
                .Map(dest => dest.Email, src=>src.Email)
                .Map(dest=> dest.MotDePasse, src=>src.MotDePasse);

        }
    }
}
