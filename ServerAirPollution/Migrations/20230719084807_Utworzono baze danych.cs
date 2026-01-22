using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirPollutionPrediction.Migrations
{
    /// <inheritdoc />
    public partial class Utworzonobazedanych : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Station_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Station_Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    PollutionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimalnaTemperaturaDobowa = table.Column<double>(type: "float", nullable: false),
                    RóznicaMaksymalnejIMinimalnejTemperatury = table.Column<double>(type: "float", nullable: false),
                    RodzajOpadu = table.Column<double>(type: "float", nullable: false),
                    ŚredniaTemperaturaDobowa = table.Column<double>(type: "float", nullable: false),
                    ŚredniaDobowaWilgotnośćWzględna = table.Column<double>(type: "float", nullable: false),
                    ŚredniaDobowaPrędkośćWiatru = table.Column<double>(type: "float", nullable: false),
                    ŚrednieDoboweZachmurzenieOgólne = table.Column<double>(type: "float", nullable: false),
                    ŚredniaDoboweCiśnienieNaPoziomieStacji = table.Column<double>(type: "float", nullable: false),
                    SumaOpaduDzień = table.Column<double>(type: "float", nullable: false),
                    DzieńRoku = table.Column<int>(type: "int", nullable: false),
                    DzieńTygodnia = table.Column<int>(type: "int", nullable: false),
                    ValuePm10 = table.Column<double>(type: "float", nullable: false),
                    ValueCatPm10 = table.Column<double>(type: "float", nullable: false),
                    ValuePm25 = table.Column<double>(type: "float", nullable: false),
                    ValueCatPm25 = table.Column<double>(type: "float", nullable: false),
                    ValueNO2 = table.Column<double>(type: "float", nullable: false),
                    ValueCatNO2 = table.Column<double>(type: "float", nullable: false),
                    ValueO3 = table.Column<double>(type: "float", nullable: false),
                    ValueCatO3 = table.Column<double>(type: "float", nullable: false),
                    ValueSO2 = table.Column<double>(type: "float", nullable: false),
                    ValueCatSO2 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id_sql = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Station_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Station = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Station_Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemperaturaDobowa = table.Column<double>(type: "float", nullable: false),
                    WilgotnośćWzględna = table.Column<double>(type: "float", nullable: false),
                    PrędkośćWiatru = table.Column<double>(type: "float", nullable: false),
                    ZachmurzenieOgólne = table.Column<double>(type: "float", nullable: false),
                    CiśnienieNaPoziomieStacji = table.Column<double>(type: "float", nullable: false),
                    SumaOpaduDzień = table.Column<double>(type: "float", nullable: false),
                    ValuePm10 = table.Column<double>(type: "float", nullable: false),
                    ValueCatPm10 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValuePm25 = table.Column<double>(type: "float", nullable: false),
                    ValueCatPm25 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueNo2 = table.Column<double>(type: "float", nullable: false),
                    ValueCatNo2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueO3 = table.Column<double>(type: "float", nullable: false),
                    ValueCatO3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueSo2 = table.Column<double>(type: "float", nullable: false),
                    ValueCatSo2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Session = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id_sql);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputData");

            migrationBuilder.DropTable(
                name: "Record");
        }
    }
}
