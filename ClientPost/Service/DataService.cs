using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Service
{
    public class DataService : IDataService
    {
        public void FetchRecord(List<UmData> outputDatas, ImgwData imgwData, string zachmurzenie,List<Record> records,int fetchCount)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";




            foreach (UmData umData in outputDatas)
            {
                Record record = new Record
                {
                    
                    Id = outputDatas.IndexOf(umData),
                    Station_Name = umData.Name,
                    Station = umData.Station,
                    Lon = umData.Lon,
                    Lat = umData.Lat,
                    Station_Timestamp = umData.Time,
                    TemperaturaDobowa = Convert.ToDouble(imgwData.temperatura,provider),
                    WilgotnośćWzględna = Convert.ToDouble(imgwData.wilgotnosc_wzgledna,provider),
                    PrędkośćWiatru = Convert.ToDouble(imgwData.predkosc_wiatru,provider),
                    ZachmurzenieOgólne = Convert.ToDouble(zachmurzenie,provider),
                    CiśnienieNaPoziomieStacji = Convert.ToDouble(imgwData.cisnienie,provider),
                    SumaOpaduDzień = Convert.ToDouble(imgwData.suma_opadu,provider),
                    valuePm10 = checkValue(umData.OutputValues,"PM10"),
                    valueCatPm10 = checkCategory(umData.OutputValues,"PM10"),
                    valuePm25 = checkValue(umData.OutputValues,"PM25"),
                    valueCatPm25 = checkCategory(umData.OutputValues,"PM25"),
                    valueNo2 = checkValue(umData.OutputValues,"NO2"),
                    valueCatNo2 = checkCategory(umData.OutputValues,"NO2"),
                    valueO3= checkValue(umData.OutputValues,"O3"),
                    valueCatO3 = checkCategory(umData.OutputValues,"O3"),
                    valueSo2 = checkValue(umData.OutputValues,"SO2"),
                    valueCatSo2 = checkCategory(umData.OutputValues,"SO2"),
                    Session = fetchCount
                };
                records.Add(record);
            }
        }

        public static double checkValue(List<DataSchemas.UmDataSchema.Datum> outputValues,string param_code)
        {
            var val = outputValues.Where(e=>e.param_code==param_code).FirstOrDefault();
            if(val is null || val.value.ToString() == "-")
                return 0;

            string result = val.value.ToString().Replace(".",",");
            return Convert.ToDouble(result);
        }

        public static string checkCategory(List<DataSchemas.UmDataSchema.Datum> outputValues,string param_code)
        {
            var val = outputValues.Where(e=>e.param_code==param_code).FirstOrDefault();
            if(val is null)
                return "null";

            return val.ijp.name;
        }
    }
}

    


