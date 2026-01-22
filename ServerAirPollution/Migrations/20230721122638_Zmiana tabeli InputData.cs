using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirPollutionPrediction.Migrations
{
    /// <inheritdoc />
    public partial class ZmianatabeliInputData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimalnaTemperaturaDobowa",
                table: "InputData",
                newName: "MinimalnaTemperaturaPrzyGruncie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimalnaTemperaturaPrzyGruncie",
                table: "InputData",
                newName: "MinimalnaTemperaturaDobowa");
        }
    }
}
