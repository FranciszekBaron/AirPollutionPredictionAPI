using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DataSchemas
{
    public class UmDataSchema
    {
        
        public class Rootobject
        {
            public Result[] result { get; set; }

            public string toString()
            {
                return " ";
            }
        }

        public class Result
        {
            public Ijp ijp { get; set; }
            public string data_source { get; set; }
            public string name { get; set; }
            public string station_type { get; set; }
            public float lon { get; set; }
            public string owner { get; set; }
            public string station { get; set; }
            public Address address { get; set; }
            public float lat { get; set; }
            public Datum[] data { get; set; }
        }

        public class Ijp
        {
            public string name { get; set; }
            public string recommendations { get; set; }
        }

        public class Address
        {
            public string city { get; set; }
            public string street { get; set; }
            public string zip_code { get; set; }
            public string district { get; set; }
            public string commune { get; set; }
        }

        public class Datum
        {
            public Ijp1 ijp { get; set; }
            public string param_name { get; set; }
            public string param_code { get; set; }
            public object value { get; set; }
            public string time { get; set; }
            public string unit { get; set; }

            public string toString(){
                return "name: " + param_name + " code: " + param_code;
            }


        }

        public class Ijp1
        {
            public string name { get; set; }

            public  string toString()
            {
                return "name: " + name;
            }


            
        }


    }
}