using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gauchada.Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class db05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoSrc",
                table: "Passenger",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoSrc",
                table: "Drivers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoSrc",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "PhotoSrc",
                table: "Drivers");
        }
    }
}
