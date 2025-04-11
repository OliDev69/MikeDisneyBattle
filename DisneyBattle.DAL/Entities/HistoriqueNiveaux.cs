using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class HistoriqueNiveaux
{
    public int Id { get; set; }

    public int PersonnageId { get; set; }

    public int CombatId { get; set; }

    public int AncienNiveau { get; set; }

    public int NouveauNiveau { get; set; }

    public DateTime? DateChangement { get; set; }

    public virtual Combat Combat { get; set; } = null!;

    public virtual Personnage Personnage { get; set; } = null!;
}
