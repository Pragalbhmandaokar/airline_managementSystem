using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace airline_backend.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    FlightsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avaliableNumber = table.Column<int>(type: "int", nullable: false),
                    sourcePlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    destinationPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataOfJourney = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.FlightsId);
                });

            migrationBuilder.CreateTable(
                name: "passengers",
                columns: table => new
                {
                    passengerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    passengerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticketNumberForFlight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    panNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passengers", x => x.passengerId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserModelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "passengers");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
