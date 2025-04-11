using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class Personnage
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public int AlignementId { get; set; }

    public int Niveau { get; set; }

    public int Experience { get; set; }

    public int PointsVie { get; set; }

    public int PointsAttaque { get; set; }

    public int PointsDefense { get; set; }

    public int? LieuId { get; set; }

    public virtual TypePersonnage TypePersonnage { get; set; } = null!;

    public virtual ICollection<CombatsObjet> CombatsObjets { get; set; } = new List<CombatsObjet>();

    public virtual ICollection<EquipesPersonnage> EquipesPersonnages { get; set; } = new List<EquipesPersonnage>();

    public virtual ICollection<HistoriqueNiveaux> HistoriqueNiveauxes { get; set; } = new List<HistoriqueNiveaux>();

    public virtual Lieux? Lieu { get; set; }

    public virtual ICollection<Objet> Objets { get; set; } = new List<Objet>();

    public virtual ICollection<PointsFaible> PointFaibles { get; set; } = new List<PointsFaible>();
}
