using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class CombatsObjet
{
    public int CombatId { get; set; }

    public int PersonnageId { get; set; }

    public int ObjetId { get; set; }

    public int Tour { get; set; }

    public virtual Combat Combat { get; set; } = null!;

    public virtual Objet Objet { get; set; } = null!;

    public virtual Personnage Personnage { get; set; } = null!;
}
