using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Client.Models
{
    public class UmData
    {
        public string Name { get; set; }

        public string Station { get; set; }

        public double Lon { get; set; }

        public double Lat { get; set; }

        public DateTime Time { get; set; }
        public List<DataSchemas.UmDataSchema.Datum> OutputValues { get; set; } 
        
        public List<DataSchemas.UmDataSchema.Ijp1>  OutputValuesNames { get; set; }


        public override string ToString()
        {
            return "\t" + "Name: " + Name + "\n" +
                   "\t" +"Lon: " + Lon + "\n" +
                   "\t"+ "Lat: " + Lat + "\n" +
                   "\t" + "Time: " + Time + "\n" +
                   "\t" + "OutputValues: " + "\n";
        }



        


    }
}