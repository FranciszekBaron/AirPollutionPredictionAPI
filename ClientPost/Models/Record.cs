using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client.Models{
    public class Record
    {
        public int Id_sql  {get;set;}
        public int Id { get; set; }
        public string Station_Name { get; set; }

        public string Station { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public DateTime Station_Timestamp { get; set; }
        
        public double TemperaturaDobowa { get; set; }
        public double WilgotnośćWzględna { get; set; }
        public double PrędkośćWiatru { get; set; }
        public double ZachmurzenieOgólne { get; set; }
        public double CiśnienieNaPoziomieStacji { get; set; }
        public double SumaOpaduDzień { get; set; }
    
        public double valuePm10 { get; set; }
        public string valueCatPm10 { get; set; }
        public double valuePm25 { get; set; }
        public string valueCatPm25 { get; set; }
        public double valueNo2 { get; set; }
        public string valueCatNo2 { get; set; }
        public double valueO3 { get; set; }
        public string valueCatO3 { get; set; }
        public double valueSo2 { get; set; }
        public string valueCatSo2 { get; set; }

        public int  Session { get; set; }


        public override string ToString()
        {
            return "\t" + "Id: " + Id + "\n" +
                    "\t" + "station_Name: " + Station_Name + "\n" + 
                    "\t" + "Station: " + Station + "\n" + 
                    "\t" + "lon: " + Lon + "\n" + 
                    "\t" + "lat: " + Lat + "\n" + 
                    "\t" + "station_timestamp: " + Station_Timestamp + "\n" +
                    "\t" + "średniaTemperaturaDobowa: " + TemperaturaDobowa + "\n" +
                    "\t" + "średniaDobowaWilgotnośćWzględna: " + WilgotnośćWzględna + "\n" +
                    "\t" + "średniaDobowaPrędkośćWiatru: " + PrędkośćWiatru + "\n" +
                    "\t" + "średnieDoboweZachmurzenieOgólne: " + ZachmurzenieOgólne + "\n" +
                    "\t" + "średniaDoboweCiśnienieNaPoziomieStacji: " + CiśnienieNaPoziomieStacji + "\n" +
                    "\t" + "sumaOpaduDzień: " + SumaOpaduDzień + "\n" +
                    "\t" + "valuePm10: " + valuePm10 + "\n" +
                    "\t" + "valueCatPm10: " + valueCatPm10 + "\n" +
                    "\t" + "valuePm25: " + valuePm25 + "\n" +
                    "\t" + "valueCatPm25: " + valueCatPm25 + "\n" +
                    "\t" + "valueNO2: " + valueCatPm25 + "\n" +
                    "\t" + "valueNO2: " + valueNo2 + "\n" +
                    "\t" + "valueCatNO2: " + valueCatNo2 + "\n" +
                    "\t" + "valueO3: " + valueO3 + "\n" +
                    "\t" + "valueCatO3: " + valueCatO3 + "\n" +
                    "\t" + "valueSO2: " + valueSo2 + "\n" +
                    "\t" + "valueCatSO2: " + valueCatSo2 + "\n" +
                    "\t" + "session: " + Session;
        }
    }

    
}