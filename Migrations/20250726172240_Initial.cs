using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MySecureWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ranks",
                columns: table => new
                {
                    rank_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rank_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranks", x => x.rank_id);
                });

            migrationBuilder.CreateTable(
                name: "ships",
                columns: table => new
                {
                    ship_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ship_name = table.Column<string>(type: "text", nullable: true),
                    registry = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ships", x => x.ship_id);
                });

            migrationBuilder.CreateTable(
                name: "officers",
                columns: table => new
                {
                    officer_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    officer_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    officer_rank_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_officers", x => x.officer_id);
                    table.ForeignKey(
                        name: "FK_officers_ranks_officer_rank_id",
                        column: x => x.officer_rank_id,
                        principalTable: "ranks",
                        principalColumn: "rank_id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "officers");

            migrationBuilder.DropTable(
                name: "ships");

            migrationBuilder.DropTable(
                name: "ranks");
        }
    }
}
