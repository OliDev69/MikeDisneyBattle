using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisneyBattle.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lieux",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lieux__3213E83FE8421A32", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "objets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    niveau_requis = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    bonus_pv = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    bonus_attaque = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    bonus_defense = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__objets__3213E83F399A5385", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "points_faibles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__points_f__3213E83F6E45F689", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "typePersonnage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__aligneme__3213E83F961303DF", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "utilisateurs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pseudo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mot_de_passe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date_inscription = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__utilisat__3213E83F47B22DBD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "personnages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    alignement_id = table.Column<int>(type: "int", nullable: false),
                    niveau = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    experience = table.Column<int>(type: "int", nullable: false),
                    points_vie = table.Column<int>(type: "int", nullable: false),
                    points_attaque = table.Column<int>(type: "int", nullable: false),
                    points_defense = table.Column<int>(type: "int", nullable: false),
                    lieu_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__personna__3213E83FBE136F56", x => x.id);
                    table.ForeignKey(
                        name: "FK_personnages_alignements",
                        column: x => x.alignement_id,
                        principalTable: "typePersonnage",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_personnages_lieux",
                        column: x => x.lieu_id,
                        principalTable: "lieux",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "equipes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    utilisateur_id = table.Column<int>(type: "int", nullable: false),
                    date_creation = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__equipes__3213E83F09C59F4A", x => x.id);
                    table.ForeignKey(
                        name: "FK_equipes_utilisateurs",
                        column: x => x.utilisateur_id,
                        principalTable: "utilisateurs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "personnages_objets_autorises",
                columns: table => new
                {
                    personnage_id = table.Column<int>(type: "int", nullable: false),
                    objet_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personnages_objets", x => new { x.personnage_id, x.objet_id });
                    table.ForeignKey(
                        name: "FK_objets_autorises_objets",
                        column: x => x.objet_id,
                        principalTable: "objets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_objets_autorises_personnages",
                        column: x => x.personnage_id,
                        principalTable: "personnages",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "personnages_points_faibles",
                columns: table => new
                {
                    personnage_id = table.Column<int>(type: "int", nullable: false),
                    point_faible_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personnages_points_faibles", x => new { x.personnage_id, x.point_faible_id });
                    table.ForeignKey(
                        name: "FK_points_faibles_personnages",
                        column: x => x.personnage_id,
                        principalTable: "personnages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_points_faibles_points",
                        column: x => x.point_faible_id,
                        principalTable: "points_faibles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "combats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    equipe1_id = table.Column<int>(type: "int", nullable: false),
                    equipe2_id = table.Column<int>(type: "int", nullable: false),
                    equipe_gagnante_id = table.Column<int>(type: "int", nullable: true),
                    experience_gagnee = table.Column<int>(type: "int", nullable: false, defaultValue: 100),
                    date_combat = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__combats__3213E83FD3B119FD", x => x.id);
                    table.ForeignKey(
                        name: "FK_combats_equipe1",
                        column: x => x.equipe1_id,
                        principalTable: "equipes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_combats_equipe2",
                        column: x => x.equipe2_id,
                        principalTable: "equipes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_combats_gagnant",
                        column: x => x.equipe_gagnante_id,
                        principalTable: "equipes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "equipes_personnages",
                columns: table => new
                {
                    equipe_id = table.Column<int>(type: "int", nullable: false),
                    personnage_id = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipes_personnages", x => new { x.equipe_id, x.personnage_id });
                    table.ForeignKey(
                        name: "FK_equipes_personnages_equipes",
                        column: x => x.equipe_id,
                        principalTable: "equipes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_equipes_personnages_personnages",
                        column: x => x.personnage_id,
                        principalTable: "personnages",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "combats_objets",
                columns: table => new
                {
                    combat_id = table.Column<int>(type: "int", nullable: false),
                    personnage_id = table.Column<int>(type: "int", nullable: false),
                    objet_id = table.Column<int>(type: "int", nullable: false),
                    tour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_combats_objets", x => new { x.combat_id, x.personnage_id, x.objet_id, x.tour });
                    table.ForeignKey(
                        name: "FK_combats_objets_combats",
                        column: x => x.combat_id,
                        principalTable: "combats",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_combats_objets_objets",
                        column: x => x.objet_id,
                        principalTable: "objets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_combats_objets_personnages",
                        column: x => x.personnage_id,
                        principalTable: "personnages",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "historique_niveaux",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personnage_id = table.Column<int>(type: "int", nullable: false),
                    combat_id = table.Column<int>(type: "int", nullable: false),
                    ancien_niveau = table.Column<int>(type: "int", nullable: false),
                    nouveau_niveau = table.Column<int>(type: "int", nullable: false),
                    date_changement = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__historiq__3213E83F0D8A910C", x => x.id);
                    table.ForeignKey(
                        name: "FK_historique_combats",
                        column: x => x.combat_id,
                        principalTable: "combats",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_historique_personnages",
                        column: x => x.personnage_id,
                        principalTable: "personnages",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_combats_equipe_gagnante_id",
                table: "combats",
                column: "equipe_gagnante_id");

            migrationBuilder.CreateIndex(
                name: "IX_combats_equipe1_id",
                table: "combats",
                column: "equipe1_id");

            migrationBuilder.CreateIndex(
                name: "IX_combats_equipe2_id",
                table: "combats",
                column: "equipe2_id");

            migrationBuilder.CreateIndex(
                name: "IX_combats_objets_objet_id",
                table: "combats_objets",
                column: "objet_id");

            migrationBuilder.CreateIndex(
                name: "IX_combats_objets_personnage_id",
                table: "combats_objets",
                column: "personnage_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipes_utilisateur_id",
                table: "equipes",
                column: "utilisateur_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipes_personnages_personnage_id",
                table: "equipes_personnages",
                column: "personnage_id");

            migrationBuilder.CreateIndex(
                name: "IX_historique_niveaux_combat_id",
                table: "historique_niveaux",
                column: "combat_id");

            migrationBuilder.CreateIndex(
                name: "IX_historique_niveaux_personnage_id",
                table: "historique_niveaux",
                column: "personnage_id");

            migrationBuilder.CreateIndex(
                name: "IX_personnages_alignement_id",
                table: "personnages",
                column: "alignement_id");

            migrationBuilder.CreateIndex(
                name: "IX_personnages_lieu_id",
                table: "personnages",
                column: "lieu_id");

            migrationBuilder.CreateIndex(
                name: "UQ__personna__DF90DC2C89D959C4",
                table: "personnages",
                column: "nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personnages_objets_autorises_objet_id",
                table: "personnages_objets_autorises",
                column: "objet_id");

            migrationBuilder.CreateIndex(
                name: "IX_personnages_points_faibles_point_faible_id",
                table: "personnages_points_faibles",
                column: "point_faible_id");

            migrationBuilder.CreateIndex(
                name: "UQ__aligneme__DF90DC2C3FA1DF58",
                table: "typePersonnage",
                column: "nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__utilisat__AB6E61649A1F478E",
                table: "utilisateurs",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__utilisat__EA0EEA2298E82E82",
                table: "utilisateurs",
                column: "pseudo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "combats_objets");

            migrationBuilder.DropTable(
                name: "equipes_personnages");

            migrationBuilder.DropTable(
                name: "historique_niveaux");

            migrationBuilder.DropTable(
                name: "personnages_objets_autorises");

            migrationBuilder.DropTable(
                name: "personnages_points_faibles");

            migrationBuilder.DropTable(
                name: "combats");

            migrationBuilder.DropTable(
                name: "objets");

            migrationBuilder.DropTable(
                name: "personnages");

            migrationBuilder.DropTable(
                name: "points_faibles");

            migrationBuilder.DropTable(
                name: "equipes");

            migrationBuilder.DropTable(
                name: "typePersonnage");

            migrationBuilder.DropTable(
                name: "lieux");

            migrationBuilder.DropTable(
                name: "utilisateurs");
        }
    }
}
