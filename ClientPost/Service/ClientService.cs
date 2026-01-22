using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Client.DataSchemas;

using Client.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Client.Service
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

        public async Task<ImgwData> GetImgwData(HttpClient client)
        {
            ImgwData imgwData = new ImgwData();    
                try
                {
                    Console.WriteLine(client.BaseAddress.ToString());
                    ImgwDataSchema fetch = new ImgwDataSchema();
                    // Get the inputData
                    fetch = await GetProductIMGWAsync(client.BaseAddress.ToString(),client);

                    imgwData = new ImgwData
                    {
                        id_stacji = fetch.id_stacji,
                        stacja = fetch.stacja,
                        data_pomiaru = fetch.data_pomiaru,
                        godzina_pomiaru = fetch.godzina_pomiaru,
                        temperatura = fetch.temperatura,
                        predkosc_wiatru = fetch.predkosc_wiatru,
                        kierunek_wiatru = fetch.kierunek_wiatru,
                        wilgotnosc_wzgledna = fetch.wilgotnosc_wzgledna,
                        suma_opadu =fetch.suma_opadu,
                        cisnienie = fetch.cisnienie,
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Nastąpił problem z pobraniem danych ze strony {client.BaseAddress}, sprawdź log.txt w celu rozwiązania problemu" + "\n" + e.Message + "\n" + e.StackTrace);
                }

                return imgwData;
        }

        public async Task<List<UmData>> GetUmData(HttpClient client)
        {
            List<UmData> outputDatas = new List<UmData>();

            try
            {
                Console.WriteLine(client.BaseAddress.ToString());
                UmDataSchema.Rootobject umData = new UmDataSchema.Rootobject();
                // Get the inputData
                umData = await GetProductUMAsync(client.BaseAddress.ToString(),client);

                

                    foreach(UmDataSchema.Result result in umData.result)
                    {
                        var outputData = new UmData
                        {
                            Name = result.name,
                            Station = result.station,
                            Lon = result.lon,
                            Lat = result.lat,
                            Time = DateTime.Now,
                            OutputValues = new List<UmDataSchema.Datum>(result.data.ToList()),
                            OutputValuesNames = new List<UmDataSchema.Ijp1>()
                        };


                        

                        Console.WriteLine(outputData.Name);
                        foreach(UmDataSchema.Datum datum in result.data){
                            Console.WriteLine(datum.toString());
                            if(datum.ijp is not null){
                                outputData.OutputValuesNames.Add(datum.ijp);
                                Console.WriteLine(datum.ijp.toString());
                                
                            }
                            else if(datum.ijp is null)
                                Console.WriteLine("Nie ma tutaj" +outputDatas.IndexOf(outputData) +  "ijp" );
                        }
                        

                        outputDatas.Add(outputData);
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Nastąpił problem z pobraniem danych ze strony {client.BaseAddress}, sprawdź log.txt w celu rozwiązania problemu" + "\n" + e.Message + "\n" + e.StackTrace);
            }

            return outputDatas;
        }

        public async Task<int> FetchRecordsList(List<UmData> outputDatas, ImgwData imgwData, string zachmurzenie,int fetchCount,HttpClient client)
        {
            
            DataService _dataService = new DataService();
            List<Record> records = new List<Record>();

            _dataService.FetchRecord(outputDatas,imgwData,zachmurzenie,records,fetchCount);

            
            try{
                foreach (Record record in records)
                {
                    Console.WriteLine(record.Id);
                    var url = await CreateProductAsync(record,client);
                    Console.WriteLine($"Created at {url}");  
                }

            }catch(Exception e){
            
               Console.WriteLine("Nastąpił problem z wykonaniem polecenia POST do bazy danych, sprawdź wiadomość błędu oraz miejsce jego zajścia" + "\n" + e.Message + "\n" + e.StackTrace); 
            }

            return records[0].Session + 1;
               
        }






        private static async Task<UmDataSchema.Rootobject> GetProductUMAsync(string path,HttpClient client)
        {
            UmDataSchema.Rootobject umData = null;
            
            HttpResponseMessage response = await client.GetAsync(path);
            
            if (response.IsSuccessStatusCode)
            {
                umData  = await response.Content.ReadFromJsonAsync<UmDataSchema.Rootobject>();
            }
            
            return umData;
        }

       static async Task<ImgwDataSchema> GetProductIMGWAsync(string path,HttpClient client)
        {
            ImgwDataSchema imgwData = null;
            
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                imgwData  = await response.Content.ReadFromJsonAsync<ImgwDataSchema>();
            }
                
            return imgwData;
        }

        static async Task<Uri> CreateProductAsync(Record inputData, HttpClient client)
        {



            Console.WriteLine(inputData);

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "AirPollution/CreateRecord", inputData);
            response.EnsureSuccessStatusCode();
            
            // return URI of the created resource.
            return response.Headers.Location;
        }

        public void PostToDatabase()
        {
            throw new NotImplementedException();
        }

        public string GetDataFromHtml() 
        {
            // Inicjalizacja przeglądarki Chrome
            string text = "-1";
            Console.WriteLine("dZiałam");

            var timesExecuted = 0;

            while(text =="-1" | timesExecuted > 8)
            {
                using (IWebDriver driver = new ChromeDriver()) {
                // Otwarcie strony
                driver.Url = "https://meteo.imgw.pl/dyn/#group=nwp&param=tcc-0-sfc&model=alaro4k0&loc=52.23279360361761,21.014013290405277,14";
                Task.Delay(2000);
                // Znajdowanie elementu na stronie (np. za pomocą selektora CSS lub XPath)
                IWebElement element = driver.FindElement(By.CssSelector("#forecast-b"));
                // Pobranie kodu HTML wybranego elementu
                string htmlCode = element.GetAttribute("outerHTML");

                // Zamykanie przeglądarki
                driver.Quit();
                

                // Zapisanie kodu HTML do pliku
                string filePath = "C:/Users/Franczi/OneDrive - Polsko-Japońska Akademia Technik Komputerowych/Desktop/AirPollutionPredictionFinal/ClientPost/filename.html";

                using (StreamWriter outwriter = new StreamWriter(filePath, true))
                {
                    outwriter.WriteLine(htmlCode);
                }

                Console.WriteLine("Zapisano kod HTML wybranego elementu do pliku.");

                text = GetValueFromFile(filePath);
                }

                timesExecuted++;
            }    
            return text;
        }



        public string GetValueFromFile(string filename)
        {
            try
            {
                string fileContent = File.ReadAllText(filename); // Odczytanie zawartości pliku

                string pattern = @"Zachmurzenie:\s*(\d+)%"; // Wyrażenie regularne dla zachmurzenia (liczba procentowa)

                // Usunięcie tagów HTML przed wyszukiwaniem
                string cleanedText = Regex.Replace(fileContent, @"<.*?>", "");

                Match match = Regex.Match(cleanedText, pattern);

                if (match.Success)
                {
                    string cloudinessValue = match.Groups[1].Value;
                    return cloudinessValue;
                }
                else
                {
                    return "-1"; //brak informacji o zachmurzeniu 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
                return "Błąd odczytu pliku.";
                
            }
        }
    }
}