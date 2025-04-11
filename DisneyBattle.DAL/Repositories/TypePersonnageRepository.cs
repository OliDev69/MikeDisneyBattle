using DisneyBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyBattle.DAL.Repositories
{
    public class TypePersonnageRepository : BaseRepository<TypePersonnage>
    {
        public TypePersonnageRepository(DbContext context) : base(context) { }
    }
}
