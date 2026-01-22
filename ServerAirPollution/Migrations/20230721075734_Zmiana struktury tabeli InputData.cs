using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirPollutionPrediction.Migrations
{
    /// <inheritdoc />
    public partial class ZmianastrukturytabeliInputData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PollutionType",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueCatNO2",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueCatO3",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueCatPm10",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueCatPm25",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueCatSO2",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueNO2",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueO3",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValuePm10",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValuePm25",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueSO2",
                table: "InputData");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "InputData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueCat",
                table: "InputData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "InputData");

            migrationBuilder.DropColumn(
                name: "ValueCat",
                table: "InputData");

            migrationBuilder.AddColumn<string>(
                name: "PollutionType",
                table: "InputData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ValueCatNO2",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueCatO3",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueCatPm10",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueCatPm25",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueCatSO2",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueNO2",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueO3",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValuePm10",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValuePm25",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueSO2",
                table: "InputData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
