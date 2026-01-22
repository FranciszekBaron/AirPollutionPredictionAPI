using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using  System.Net.Http;
using System.Text.Json;

using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Service;

namespace ClientDelete
{
    
    class Program
    {
        
        static HttpClient client = new HttpClient();
        static ClientService clientService = new ClientService();
        static string LocalApi = "http://localhost:5235/";      //do zmiany
        static string apiKey = "f2448d28-bc3a-478a-991d-7a3c578b6406";

        

               
        
        static void Main()
        {
            RunAsync(clientService).GetAwaiter().GetResult();
        }


        //Client Part, Http Request, Get and Update, fetch data
        static async Task RunAsync(IClientService _clientService)
        {

            client = _clientService.CreateClient(new Uri(LocalApi));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiKey);

            await _clientService.DeleteData(client);


            
                
        }    

    }
   
}

                    
                    

                    

            



                

        



        
        

    






        

                
                

            
            

             



            


                





                

                

                    

                
                

                

                



