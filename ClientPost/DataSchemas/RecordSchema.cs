using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models;

namespace Client.DataSchemas
{

    public class RecordSchema
    {
        public class Rootobject
        {
            public Class1[] records { get; set; }

        }

        public class Class1
        {
            public int id { get; set; }
            public string station_Name { get; set; }
            public double lon { get; set; }
            public double lat { get; set; }
            public DateTime station_Timestamp { get; set; }
            public double temperaturaDobowa { get; set; }
            public double wilgotnośćWzględna { get; set; }
            public double prędkośćWiatru { get; set; }
            public double zachmurzenieOgólne { get; set; }
            public double ciśnienieNaPoziomieStacji { get; set; }
            public double sumaOpaduDzień { get; set; }
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
            public int session { get; set; }
        }



    }
}