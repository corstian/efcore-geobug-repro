using System;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Migrations;

namespace efcore_spatial_aggregateException.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airfields",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OurAirfieldsId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Icao = table.Column<string>(nullable: true),
                    Iata = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    HomePage = table.Column<string>(nullable: true),
                    Location = table.Column<IPoint>(nullable: true),
                    Continent = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Muncipality = table.Column<string>(nullable: true),
                    HasScheduledService = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airfields", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airfields");
        }
    }
}
