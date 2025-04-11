using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class Combat
{
    public int Id { get; set; }

    public int Equipe1Id { get; set; }

    public int Equipe2Id { get; set; }

    public int? EquipeGagnanteId { get; set; }

    public int ExperienceGagnee { get; set; }

    public DateTime? DateCombat { get; set; }

    public virtual ICollection<CombatsObjet> CombatsObjets { get; set; } = new List<CombatsObjet>();

    public virtual Equipe Equipe1 { get; set; } = null!;

    public virtual Equipe Equipe2 { get; set; } = null!;

    public virtual Equipe? EquipeGagnante { get; set; }

    public virtual ICollection<HistoriqueNiveaux> HistoriqueNiveauxes { get; set; } = new List<HistoriqueNiveaux>();
}
