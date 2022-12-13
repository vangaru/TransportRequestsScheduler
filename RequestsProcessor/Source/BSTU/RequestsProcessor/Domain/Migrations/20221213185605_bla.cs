using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTU.RequestsProcessor.Domain.Migrations
{
    /// <inheritdoc />
    public partial class bla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SourceBusStopName = table.Column<string>(type: "text", nullable: false),
                    DestinationBusStopName = table.Column<string>(type: "text", nullable: false),
                    ReasonForTravel = table.Column<string>(type: "text", nullable: false),
                    SeatsCount = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
