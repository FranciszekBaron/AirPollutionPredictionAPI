using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirPollutionPrediction.Migrations
{
    /// <inheritdoc />
    public partial class ZmianatabeliInputData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RodzajOpadu",
                table: "InputData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RodzajOpadu",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
