using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbOperations.Migrations
{
    /// <inheritdoc />
    public partial class updatedCurrencyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Currencies",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Currencies",
                newName: "Currency");
        }
    }
}
