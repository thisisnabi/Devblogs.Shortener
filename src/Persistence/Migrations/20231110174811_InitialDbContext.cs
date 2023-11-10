using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Devblogs.Shortener.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shortener");

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "shortener",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LongUrl = table.Column<string>(type: "nvarchar(2083)", maxLength: 2083, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LongUrl",
                schema: "shortener",
                table: "Tags",
                column: "LongUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ShortCode",
                schema: "shortener",
                table: "Tags",
                column: "ShortCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags",
                schema: "shortener");
        }
    }
}
