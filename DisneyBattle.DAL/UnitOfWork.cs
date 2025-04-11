using DisneyBattle.DAL.Interfaces;
using DisneyBattle.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyBattle.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DisneyBattleContext _context;

        public UnitOfWork(DisneyBattleContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
            _context.Database.CurrentTransaction?.Commit();
        }

        public void Rollback()
        {
            _context.Database.CurrentTransaction?.Rollback();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public CombatRepository Combats => new CombatRepository(_context);
        public CombatsObjetRepository CombatsObjets => new CombatsObjetRepository(_context);
        public EquipeRepository Equipes => new EquipeRepository(_context);
        public EquipesPersonnageRepository EquipesPersonnages => new EquipesPersonnageRepository(_context);
        public HistoriqueNiveauxRepository HistoriqueNiveaux => new HistoriqueNiveauxRepository(_context);
        public LieuxRepository Lieux => new LieuxRepository(_context);
        public ObjetRepository Objets => new ObjetRepository(_context);
        public PalmaresUtilisateurRepository PalmaresUtilisateurs => new PalmaresUtilisateurRepository(_context);
        public PersonnageRepository Personnages => new PersonnageRepository(_context);
        public PointsFaibleRepository PointsFaibles => new PointsFaibleRepository(_context);
        public TypePersonnageRepository TypesPersonnages => new TypePersonnageRepository(_context);
        public UtilisateurRepository Utilisateurs => new UtilisateurRepository(_context);
    }

}
