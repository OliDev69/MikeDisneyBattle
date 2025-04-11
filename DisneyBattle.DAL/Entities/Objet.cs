using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class Objet
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public int NiveauRequis { get; set; }

    public int? BonusPv { get; set; }

    public int? BonusAttaque { get; set; }

    public int? BonusDefense { get; set; }

    public virtual ICollection<CombatsObjet> CombatsObjets { get; set; } = new List<CombatsObjet>();

    public virtual ICollection<Personnage> Personnages { get; set; } = new List<Personnage>();
}
