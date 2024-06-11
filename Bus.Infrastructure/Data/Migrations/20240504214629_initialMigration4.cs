using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bus.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravellerHistorySearch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TravellerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravellerHistorySearch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravellerHistorySearch_Users_TravellerId",
                        column: x => x.TravellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravellerHistorySearch_TravellerId",
                table: "TravellerHistorySearch",
                column: "TravellerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravellerHistorySearch");
        }
    }
}
