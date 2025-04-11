using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class EquipesPersonnage
{
    public int EquipeId { get; set; }

    public int PersonnageId { get; set; }

    public int? Position { get; set; }

    public virtual Equipe Equipe { get; set; } = null!;

    public virtual Personnage Personnage { get; set; } = null!;
}
