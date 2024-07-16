using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gauchada.Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class db03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarPlate = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaxPassengers = table.Column<int>(type: "int", nullable: false),
                    OwnerUserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarPlate);
                    table.ForeignKey(
                        name: "FK_Cars_Drivers_OwnerUserName",
                        column: x => x.OwnerUserName,
                        principalTable: "Drivers",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DriverUserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Drivers_DriverUserName",
                        column: x => x.DriverUserName,
                        principalTable: "Drivers",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassengerTrips",
                columns: table => new
                {
                    PassengersUserName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    TripsTripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerTrips", x => new { x.PassengersUserName, x.TripsTripId });
                    table.ForeignKey(
                        name: "FK_PassengerTrips_Passenger_PassengersUserName",
                        column: x => x.PassengersUserName,
                        principalTable: "Passenger",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengerTrips_Trips_TripsTripId",
                        column: x => x.TripsTripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerUserName",
                table: "Cars",
                column: "OwnerUserName");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerTrips_TripsTripId",
                table: "PassengerTrips",
                column: "TripsTripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverUserName",
                table: "Trips",
                column: "DriverUserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "PassengerTrips");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
