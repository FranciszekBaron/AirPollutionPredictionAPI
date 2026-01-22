using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using  System.Net.Http;
using System.Text.Json;
using Client.DataSchemas;
using Client.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System.IO;
using System.Collections.Generic;
using System.Globalization;
using static Client.DataSchemas.UmDataSchema;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Client.Service;

namespace Client
{
    
    class Program
    {
        
        static HttpClient client = new HttpClient();
        static ClientService clientService = new ClientService();

        static int fetchCount = 1;

        static string UmApiAdress = "https://api.um.warszawa.pl/api/action/air_sensors_get/?apikey=f040f130-a2b1-4890-993b-a7a7a92382dd";

        static string ImgwApiAdress = "https://danepubliczne.imgw.pl/api/data/synop/station/warszawa";

        static string LocalApi = "http://localhost:5235/";      //do zmiany
        
        static string apiKey = "f2448d28-bc3a-478a-991d-7a3c578b6406";

        

               
        
        static void Main()
        {
            RunAsync(clientService).GetAwaiter().GetResult();
        }


        //Client Part, Http Request, Get and Update, fetch data
        static async Task RunAsync(IClientService _clientService)
        {

            
            //na potrzebe testowania
            
            try{

                //Get and fetch data with UM data
                client = _clientService.CreateClient(new Uri(UmApiAdress));

                List<UmData> outputDatas = await _clientService.GetUmData(client);


                Console.WriteLine("--------------------------------------------------------------------------------------------------------");

                foreach(DataSchemas.UmDataSchema.Datum datum in outputDatas[1].OutputValues){
                    Console.WriteLine(datum.toString());
                }

                foreach(DataSchemas.UmDataSchema.Ijp1 ijp11 in outputDatas[1].OutputValuesNames){
                    Console.WriteLine(ijp11.toString());
                }

                

                //Get and Fetch data from IMGW
                client = _clientService.CreateClient(new Uri(ImgwApiAdress));

                ImgwData imgwData = await _clientService.GetImgwData(client);
                
                
                //Get and Fetch data from Zachmurzenie 
                var zachmurzenie = _clientService.GetDataFromHtml();


                //Post do bazy 
                client = _clientService.CreateClient(new Uri(LocalApi));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiKey); // Ustawienie nagłówka Authorization z kluczem
                


                //Uzupełenienie bazy w metodzie FetchRecordsList
                fetchCount = await _clientService.FetchRecordsList(outputDatas,imgwData,zachmurzenie,fetchCount,client);
                outputDatas.Clear();
                outputDatas = null;
                    
            }catch(Exception e){
                Console.WriteLine(e);
            }
        
            //na potrzebe testowania
            await Task.Delay(3000);
                
               

        }
   
    }
}
                    
                    

                    

            



                

        



        
        

    






        

                
                

            
            

             



            


                





                

                

                    

                
                

                

                



