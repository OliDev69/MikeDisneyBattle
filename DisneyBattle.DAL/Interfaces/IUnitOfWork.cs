using DisneyBattle.DAL.Repositories;

namespace DisneyBattle.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        CombatRepository Combats { get; }
        CombatsObjetRepository CombatsObjets { get; }
        EquipeRepository Equipes { get; }
        EquipesPersonnageRepository EquipesPersonnages { get; }
        HistoriqueNiveauxRepository HistoriqueNiveaux { get; }
        LieuxRepository Lieux { get; }
        ObjetRepository Objets { get; }
        PalmaresUtilisateurRepository PalmaresUtilisateurs { get; }
        PersonnageRepository Personnages { get; }
        PointsFaibleRepository PointsFaibles { get; }
        TypePersonnageRepository TypesPersonnages { get; }
        UtilisateurRepository Utilisateurs { get; }

        void BeginTransaction();
        void Commit();
        void Dispose();
        void Rollback();
    }
}