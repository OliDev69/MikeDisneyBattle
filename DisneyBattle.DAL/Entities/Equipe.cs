using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class Equipe
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public int UtilisateurId { get; set; }

    public DateTime? DateCreation { get; set; }

    public virtual ICollection<Combat> CombatEquipe1s { get; set; } = new List<Combat>();

    public virtual ICollection<Combat> CombatEquipe2s { get; set; } = new List<Combat>();

    public virtual ICollection<Combat> CombatEquipeGagnantes { get; set; } = new List<Combat>();

    public virtual ICollection<EquipesPersonnage> EquipesPersonnages { get; set; } = new List<EquipesPersonnage>();

    public virtual UtilisateurModel Utilisateur { get; set; } = null!;
}
