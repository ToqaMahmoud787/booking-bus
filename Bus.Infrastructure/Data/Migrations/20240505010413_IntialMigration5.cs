using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bus.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntialMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravellerHistorySearch_Users_TravellerId",
                table: "TravellerHistorySearch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravellerHistorySearch",
                table: "TravellerHistorySearch");

            migrationBuilder.RenameTable(
                name: "TravellerHistorySearch",
                newName: "TravellerHistorySearchs");

            migrationBuilder.RenameIndex(
                name: "IX_TravellerHistorySearch_TravellerId",
                table: "TravellerHistorySearchs",
                newName: "IX_TravellerHistorySearchs_TravellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravellerHistorySearchs",
                table: "TravellerHistorySearchs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TravellerHistorySearchs_Users_TravellerId",
                table: "TravellerHistorySearchs",
                column: "TravellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravellerHistorySearchs_Users_TravellerId",
                table: "TravellerHistorySearchs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravellerHistorySearchs",
                table: "TravellerHistorySearchs");

            migrationBuilder.RenameTable(
                name: "TravellerHistorySearchs",
                newName: "TravellerHistorySearch");

            migrationBuilder.RenameIndex(
                name: "IX_TravellerHistorySearchs_TravellerId",
                table: "TravellerHistorySearch",
                newName: "IX_TravellerHistorySearch_TravellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravellerHistorySearch",
                table: "TravellerHistorySearch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TravellerHistorySearch_Users_TravellerId",
                table: "TravellerHistorySearch",
                column: "TravellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
