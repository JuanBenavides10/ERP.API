using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "users",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "persons",
                newName: "persons",
                newSchema: "identity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "users",
                schema: "identity",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "persons",
                schema: "identity",
                newName: "persons");
        }
    }
}
