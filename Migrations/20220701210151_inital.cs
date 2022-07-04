using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace airline_backend.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightName",
                table: "flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    TicketsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    passengerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flightId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.TicketsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropColumn(
                name: "FlightName",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "price",
                table: "flights");
        }
    }
}
