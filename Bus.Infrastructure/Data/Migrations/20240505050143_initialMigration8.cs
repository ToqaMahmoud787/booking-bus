using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bus.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilterCriteria",
                table: "TravellerHistorySearchs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilterCriteria",
                table: "TravellerHistorySearchs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
