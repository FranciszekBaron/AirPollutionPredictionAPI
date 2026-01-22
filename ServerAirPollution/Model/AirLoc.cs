using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAirPollution.Model
{
    /// <summary>
    /// Klasa reprezentująca spis informacji o przydziale Obszarów zalokowania do odpowiedniego zanieczyszczenia powietrza.
    /// Posiada pola takie jak nazwy lokalizacji, długości geograficznej, szerokości geograficznej i numeru kategorii.
    /// </summary>
    public class AirLoc
    {
        /// <summary>
        /// Identyfikator lokalizacji.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rodzaj zanieczyszczenia.
        /// </summary>
        public string PollutionType { get; set; }

        /// <summary>
        /// Nazwa lokalizacji.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Długość geograficzna.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Szerokość geograficzna.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Numer kategorii.
        /// </summary>
        public int CategoryNumber { get; set; }
    }


}

        