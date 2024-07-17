using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gauchada.Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class db04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarPlate",
                table: "Trips",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CarPlate",
                table: "Trips",
                column: "CarPlate");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cars_CarPlate",
                table: "Trips",
                column: "CarPlate",
                principalTable: "Cars",
                principalColumn: "CarPlate",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cars_CarPlate",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_CarPlate",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CarPlate",
                table: "Trips");
        }
    }
}
