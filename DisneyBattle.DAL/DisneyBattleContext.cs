using System;
using System.Collections.Generic;
using DisneyBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DisneyBattle.DAL;

public partial class DisneyBattleContext : DbContext
{
    public DisneyBattleContext()
    {
    }

    public DisneyBattleContext(DbContextOptions<DisneyBattleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Combat> Combats { get; set; }

    public virtual DbSet<CombatsObjet> CombatsObjets { get; set; }

    public virtual DbSet<Equipe> Equipes { get; set; }

    public virtual DbSet<EquipesPersonnage> EquipesPersonnages { get; set; }

    public virtual DbSet<HistoriqueNiveaux> HistoriqueNiveauxes { get; set; }

    public virtual DbSet<Lieux> Lieuxes { get; set; }

    public virtual DbSet<Objet> Objets { get; set; }

    public virtual DbSet<PalmaresUtilisateur> PalmaresUtilisateurs { get; set; }

    public virtual DbSet<Personnage> Personnages { get; set; }

    public virtual DbSet<PointsFaible> PointsFaibles { get; set; }

    public virtual DbSet<TypePersonnage> TypePersonnages { get; set; }

    public virtual DbSet<UtilisateurModel> Utilisateurs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 
    //    => optionsBuilder.UseSqlServer("Data Source=LENOMIKE\\TFTIC2022;Initial Catalog=DisneyBattle;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__combats__3213E83FD3B119FD");

            entity.ToTable("combats", tb => tb.HasTrigger("TR_after_combat_update"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCombat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_combat");
            entity.Property(e => e.Equipe1Id).HasColumnName("equipe1_id");
            entity.Property(e => e.Equipe2Id).HasColumnName("equipe2_id");
            entity.Property(e => e.EquipeGagnanteId).HasColumnName("equipe_gagnante_id");
            entity.Property(e => e.ExperienceGagnee)
                .HasDefaultValue(100)
                .HasColumnName("experience_gagnee");

            entity.HasOne(d => d.Equipe1).WithMany(p => p.CombatEquipe1s)
                .HasForeignKey(d => d.Equipe1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_combats_equipe1");

            entity.HasOne(d => d.Equipe2).WithMany(p => p.CombatEquipe2s)
                .HasForeignKey(d => d.Equipe2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_combats_equipe2");

            entity.HasOne(d => d.EquipeGagnante).WithMany(p => p.CombatEquipeGagnantes)
                .HasForeignKey(d => d.EquipeGagnanteId)
                .HasConstraintName("FK_combats_gagnant");
        });

        modelBuilder.Entity<CombatsObjet>(entity =>
        {
            entity.HasKey(e => new { e.CombatId, e.PersonnageId, e.ObjetId, e.Tour });

            entity.ToTable("combats_objets");

            entity.Property(e => e.CombatId).HasColumnName("combat_id");
            entity.Property(e => e.PersonnageId).HasColumnName("personnage_id");
            entity.Property(e => e.ObjetId).HasColumnName("objet_id");
            entity.Property(e => e.Tour).HasColumnName("tour");

            entity.HasOne(d => d.Combat).WithMany(p => p.CombatsObjets)
                .HasForeignKey(d => d.CombatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_combats_objets_combats");

            entity.HasOne(d => d.Objet).WithMany(p => p.CombatsObjets)
                .HasForeignKey(d => d.ObjetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_combats_objets_objets");

            entity.HasOne(d => d.Personnage).WithMany(p => p.CombatsObjets)
                .HasForeignKey(d => d.PersonnageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_combats_objets_personnages");
        });

        modelBuilder.Entity<Equipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__equipes__3213E83F09C59F4A");

            entity.ToTable("equipes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_creation");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.UtilisateurId).HasColumnName("utilisateur_id");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.Equipes)
                .HasForeignKey(d => d.UtilisateurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_equipes_utilisateurs");
        });

        modelBuilder.Entity<EquipesPersonnage>(entity =>
        {
            entity.HasKey(e => new { e.EquipeId, e.PersonnageId });

            entity.ToTable("equipes_personnages");

            entity.Property(e => e.EquipeId).HasColumnName("equipe_id");
            entity.Property(e => e.PersonnageId).HasColumnName("personnage_id");
            entity.Property(e => e.Position).HasColumnName("position");

            entity.HasOne(d => d.Equipe).WithMany(p => p.EquipesPersonnages)
                .HasForeignKey(d => d.EquipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_equipes_personnages_equipes");

            entity.HasOne(d => d.Personnage).WithMany(p => p.EquipesPersonnages)
                .HasForeignKey(d => d.PersonnageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_equipes_personnages_personnages");
        });

        modelBuilder.Entity<HistoriqueNiveaux>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__historiq__3213E83F0D8A910C");

            entity.ToTable("historique_niveaux");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AncienNiveau).HasColumnName("ancien_niveau");
            entity.Property(e => e.CombatId).HasColumnName("combat_id");
            entity.Property(e => e.DateChangement)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_changement");
            entity.Property(e => e.NouveauNiveau).HasColumnName("nouveau_niveau");
            entity.Property(e => e.PersonnageId).HasColumnName("personnage_id");

            entity.HasOne(d => d.Combat).WithMany(p => p.HistoriqueNiveauxes)
                .HasForeignKey(d => d.CombatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_historique_combats");

            entity.HasOne(d => d.Personnage).WithMany(p => p.HistoriqueNiveauxes)
                .HasForeignKey(d => d.PersonnageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_historique_personnages");
        });

        modelBuilder.Entity<Lieux>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__lieux__3213E83FE8421A32");

            entity.ToTable("lieux");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Objet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__objets__3213E83F399A5385");

            entity.ToTable("objets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BonusAttaque)
                .HasDefaultValue(0)
                .HasColumnName("bonus_attaque");
            entity.Property(e => e.BonusDefense)
                .HasDefaultValue(0)
                .HasColumnName("bonus_defense");
            entity.Property(e => e.BonusPv)
                .HasDefaultValue(0)
                .HasColumnName("bonus_pv");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.NiveauRequis)
                .HasDefaultValue(1)
                .HasColumnName("niveau_requis");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<PalmaresUtilisateur>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("palmares_utilisateurs");

            entity.Property(e => e.Defaites).HasColumnName("defaites");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PourcentageVictoire)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("pourcentage_victoire");
            entity.Property(e => e.Pseudo)
                .HasMaxLength(50)
                .HasColumnName("pseudo");
            entity.Property(e => e.TotalCombats).HasColumnName("total_combats");
            entity.Property(e => e.Victoires).HasColumnName("victoires");
        });

        modelBuilder.Entity<Personnage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personna__3213E83FBE136F56");

            entity.ToTable("personnages");

            entity.HasIndex(e => e.Nom, "UQ__personna__DF90DC2C89D959C4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlignementId).HasColumnName("alignement_id");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.LieuId).HasColumnName("lieu_id");
            entity.Property(e => e.Niveau)
                .HasDefaultValue(1)
                .HasColumnName("niveau");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.PointsAttaque).HasColumnName("points_attaque");
            entity.Property(e => e.PointsDefense).HasColumnName("points_defense");
            entity.Property(e => e.PointsVie).HasColumnName("points_vie");

            entity.HasOne(d => d.TypePersonnage).WithMany(p => p.Personnages)
                .HasForeignKey(d => d.AlignementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnages_alignements");

            entity.HasOne(d => d.Lieu).WithMany(p => p.Personnages)
                .HasForeignKey(d => d.LieuId)
                .HasConstraintName("FK_personnages_lieux");

            entity.HasMany(d => d.Objets).WithMany(p => p.Personnages)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonnagesObjetsAutorise",
                    r => r.HasOne<Objet>().WithMany()
                        .HasForeignKey("ObjetId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_objets_autorises_objets"),
                    l => l.HasOne<Personnage>().WithMany()
                        .HasForeignKey("PersonnageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_objets_autorises_personnages"),
                    j =>
                    {
                        j.HasKey("PersonnageId", "ObjetId").HasName("PK_personnages_objets");
                        j.ToTable("personnages_objets_autorises");
                        j.IndexerProperty<int>("PersonnageId").HasColumnName("personnage_id");
                        j.IndexerProperty<int>("ObjetId").HasColumnName("objet_id");
                    });

            entity.HasMany(d => d.PointFaibles).WithMany(p => p.Personnages)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonnagesPointsFaible",
                    r => r.HasOne<PointsFaible>().WithMany()
                        .HasForeignKey("PointFaibleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_points_faibles_points"),
                    l => l.HasOne<Personnage>().WithMany()
                        .HasForeignKey("PersonnageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_points_faibles_personnages"),
                    j =>
                    {
                        j.HasKey("PersonnageId", "PointFaibleId");
                        j.ToTable("personnages_points_faibles");
                        j.IndexerProperty<int>("PersonnageId").HasColumnName("personnage_id");
                        j.IndexerProperty<int>("PointFaibleId").HasColumnName("point_faible_id");
                    });
        });

        modelBuilder.Entity<PointsFaible>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__points_f__3213E83F6E45F689");

            entity.ToTable("points_faibles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<TypePersonnage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__aligneme__3213E83F961303DF");

            entity.ToTable("typePersonnage");

            entity.HasIndex(e => e.Nom, "UQ__aligneme__DF90DC2C3FA1DF58").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<UtilisateurModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__utilisat__3213E83F47B22DBD");

            entity.ToTable("utilisateurs");

            entity.HasIndex(e => e.Email, "UQ__utilisat__AB6E61649A1F478E").IsUnique();

            entity.HasIndex(e => e.Pseudo, "UQ__utilisat__EA0EEA2298E82E82").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateInscription)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_inscription");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(255)
                .HasColumnName("mot_de_passe");
            entity.Property(e => e.Pseudo)
                .HasMaxLength(50)
                .HasColumnName("pseudo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
