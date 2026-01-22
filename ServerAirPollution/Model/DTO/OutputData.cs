using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerAirPollution.Model.DTO;

namespace ServerAirPollution.Model
{
    /// <summary>
    /// Klasa reprezentująca dane wyjściowe, zawierająca informacje o nazwie stacji pomiarowej,
    /// znaczniku czasowym stacji oraz liście wartości pomiarowych.
    /// </summary>
    public class OutputData
    {
        /// <summary>
        /// Nazwa stacji pomiarowej.
        /// </summary>
        public string Station_Name { get; set; }

        /// <summary>
        /// Znacznik czasowy stacji pomiarowej.
        /// </summary>
        public DateTime Station_Timestamp { get; set; }

        /// <summary>
        /// Lista zawierająca wartości pomiarowe.
        /// </summary>
        public List<Values> Values { get; set; }
    }

}
