using DisneyBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyBattle.DAL.Repositories
{
    public class PersonnageRepository : BaseRepository<Personnage>
    {
        public PersonnageRepository(DbContext context) : base(context) { }
    }

}
