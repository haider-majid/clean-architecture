using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clean_architecture.Migrations
{
    /// <inheritdoc />
    public partial class updateLocationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Loaction",
                table: "users",
                newName: "Location");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "users",
                newName: "Loaction");
        }
    }
}
