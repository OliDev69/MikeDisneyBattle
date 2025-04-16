using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class UtilisateurModel
{
    public int Id { get; set; }

    public string Pseudo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public DateTime? DateInscription { get; set; }

    public string? RefreshToken { get; set; }
    public string? AccessToken { get; set; }

    public virtual ICollection<Equipe> Equipes { get; set; } = new List<Equipe>();
}
