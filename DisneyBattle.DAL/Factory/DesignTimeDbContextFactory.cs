using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyBattle.DAL.Factory
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DisneyBattleContext>
    {
        public DisneyBattleContext CreateDbContext(string[] args)
        {
            // Charger la configuration depuis appSettings.json
             

            // Obtenir la chaîne de connexion
            string connectionString = "Data Source=LENOMIKE\\TFTIC2022;Initial Catalog=DisneyBattle;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

            // Configurer les options du DbContext
            var optionsBuilder = new DbContextOptionsBuilder<DisneyBattleContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DisneyBattleContext(optionsBuilder.Options);
        }
    }
}
