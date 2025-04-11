using DisneyBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyBattle.DAL.Repositories
{
    public class EquipesPersonnageRepository : BaseRepository<EquipesPersonnage>
    {
        public EquipesPersonnageRepository(DbContext context) : base(context) { }
    }
}
