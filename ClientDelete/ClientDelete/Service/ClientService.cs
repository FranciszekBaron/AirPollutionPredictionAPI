using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Service
{
    public class ClientService : IClientService
    {

        


        public HttpClient CreateClient(Uri adress)
        {
            HttpClient client = new HttpClient();
            // Update port # in the following line.
            client.BaseAddress = adress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        
        public async Task<HttpStatusCode> DeleteData( HttpClient client)
        {
            var statusCode = HttpStatusCode.OK;

            try
            {
                statusCode = await DeleteAsync(client);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                return statusCode;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
            }
            
            return statusCode;
        }



        static async Task<HttpStatusCode> DeleteAsync(HttpClient client)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"AirPollution/DeleteRecords");
            return response.StatusCode;
        }

    }
}