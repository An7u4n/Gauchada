using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gauchada.Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class db02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "Passenger",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Passenger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Passenger",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Passenger",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Passenger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Passenger");
        }
    }
}
