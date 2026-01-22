using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirPollutionPrediction.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServerAirPollution.Model;
using ServerAirPollution.Model.Validation;
using ServerAirPollution.Service.GlobalService;

namespace ServerAirPollution.Service.ValidationService
{
    

    /// <summary>
    /// Interfejs serwisu do przeprowadzania walidacji danych i argumentów.
    /// </summary>
    public interface IValidService
    {
        /// <summary>
        /// Sprawdza poprawność walidacji obiektu rekordu pomiarowego.
        /// </summary>
        /// <param name="body">Obiekt rekordu do sprawdzenia.</param>
        /// <param name="modelState">Stan modelu, w którym zostaną zgromadzone błędy walidacji.</param>
        /// <param name="errors">Obiekt zawierający komunikaty błędów walidacji.</param>
        /// <returns>True, jeśli obiekt jest poprawnie zwalidowany; w przeciwnym razie false.</returns>
        bool CheckBodyValidation(Record body, ModelStateDictionary modelState, out ValidationErrorResponse errors);

        /// <summary>
        /// Sprawdza poprawność argumentów i formatów dla stacji i rodzaju zanieczyszczenia.
        /// </summary>
        /// <param name="stationName">Nazwa stacji.</param>
        /// <param name="pollutionType">Rodzaj zanieczyszczenia.</param>
        /// <param name="service">Serwis do operacji na danych.</param>
        /// <param name="globalService">Globalny serwis do zarządzania danymi.</param>
        /// <returns>Obiekt zawierający formaty argumentów walidacyjnych.</returns>
        Task<ValidationArgumentFormats> AreArgumentsValid(string stationName, string pollutionType, IDataService service, IGlobalService globalService);

        /// <summary>
        /// Sprawdza poprawność argumentów i formatów dla stacji.
        /// </summary>
        /// <param name="stationName">Nazwa stacji.</param>
        /// <param name="service">Serwis do operacji na danych.</param>
        /// <param name="globalService">Globalny serwis do zarządzania danymi.</param>
        /// <returns>Obiekt zawierający formaty argumentów walidacyjnych.</returns>
        Task<ValidationArgumentFormats> AreArgumentsValid(string stationName, IDataService service, IGlobalService globalService);

        /// <summary>
        /// Sprawdza, czy dany rodzaj zanieczyszczenia istnieje w rekordzie pomiarowym.
        /// </summary>
        /// <param name="body">Obiekt rekordu pomiarowego.</param>
        /// <param name="pollution_Type">Rodzaj zanieczyszczenia do sprawdzenia.</param>
        /// <returns>True, jeśli rodzaj zanieczyszczenia istnieje; w przeciwnym razie false.</returns>
        bool CheckPollutionExists(Record body, string pollution_Type);
    }

}