using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerAirPollution.Model;
using ServerAirPollution.Model.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Reflection;
using AirPollutionPrediction.Model;
using ServerAirPollution.Service.GlobalService;

namespace ServerAirPollution.Service.ValidationService
{
    /// <summary>
    /// Serwis do przeprowadzania walidacji danych i argumentów.
    /// </summary>
    public class ValidationService : IValidService
    {

        private readonly AirPollutionDbContext _dbContext;

        public ValidationService(AirPollutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <inheritdoc/>
        
        public bool CheckBodyValidation(Record body, ModelStateDictionary modelState, out ValidationErrorResponse errors)
        {
            
           
            
            
            
            // Inicjalizacja obiektu, który przechowa komunikaty błędów walidacji.
            errors = new ValidationErrorResponse();

            // Sprawdzenie, czy ciało żądania nie jest puste.
            if (body == null)
            {
                modelState.AddModelError("Body", "Ciało żądania nie może być puste.");
                errors.Errors.Add("Ciało żądania nie może być puste.");
                return false;
            }

            // Sprawdzenie, czy modele walidacji zawierają błędy.
            if (!modelState.IsValid)
            {
                // Pobranie listy komunikatów błędów z modeli walidacji.
                var err = modelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage)
                                    .ToList();

                // Pobranie listy pól, które zawierają nieprawidłowe dane.
                var invalidFields = modelState.Keys.ToList();

                // Przypisanie komunikatów błędów i nieprawidłowych pól do obiektu errors.
                errors.Errors = err;
                errors.InvalidFields = invalidFields;

                return false;
            }

            // Jeśli walidacja przeszła pomyślnie, zwróć true.
            return true;
        }


       


        /// <inheritdoc/>
        public async Task<ValidationArgumentFormats> AreArgumentsValid(string stationName, string pollutionType, IDataService service, IGlobalService globalService)
        {
            // Inicjalizacja obiektu, który przechowa formaty argumentów walidacji.
            ValidationArgumentFormats displayFormat = new ValidationArgumentFormats();

            // Pobranie wszystkich rekordów związanych z danymi pomiarowymi.
            var records = (await service.GetDataAsync().ToListAsync()).Select(e =>
                new Record
                {
                    // Skopiowanie odpowiednich pól rekordu.
                    Id_sql = e.Id_sql,
                    Id = e.Id,
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

            // Inicjalizacja listy stacji.
            displayFormat.Stations = new List<string>();

            // Pobranie listy globalnych rodzajów zanieczyszczeń.
            displayFormat.PollutionTypes = globalService.GetGlobalList();

            // Dodanie nazw stacji do listy stacji.
            foreach (Record record in records)
            {
                displayFormat.Stations.Add(record.Station_Name);
            }

            // Jeśli stationName lub pollutionType jest puste lub nieprawidłowe, zwróć formaty argumentów walidacji.
            if (string.IsNullOrEmpty(stationName) || string.IsNullOrEmpty(pollutionType) ||
                !displayFormat.Stations.Contains(stationName) || !displayFormat.PollutionTypes.Contains(pollutionType))
            {
                return displayFormat;
            }

            // W przeciwnym razie zwróć null, co oznacza, że argumenty są poprawne.
            return null;
        }



    

        /// <inheritdoc/>
        public async Task<ValidationArgumentFormats> AreArgumentsValid(string stationName, IDataService service, IGlobalService globalService)
        {
            // Inicjalizacja obiektu, który przechowa formaty argumentów walidacji.
            ValidationArgumentFormats displayFormat = new ValidationArgumentFormats();

            // Pobranie wszystkich rekordów związanych z danymi pomiarowymi.
            var records = (await service.GetDataAsync().ToListAsync()).Select(e =>
                new Record
                {
                    // Skopiowanie odpowiednich pól rekordu.
                    Id_sql = e.Id_sql,
                    Id = e.Id,
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

            // Inicjalizacja listy stacji.
            displayFormat.Stations = new List<string>();

            // Dodanie nazw stacji do listy stacji.
            foreach (Record record in records)
            {
                displayFormat.Stations.Add(record.Station_Name);
            }

            // Jeśli stationName jest puste lub nieprawidłowe, zwróć formaty argumentów walidacji.
            if (string.IsNullOrEmpty(stationName) || !displayFormat.Stations.Contains(stationName))
            {
                return displayFormat;
            }

            // W przeciwnym razie zwróć null, co oznacza, że argumenty są poprawne.
            return null;
        }



        /// <inheritdoc/>

        public bool CheckPollutionExists(Record body, string pollution_Type)
        {
            // Sprawdzenie rodzaju zanieczyszczenia i wartości kategorii w zależności od niego.
            switch (pollution_Type)
            {
                case "PM10":
                    // Jeśli kategoria wartości dla PM10 to "null", zwróć false.
                    if (body.ValueCatPm10 == "null")
                        return false;
                    return false;
                case "PM2.5":
                    // Jeśli kategoria wartości dla PM2.5 to "null", zwróć false.
                    if (body.ValueCatPm25 == "null")
                        return false;
                    break;
                case "SO2":
                    // Jeśli kategoria wartości dla SO2 to "null", zwróć false.
                    if (body.ValueCatO3 == "null")
                        return false;
                    break;
                case "O3":
                    // Jeśli kategoria wartości dla O3 to "null", zwróć false.
                    if (body.ValueCatSo2 == "null")
                        return false;
                    break;
                case "NO2":
                    // Jeśli kategoria wartości dla NO2 to "null", zwróć false.
                    if (body.ValueCatNo2 == "null")
                        return false;
                    break;
            }
            
            // Jeśli żadna z powyższych warunków nie zachodzi, zwróć true, co oznacza, że zanieczyszczenie istnieje.
            return true;
        }
        
    }
}



                
            
            








