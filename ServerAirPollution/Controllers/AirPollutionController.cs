using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using AirPollutionPrediction.Model;


using System.Collections;

using System.IO;
using ServerAirPollution.Service;
using Microsoft.EntityFrameworkCore;
using ServerAirPollution.Model;
using System.Net.Http.Headers;
using ServerAirPollution.Model.Validation;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ServerAirPollution.Service.CalculationService;
using ServerAirPollution.Service.GlobalService;
using ServerAirPollution.Model.DTO;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.OutputCaching;

namespace ServerAirPollution.Controllers
{
    

// Kontroler obsługujący zapytania dotyczące zanieczyszczenia powietrza.
[Route("[controller]")]

public class AirPollutionController : Controller
{
    private readonly IRepositoryWrapper _service;          /// <summary>Interfejs do repozytorium.</summary>
    private readonly ICalculationService _calculation;     /// <summary>Interfejs do usługi obliczeniowej.</summary>
    private readonly IGlobalService _globalService;         /// <summary>Interfejs do globalnej usługi.</summary>
    private readonly ILogger<AirPollutionController> _logger;  /// <summary>Obiekt rejestrujący zdarzenia.</summary>

    private readonly IConfiguration _configuaration;

    

    /// <summary>
    /// Konstruktor klasy AirPollutionController.
    /// </summary>
    /// <param name="service">Interfejs do repozytorium.</param>
    /// <param name="calculation">Interfejs do usługi obliczeniowej.</param>
    /// <param name="globalService">Interfejs do globalnej usługi.</param>
    /// <param name="logger">Obiekt rejestrujący zdarzenia.</param>
    public AirPollutionController(IRepositoryWrapper service, ICalculationService calculation, IGlobalService globalService, ILogger<AirPollutionController> logger, IConfiguration configuration)
    {
        _service = service;             // Inicjalizacja repozytorium.
        _calculation = calculation;     // Inicjalizacja usługi obliczeniowej.
        _globalService = globalService; // Inicjalizacja globalnej usługi.
        _logger = logger;               // Inicjalizacja rejestrującego obiektu.
        _configuaration = configuration;
        

        // Dodanie różnych typów zanieczyszczeń do globalnej listy obsługiwanych typów.
        _globalService.AddToGlobalList("PM10");
        _globalService.AddToGlobalList("PM2.5");
        _globalService.AddToGlobalList("O3");
        _globalService.AddToGlobalList("NO2");
        _globalService.AddToGlobalList("SO2");

        // Rejestracja informacji o wywołaniu kontrolera w dzienniku.
        _logger.LogInformation("AirPollutionController called");
    }




    /// <summary>
    /// Punkt końcowy, który pobiera prognozy dla konkretnej stacji i rodzaju zanieczyszczenia.
    /// Wymaga uwierzytelnienia.
    /// </summary>
    /// <param name="station_Name">Nazwa stacji, dla której chcemy pobrać prognozy.</param>
    /// <param name="pollution_Type">Rodzaj zanieczyszczenia, dla którego chcemy pobrać prognozy.</param>
    /// <returns>Asynchroniczne zadanie reprezentujące operację i przechowujące 
    //[Authorize]
    [OutputCache(PolicyName = "base")]
    [HttpGet("GetPredictionForStation")]

    public async Task<IActionResult> GetPredictionForStation(string station_Name, string pollution_Type)
    {
        

        Console.WriteLine(_globalService.GetGlobalList());

        _logger.LogInformation("AirPollutionController got GetPredictionForStation() method started");

        // Sprawdzenie i walidacja argumentów stacji i typu zanieczyszczenia.
        var info = await _service.validation.AreArgumentsValid(station_Name, pollution_Type, _service.data, _globalService);

        if (info is not null)
        {
            _logger.LogWarning("Brakuje argumentu wywołaniu lub Dostarczono złe argumenty wywołania");
            _logger.LogInformation("Dostępne stację oraz zanieczyszczenia");
            _logger.LogInformation(JsonConvert.SerializeObject(info, Formatting.Indented));
            return BadRequest("Brakuję argumentu wywołania lub Dostarczone argumenty są nieodpowiednie" + "\n" +
                "Dostępne stację oraz zanieczyszczenia:" + "\n" + JsonConvert.SerializeObject(info, Formatting.Indented));
        }

        // Inicjalizacja zmiennych do przechowywania danych pomiarowych.
        InputData inputData = new InputData();
        OutputData outputData = new OutputData();
        List<Record> recordsData = await _service.data.GetByCondition(e => e.Station_Name == station_Name).ToListAsync();
        List<AirLoc> airLocList = await _service.airLoc.GetByCondition(e => e.PollutionType == pollution_Type).ToListAsync();

        // Inicjalizacja list przechowujących poszczególne parametry pomiarowe.
        List<double> temperatura = new List<double>();
        List<double> wilgotność = new List<double>();
        List<double> wiatr = new List<double>();
        List<double> zachmurzenie = new List<double>();
        List<double> ciśnienie = new List<double>();

        // Sprawdzenie, czy istnieją rekordy w tabelach Record oraz AirLoc.
        if (!(recordsData.Any() & airLocList.Any()))
            return BadRequest("Brak rekordów w tabelach Record lub AirLoc");

        // Przypisanie parametrów pomiarowych do odpowiednich list.
        //W bazie znajduje się maksymalnie 24 pomiary, każdy z nich indywidualnie od kolumny dodajemy do listy o nazwie kolumny, aby poźniej policzyć średnia z całego dnia
        foreach (Record record in recordsData)
        {
            temperatura.Add(record.TemperaturaDobowa);
            wilgotność.Add(record.WilgotnośćWzględna);
            wiatr.Add(record.PrędkośćWiatru);
            zachmurzenie.Add(record.ZachmurzenieOgólne);
            ciśnienie.Add(record.CiśnienieNaPoziomieStacji);
        }

        // Przygotowanie danych wejściowych dla modelu predykcyjnego.
        inputData = new InputData
        {
            //W bazie posiadiamy tabele zawierająca x rekordów, których nazwa jest równa wyszukiwanej
            //Każdy rekord który znajduje się w bazie rózni się tylko danymi pomiarowymi, są wartości które z przyczyn logicznych nie zmieniają się, takie jak lokalizacja czy nazwa
            //stąd w niektórych przypisaniach pojawia się konkretny index np. First() lub Last() - nie ma to znaczenia, który to jest 

            //Poniżej nastepujące przypisania do nowego obiektu InputData
            Id = recordsData.First().Id,                                                                           // Przypisanie Id
            Station_Name = recordsData.First().Station_Name,                                                       // Przypisianie nazwy stacji
            Lon = recordsData.First().Lon,                                                                         // Przypisianie Dlugości Geograficznej
            Lat = recordsData.First().Lat,                                                                         // Przypisianie Szerokości Geograficznej
            Station_Timestamp = recordsData.First().Station_Timestamp,                                             // Przypisianie Czasu pomiaru
            Location = FindClosestLocation(airLocList, recordsData.First().Lon, recordsData.First().Lat),          // Znalezienie najbliższej zmiennej kategorycznej location dla podanej długości i szerokości geograficznej
            MinimalnaTemperaturaPrzyGruncie = temperatura.Min() - (0.1 * recordsData.Last().SumaOpaduDzień),       // Wyliczenie minimalnej temperatury przy gruncie
            RóznicaMaksymalnejIMinimalnejTemperatury = (temperatura.Max() - temperatura.Min()),                    // Obliczenie róznicy temperatury minimalnej i maksymalnej
            ŚredniaTemperaturaDobowa = countMean(temperatura),                                                     // Policzenie sredniej temperatury dobowej na podstawie zapisanych informacji w liscie tempertura
            ŚredniaDobowaWilgotnośćWzględna = countMean(wilgotność),                                               // Policzenie sredniej wilgotności dobowej na podstawie zapisanych informacji w liscie wilgotnosc
            ŚredniaDobowaPrędkośćWiatru = countMean(wiatr),                                                        // Policzenie sredniej predkości wiatru na podstawie zapisanych informacji w liscie wiatr
            ŚrednieDoboweZachmurzenieOgólne = countMean(zachmurzenie),                                             // Policzenie sredniego zachmurzenia dobowego na podstawie zapisanych informacji w liscie zachmurzenie
            ŚredniaDoboweCiśnienieNaPoziomieStacji = countMean(ciśnienie),                                         // Policzenie sredniej temperatury dobowej na podstawie zapisanych informacji w liscie tempertura
            SumaOpaduDzień = recordsData.Last().SumaOpaduDzień,                                                    // Przypisanie sumy opadów dziennych
            DzieńRoku = DateTime.Now.DayOfYear,                                                                    // Przypisanie nowej wartości dla aktualnego dnia roku 
            DzieńTygodnia = Convert.ToInt16(DateTime.Now.DayOfWeek),                                               // Prypisanie nowej wartości dla aktualnego dnia tygodnia
            Value = 0,                                                                                             // Zainicjowanie pola Value zerem 
            ValueCat = 0,                                                                                          // Zainicjowanie pola ValueCat zerem
            Prediction = 1                                                                                        // Zainicjowanie pola Prediction zerem
        };

        // Przygotowanie danych dla predykcji.
        PredictionData toRequestPrediction = new PredictionData
        {
            //Przypisanie odpowiednich wartości do wartości obiektu PredictionData w celu przekazanie poprawnych danych wejsciowych 

            MinimalnaTemperaturaPrzyGruncie = inputData.MinimalnaTemperaturaPrzyGruncie,
            RóznicaMaksymalnejIMinimalnejTemperatury = inputData.RóznicaMaksymalnejIMinimalnejTemperatury,
            ŚredniaTemperaturaDobowa = inputData.ŚredniaTemperaturaDobowa,
            ŚredniaDobowaWilgotnośćWzględna = inputData.ŚredniaDobowaWilgotnośćWzględna,
            ŚredniaDobowaPrędkośćWiatru = inputData.ŚredniaDobowaPrędkośćWiatru,
            ŚrednieDoboweZachmurzenieOgólne = inputData.ŚrednieDoboweZachmurzenieOgólne,
            ŚredniaDoboweCiśnienieNaPoziomieStacji = inputData.ŚredniaDoboweCiśnienieNaPoziomieStacji,
            SumaOpaduDzień = inputData.SumaOpaduDzień,
            Location = inputData.Location,
            DzieńRoku = inputData.DzieńRoku,
            DzieńTygodnia = inputData.DzieńTygodnia,
            Value = inputData.Value,
            ValueCat = inputData.ValueCat
        };

        // Wykonanie predykcji i uzyskanie wyników
        
        //Parametry przekazania to inputData
        outputData = await _calculation.fillPollutionValues(inputData, recordsData.Last(), toRequestPrediction, pollution_Type);

        // Zwrócenie wyników predykcji.
        return Ok(outputData);
    }

        private object CalculateAgeInSeconds()
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Punkt końcowy, który pobiera wszystkie prognozy dla konkretnej stacji.
        /// Wymaga uwierzytelnienia.
        /// </summary>
        /// <param name="station_Name">Nazwa stacji, dla której chcemy pobrać prognozy.</param>
        /// <returns>Asynchroniczne zadanie reprezentujące operację i przechowujące wynik działania.</returns>
    [Authorize]
    [HttpGet("GetAllPredictionsForStation")]
    [OutputCache(PolicyName = "base")]
    public async Task<IActionResult> GetAllPredictionsForStation(string station_Name)
    {
        List<OutputData> outputDatas = new List<OutputData>();

        // Dla każdego typu zanieczyszczenia z globalnej listy.
        foreach (string pollution_Type in _globalService.GetGlobalList())
        {
            _logger.LogInformation("AirPollutionController got GetAllPredictionsForStation() method started");

            // Sprawdzenie i walidacja argumentu stacji.
            var info = await _service.validation.AreArgumentsValid(station_Name, _service.data, _globalService);

            if (info is not null)
            {
                _logger.LogWarning("Brakuje argumentu wywołaniu lub Dostarczono złe argumenty wywołania");
                _logger.LogInformation("Dostępne stację oraz zanieczyszczenia");
                _logger.LogInformation(JsonConvert.SerializeObject(info, Formatting.Indented));
                return BadRequest("Brakuję argumentu wywołania lub Dostarczone argumenty są nieodpowiednie" + "\n" +
                    "Dostępne stację oraz zanieczyszczenia:" + "\n" + JsonConvert.SerializeObject(info, Formatting.Indented));
            }

            // Inicjalizacja zmiennych i list.
            InputData inputData = new InputData();
            OutputData outputData = new OutputData();
            List<Record> recordsData = await _service.data.GetByCondition(e => e.Station_Name == station_Name).ToListAsync();
            List<AirLoc> airLocList = await _service.airLoc.GetByCondition(e => e.PollutionType == pollution_Type).ToListAsync();

            List<double> temperatura = new List<double>();
            List<double> wilgotność = new List<double>();
            List<double> wiatr = new List<double>();
            List<double> zachmurzenie = new List<double>();
            List<double> ciśnienie = new List<double>();

            // Sprawdzenie, czy istnieją rekordy w tabelach Record oraz AirLoc.
            if (!(recordsData.Any() & airLocList.Any()))
                return BadRequest("Brak rekordów w tabelach Record lub AirLoc");

            // Przypisanie parametrów pomiarowych do odpowiednich list.
            foreach (Record record in recordsData)
            {
                temperatura.Add(record.TemperaturaDobowa);
                wilgotność.Add(record.WilgotnośćWzględna);
                wiatr.Add(record.PrędkośćWiatru);
                zachmurzenie.Add(record.ZachmurzenieOgólne);
                ciśnienie.Add(record.CiśnienieNaPoziomieStacji);
            }

            // Przygotowanie danych wejściowych dla modelu predykcyjnego.
            inputData = new InputData
            {
                Id = recordsData.First().Id,
                Station_Name = recordsData.First().Station_Name,
                Lon = recordsData.First().Lon,
                Lat = recordsData.First().Lat,
                Station_Timestamp = recordsData.First().Station_Timestamp,
                Location = FindClosestLocation(airLocList, recordsData.First().Lon, recordsData.First().Lat),
                MinimalnaTemperaturaPrzyGruncie = temperatura.Min() - (0.1 * recordsData.Last().SumaOpaduDzień),
                RóznicaMaksymalnejIMinimalnejTemperatury = (temperatura.Max() - temperatura.Min()),
                ŚredniaTemperaturaDobowa = countMean(temperatura),
                ŚredniaDobowaWilgotnośćWzględna = countMean(wilgotność),
                ŚredniaDobowaPrędkośćWiatru = countMean(wiatr),
                ŚrednieDoboweZachmurzenieOgólne = countMean(zachmurzenie),
                ŚredniaDoboweCiśnienieNaPoziomieStacji = countMean(ciśnienie),
                SumaOpaduDzień = recordsData.Last().SumaOpaduDzień,
                DzieńRoku = DateTime.Now.DayOfYear,
                DzieńTygodnia = Convert.ToInt16(DateTime.Now.DayOfWeek),
                Value = 0,
                ValueCat = 0,
                Prediction = 0
            };

            // Przygotowanie danych dla predykcji.
            PredictionData toRequestPrediction = new PredictionData
            {
                MinimalnaTemperaturaPrzyGruncie = inputData.MinimalnaTemperaturaPrzyGruncie,
                RóznicaMaksymalnejIMinimalnejTemperatury = inputData.RóznicaMaksymalnejIMinimalnejTemperatury,
                ŚredniaTemperaturaDobowa = inputData.ŚredniaTemperaturaDobowa,
                ŚredniaDobowaWilgotnośćWzględna = inputData.ŚredniaDobowaWilgotnośćWzględna,
                ŚredniaDobowaPrędkośćWiatru = inputData.ŚredniaDobowaPrędkośćWiatru,
                ŚrednieDoboweZachmurzenieOgólne = inputData.ŚrednieDoboweZachmurzenieOgólne,
                ŚredniaDoboweCiśnienieNaPoziomieStacji = inputData.ŚredniaDoboweCiśnienieNaPoziomieStacji,
                SumaOpaduDzień = inputData.SumaOpaduDzień,
                Location = inputData.Location,
                DzieńRoku = inputData.DzieńRoku,
                DzieńTygodnia = inputData.DzieńTygodnia,
                Value = inputData.Value,
                ValueCat = inputData.ValueCat
            };

            // Wykonanie oczekiwanych predykcji w serwisie calculation i dodanie wyników do listy.
            outputData = await _calculation.fillPollutionValues(inputData, recordsData.Last(), toRequestPrediction, pollution_Type);
            outputDatas.Add(outputData);
        }

        // Zwrócenie listy z wynikami predykcji dla wszystkich typów zanieczyszczeń, dla okreslonej stacji
        return Ok(outputDatas);
    }


        


        /// <summary>
        /// Punkt końcowy, który pobiera wszystkie rekordy dotyczące zanieczyszczenia powietrza.
        /// </summary>
        /// <returns>Asynchroniczne zadanie reprezentujące operację i przechowujące wynik działania.</returns>
        [HttpGet("GetAllRecords")]
        public async Task<IActionResult> GetAll()
        {   
            _logger.LogInformation("AirPollutionController got GetAll() method started");

            // Pobranie wszystkich rekordów z bazy danych i przekształcenie ich do nowego formatu.
            var result = (await _service.data.GetDataAsync().ToListAsync()).Select(e => 
                new Record
                {
                    Id_sql = e.Id_sql,
                    Id=e.Id,
                    Station_Name = e.Station_Name,
                    Station = e.Station,
                    Lon = e.Lon,
                    Lat = e.Lat,
                    Station_Timestamp = e.Station_Timestamp,
                    TemperaturaDobowa = e.TemperaturaDobowa,
                    WilgotnośćWzględna = e.WilgotnośćWzględna,
                    PrędkośćWiatru = e.PrędkośćWiatru,
                    ZachmurzenieOgólne = e.ZachmurzenieOgólne,
                    CiśnienieNaPoziomieStacji = e.CiśnienieNaPoziomieStacji,
                    SumaOpaduDzień = e.SumaOpaduDzień,
                    ValuePm10 = e.ValuePm10,
                    ValueCatPm10 = e.ValueCatPm10,
                    ValuePm25 = e.ValuePm25,
                    ValueCatPm25 = e.ValueCatPm25,
                    ValueNo2 = e.ValueNo2,
                    ValueCatNo2 = e.ValueCatNo2,
                    ValueO3 = e.ValueO3,
                    ValueCatO3 = e.ValueCatO3,
                    ValueSo2 = e.ValueSo2,
                    ValueCatSo2 = e.ValueCatSo2,
                    Session = e.Session
                }
            );

            // Zwrócenie przekształconych rekordów.
            return Ok(result);
        }

        


        /// <summary>
        /// Punkt końcowy, który pobiera rekord zanieczyszczenia powietrza na podstawie podanego identyfikatora.
        /// </summary>
        /// <param name="Id_sql">Identyfikator rekordu w bazie danych.</param>
        /// <returns>Asynchroniczne zadanie reprezentujące operację i przechowujące wynik działania.</returns>
        [HttpGet("GetRecordById")]
        public async Task<IActionResult> GetById(int Id_sql) {
            _logger.LogInformation("AirPollutionController got GetById() method started");
            
            // Pobranie rekordu o określonym Id_sql z bazy danych i zwrócenie go jako wynik zapytania HTTP.
            return Ok(await _service.data.GetByCondition(e=>e.Id_sql == Id_sql).FirstOrDefaultAsync());
        }

        /// <summary>
        /// Punkt końcowy umożliwiający dodawanie nowego rekordu zanieczyszczenia powietrza.
        /// </summary>
        /// <param name="body">Obiekt reprezentujący nowy rekord zanieczyszczenia powietrza.</param>
        /// <returns>Asynchroniczne zadanie reprezentujące operację i przechowujące wynik działania.</returns>
        [Authorize]
        [HttpPost("CreateRecord")]
        public async Task<IActionResult> CreateRecord([FromBody]Record body){

             Console.WriteLine(body.ValuePm25);

            _logger.LogInformation("AirPollutionController got CreateRecord() method started");

            // Walidacja ciała żądania przy użyciu modelu ModelState.
            // if(!_service.validation.CheckBodyValidation(body,ModelState,out var errorResponse)){
            //     _logger.LogError(JsonConvert.SerializeObject(errorResponse, Formatting.Indented));
            //     return BadRequest(errorResponse); // Zwróć listę komunikatów błędów jako część odpowiedzi API
            // }

            // Stworzenie nowego rekordu na podstawie danych z ciała żądania.
            var newData = new Record {
                Id= body.Id,
                Station_Name = body.Station_Name,
                Station = body.Station,
                Lon = body.Lon,
                Lat = body.Lat,
                Station_Timestamp = body.Station_Timestamp,
                TemperaturaDobowa = body.TemperaturaDobowa,
                WilgotnośćWzględna = body.WilgotnośćWzględna,
                PrędkośćWiatru = body.PrędkośćWiatru,
                ZachmurzenieOgólne = body.ZachmurzenieOgólne,
                CiśnienieNaPoziomieStacji = body.CiśnienieNaPoziomieStacji,
                SumaOpaduDzień = body.SumaOpaduDzień,
                ValuePm10 = body.ValuePm10,
                ValueCatPm10 = body.ValueCatPm10,
                ValuePm25 = body.ValuePm25,
                ValueCatPm25 = body.ValueCatPm25,
                ValueNo2 = body.ValueNo2,
                ValueCatNo2 = body.ValueCatNo2,
                ValueO3 = body.ValueO3,
                ValueCatO3 = body.ValueCatO3,
                ValueSo2 = body.ValueSo2,
                ValueCatSo2 = body.ValueCatSo2,
                Session = body.Session
            };
                        
            // Dodanie rekordu do bazy danych.
            _service.data.Create(newData);

            // Zapisanie zmian w bazie danych.
            await _service.saveAsync();

            // Zwrócenie informacji o utworzeniu rekordu jako wynik zapytania HTTP.
            return Ok("Record Created");
        }




        /// <summary>
        /// Punkt końcowy umożliwiający pobranie klucza API.
        /// </summary>
        /// <param name="body">Obiekt zawierający dane użytkownika (tu: email).</param>
        /// <returns>Asynchroniczne zadanie reprezentujące operację pobierania klucza API.</returns>
        
        [HttpPost("GetApiKey")]
        public async Task<IActionResult> GetApiKey(UserDTO body){

            User user = await _service.user.GetByCondition(e=>e.Email == body.Email).FirstOrDefaultAsync();


            if(!(user is null)){
                return BadRequest("This user already exists");
            }
            
            var ipAdress = Request.HttpContext.Connection.RemoteIpAddress;
            //walidacja ciała żądania
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }


            var userNew = new User{
                Email = body.Email,
                IP = Convert.ToString(ipAdress),
                TimeStamp = DateTime.Now
            };


            _service.user.Create(userNew);

            await _service.saveAsync();

            string apiKey = _configuaration.GetValue<string>("AuthSettings:AuthKey");
            
            return Ok(apiKey);
        }




            




        /// <summary>
        /// Punkt końcowy umożliwiający usuwanie rekordów zanieczyszczenia powietrza.
        /// Metoda dostępna tylko dla uwierzytelnionych użytkowników.
        /// </summary>
        /// <returns>Asynchroniczne zadanie reprezentujące operację usuwania rekordów.</returns>
        [Authorize]
        [HttpDelete("DeleteRecords")]
        public async Task<IActionResult> DeleteRecords(){
            _logger.LogInformation("AirPollutionController got DeleteRecord() method started");

            // Pobranie wszystkich rekordów z bazy danych i utworzenie ich kopii w nowej kolekcji.
            IEnumerable<Record> records = (await _service.data.GetDataAsync().ToListAsync()).Select(e => 
            new Record
            {
                Id_sql = e.Id_sql,
                Id=e.Id,
                Station_Name = e.Station_Name,
                Station = e.Station,
                Lon = e.Lon,
                Lat = e.Lat,
                Station_Timestamp = e.Station_Timestamp,
                TemperaturaDobowa = e.TemperaturaDobowa,
                WilgotnośćWzględna = e.WilgotnośćWzględna,
                PrędkośćWiatru = e.PrędkośćWiatru,
                ZachmurzenieOgólne = e.ZachmurzenieOgólne,
                CiśnienieNaPoziomieStacji = e.CiśnienieNaPoziomieStacji,
                SumaOpaduDzień = e.SumaOpaduDzień,
                ValuePm10 = e.ValuePm10,
                ValueCatPm10 = e.ValueCatPm10,
                ValuePm25 = e.ValuePm25,
                ValueCatPm25 = e.ValueCatPm25,
                ValueNo2 = e.ValueNo2,
                ValueCatNo2 = e.ValueCatNo2,
                ValueO3 = e.ValueO3,
                ValueCatO3 = e.ValueCatO3,
                ValueSo2 = e.ValueSo2,
                ValueCatSo2 = e.ValueCatSo2,
                Session = e.Session
            });

            // Rzucenie wyjątku, jeśli nie ma żadnych rekordów w bazie danych.
            if(!records.Any()){
                throw new Exception("There are no records in the database");
            }
            
            // Iteracyjne usuwanie rekordów z bazy danych.
            foreach (Record record in records){
                _service.data.Delete(record);
            }

            // Zapisanie zmian w bazie danych.
            await _service.saveAsync();

            _logger.LogInformation("Recordy usunięte");
            
            // Zwrócenie informacji o usunięciu rekordów jako wynik zapytania HTTP.
            return Ok("Recordy usunięte");
        }



                
            
        /// <summary>
        /// Metoda obliczająca średnią wartość z listy liczb zmiennoprzecinkowych.
        /// </summary>
        /// <param name="list">Lista liczb zmiennoprzecinkowych, dla których ma być obliczona średnia.</param>
        /// <returns>Średnia wartość z listy lub 0, jeśli lista jest pusta.</returns>
        private static double countMean(List<double> list) {
            var mean = 0.0;

            foreach (double value in list) {
                mean = mean + value;
            }

            mean = mean / list.Count; // Oblicz średnią dzieląc sumę przez liczbę elementów.
            return mean;
        }



        /// <summary>
        /// Metoda znajdująca najbliższą lokalizację na podstawie współrzędnych geograficznych.
        /// </summary>
        /// <param name="locations">Lista lokalizacji, w której ma być znaleziona najbliższa lokalizacja.</param>
        /// <param name="lon">Długość geograficzna punktu referencyjnego.</param>
        /// <param name="lat">Szerokość geograficzna punktu referencyjnego.</param>
        /// <returns>Indeks najbliższej lokalizacji w liście, lub -1 jeśli lista jest pusta.</returns>
        private static int FindClosestLocation(List<AirLoc> locations, double lon, double lat) {
            AirLoc closestLocation = null;
            double minDistance = double.MaxValue;

            foreach (var location in locations) {
                double distance = CalculateDistance(location, lon, lat); // Oblicz odległość między lokalizacją a danymi współrzędnymi.
                if (distance < minDistance) {
                    minDistance = distance; // Zaktualizuj minimalną odległość.
                    closestLocation = location; // Zaktualizuj najbliższą lokalizację.
                }
            }

            return closestLocation.CategoryNumber; // Zwróć numer kategorii najbliższej lokalizacji.
        }


        /// <summary>
        /// Metoda obliczająca odległość pomiędzy dwoma punktami geograficznymi.
        /// </summary>
        /// <param name="loc1">Pierwsza lokalizacja, dla której obliczana jest odległość.</param>
        /// <param name="lon">Długość geograficzna punktu referencyjnego.</param>
        /// <param name="lat">Szerokość geograficzna punktu referencyjnego.</param>
        /// <returns>Obliczona odległość pomiędzy dwoma punktami.</returns>
        private static double CalculateDistance(AirLoc loc1, double lon, double lat) {
            double lonDiff = lon - loc1.Longitude; // Różnica długości geograficznej między punktami.
            double latDiff = lat - loc1.Latitude; // Różnica szerokości geograficznej między punktami.
            
            // Oblicz odległość
            return Math.Sqrt(lonDiff * lonDiff + latDiff * latDiff);
        }



    
    


                
                
                

            

            


            


                








            
            



            

            
            
            

                

                
                


    }
}