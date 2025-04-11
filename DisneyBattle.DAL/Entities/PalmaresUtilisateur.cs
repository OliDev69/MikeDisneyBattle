using System;
using System.Collections.Generic;

namespace DisneyBattle.DAL.Entities;

public partial class PalmaresUtilisateur
{
    public int Id { get; set; }

    public string Pseudo { get; set; } = null!;

    public int? TotalCombats { get; set; }

    public int? Victoires { get; set; }

    public int? Defaites { get; set; }

    public decimal? PourcentageVictoire { get; set; }
}
