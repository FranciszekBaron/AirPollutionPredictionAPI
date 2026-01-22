using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace ServerAirPollution.Model
{
    /// <summary>
    /// Klasa reprezentująca zapis pomiarów, która zawiera informacje o różnych parametrach pomiarowych oraz ich walidacji.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Identyfikator SQL.
        /// </summary>
        public int Id_sql { get; set; }

        /// <summary>
        /// Identyfikator pomiaru.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa stacji pomiarowej.
        /// </summary>
        public string Station_Name { get; set; }

        /// <summary>
        /// Nazwa stacji.
        /// </summary>
        public string Station { get; set; }

        /// <summary>
        /// Długość geograficzna.
        /// </summary>
        public double Lon { get; set; }

        /// <summary>
        /// Szerokość geograficzna.
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Znacznik czasowy stacji pomiarowej.
        /// </summary>
        public DateTime Station_Timestamp { get; set; }

        /// <summary>
        /// Temperatura dobowa.
        /// </summary>
        public double TemperaturaDobowa { get; set; }

        /// <summary>
        /// Wilgotność względna.
        /// </summary>
        public double WilgotnośćWzględna { get; set; }

        /// <summary>
        /// Prędkość wiatru.
        /// </summary>
        public double PrędkośćWiatru { get; set; }

        /// <summary>
        /// Zachmurzenie ogólne.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Pole ZachmurzenieOgólne nie może być ujemne.")]
        public double ZachmurzenieOgólne { get; set; }

        /// <summary>
        /// Ciśnienie na poziomie stacji.
        /// </summary>
        public double CiśnienieNaPoziomieStacji { get; set; }

        /// <summary>
        /// Suma opadu w ciągu dnia.
        /// </summary>
        public double SumaOpaduDzień { get; set; }

        /// <summary>
        /// Wartość PM10.
        /// </summary>
        [AllowNull]
        public double ValuePm10 { get; set; }

        /// <summary>
        /// Kategoria wartości PM10.
        /// </summary>
        [AllowNull]
        public string ValueCatPm10 { get; set; }

        /// <summary>
        /// Wartość PM2.5.
        /// </summary>
        [AllowNull]
        public double ValuePm25 { get; set; }

        /// <summary>
        /// Kategoria wartości PM2.5.
        /// </summary>
        [AllowNull]
        public string ValueCatPm25 { get; set; }

        /// <summary>
        /// Wartość NO2.
        /// </summary>
        [AllowNull]
        public double ValueNo2 { get; set; }

        /// <summary>
        /// Kategoria wartości NO2.
        /// </summary>
        [AllowNull]
        public string ValueCatNo2 { get; set; }

        /// <summary>
        /// Wartość O3.
        /// </summary>
        [AllowNull]
        public double ValueO3 { get; set; }

        /// <summary>
        /// Kategoria wartości O3.
        /// </summary>
        [AllowNull]
        public string ValueCatO3 { get; set; }

        /// <summary>
        /// Wartość SO2.
        /// </summary>
        [AllowNull]
        public double ValueSo2 { get; set; }

        /// <summary>
        /// Kategoria wartości SO2.
        /// </summary>
        [AllowNull]
        public string ValueCatSo2 { get; set; }

        /// <summary>
        /// Sesja pomiarowa.
        /// </summary>
        [Range(1, 24, ErrorMessage = "Pole {0} musi mieścić się w przedziale od {1} do {2}.")]
        public int Session { get; set; }

        /// <summary>
        /// Przesłonięta metoda ToString do formatowania zawartości klasy.
        /// </summary>
        public override string ToString()
        {
            return "\t" + "Id: " + Id + "\n" +
                "\t" + "station_Name: " + Station_Name + "\n" +
                "\t" + "lon: " + Lon + "\n" +
                "\t" + "lat: " + Lat + "\n" +
                "\t" + "station_timestamp: " + Station_Timestamp + "\n" +
                "\t" + "średniaTemperaturaDobowa: " + TemperaturaDobowa + "\n" +
                "\t" + "średniaDobowaWilgotnośćWzględna: " + WilgotnośćWzględna + "\n" +
                "\t" + "średniaDobowaPrędkośćWiatru: " + PrędkośćWiatru + "\n" +
                "\t" + "średnieDoboweZachmurzenieOgólne: " + ZachmurzenieOgólne + "\n" +
                "\t" + "średniaDoboweCiśnienieNaPoziomieStacji: " + CiśnienieNaPoziomieStacji + "\n" +
                "\t" + "sumaOpaduDzień: " + SumaOpaduDzień + "\n" +
                "\t" + "valuePm10: " + ValuePm10 + "\n" +
                "\t" + "valueCatPm10: " + ValueCatPm10 + "\n" +
                "\t" + "valuePm25: " + ValuePm25 + "\n" +
                "\t" + "valueCatPm25: " + ValueCatPm25 + "\n" +
                "\t" + "valueNO2: ";
        }
    }
    
}

        
