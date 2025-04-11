using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class TypePersonnage
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public virtual ICollection<Personnage> Personnages { get; set; } = new List<Personnage>();
}
