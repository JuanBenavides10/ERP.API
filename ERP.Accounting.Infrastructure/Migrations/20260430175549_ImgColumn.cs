using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Accounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImgColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "logo",
                table: "companies");

            migrationBuilder.AddColumn<string>(
                name: "logo_path",
                table: "companies",
                type: "varchar(40)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "logo_path",
                table: "companies");

            migrationBuilder.AddColumn<byte[]>(
                name: "logo",
                table: "companies",
                type: "bytea",
                nullable: true);
        }
    }
}
