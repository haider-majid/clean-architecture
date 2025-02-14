using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clean_architecture.Migrations
{
    /// <inheritdoc />
    public partial class updatregister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "location",
                table: "users");

            migrationBuilder.DropColumn(
                name: "name",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
