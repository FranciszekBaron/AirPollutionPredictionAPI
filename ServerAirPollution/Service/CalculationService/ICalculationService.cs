using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirPollutionPrediction.Model;
using ServerAirPollution.Model;

namespace ServerAirPollution.Service.CalculationService
{
    /// <summary>
    /// Interfejs służący do deklaracji metod związanych z obliczeniami dotyczącymi zanieczyszczeń powietrza.
    /// </summary>
    public interface ICalculationService
    {
        /// <summary>
        /// Metoda wypełniająca dane dotyczące zanieczyszczeń i ich prognoz na podstawie dostarczonych danych wejściowych.
        /// </summary>
        /// <param name="inputData">Dane wejściowe zawierające informacje o stacji i parametrach pomiarowych.</param>
        /// <param name="record">Rekord pomiarowy zawierający szczegóły pomiaru.</param>
        /// <param name="toRequestPrediction">Dane prognozowe do wykorzystania w przewidywaniach.</param>
        /// <param name="pollution_Type">Rodzaj zanieczyszczenia powietrza.</param>
        /// <returns>Obiekt zawierający informacje o stacji, znaczniku czasowym i wartościach zanieczyszczeń.</returns>
        Task<OutputData> fillPollutionValues(InputData inputData, Record record, PredictionData toRequestPrediction, string pollution_Type);
    }

}