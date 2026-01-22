using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using ServerAirPollution.Model;

namespace AirPollutionPrediction.Model
{
    
    /// <summary>
    /// Klasa reprezentująca dane wejściowe, które zawierają informacje o identyfikatorze, nazwie stacji,
    /// długości geograficznej, szerokości geograficznej, znaczniku czasowym stacji oraz prognozie.
    /// Klasa dziedziczy po klasie PredictionData.
    /// </summary>
    public class InputData : PredictionData
    {
        /// <summary>
        /// Wymagane pole - identyfikator danych wejściowych.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Nazwa stacji pomiarowej.
        /// </summary>
        public string Station_Name { get; set; }

        /// <summary>
        /// Długość geograficzna.
        /// </summary>
        public double Lon { get; set; }

        /// <summary>
        /// Szerokość geograficzna.
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Znacznik czasowy stacji pomiarowej.
        /// </summary>
        public DateTime Station_Timestamp { get; set; }

        /// <summary>
        /// Prognoza.
        /// </summary>
        public int Prediction { get; set; }
    }


}


        

        


