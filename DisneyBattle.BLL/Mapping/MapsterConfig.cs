using DisneyBattle.BLL.Models;
using DisneyBattle.DAL.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyBattle.BLL.Mapping
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Utilisateur, UtilisateurModel>.NewConfig()
               .Map(dest => dest.Id, src => src.Id)

               .Map(dest => dest.Pseudo, src => src.Pseudo)

               .Map(dest => dest.Email, src => src.Email)

               .Map(dest => dest.DateInscription, src => src.DateInscription)
               .Map(dest => dest.MotDePasse, src => src.MotDePasse)

               .Map(dest => dest.RefreshToken, src => src.RefreshToken)

               .Map(dest => dest.AccessToken, src => src.AccessToken);

            TypeAdapterConfig<UtilisateurModel, Utilisateur>.NewConfig()
                .IgnoreNullValues(true) 
                .Map(dest => dest.Id, src => src.Id)

                .Map(dest => dest.Pseudo, src => src.Pseudo)

                .Map(dest => dest.Email, src => src.Email)

                .Map(dest => dest.DateInscription, src => src.DateInscription)
                .Map(dest => dest.MotDePasse, src => src.MotDePasse)
               .Map(dest => dest.RefreshToken, src => src.RefreshToken)

               .Map(dest => dest.AccessToken, src => src.AccessToken);
        }
    }
}
