using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerAirPollution.Service.GlobalService;

namespace ServerAirPollution.Model.Validation
{
    /// <summary>
    /// Klasa reprezentująca formaty argumentów używane do wyświetlania błędów walidacji.
    /// </summary>
    public class ValidationArgumentFormats
    {
        /// <summary>
        /// Lista przechowująca dostępne nazwy stacji.
        /// </summary>
        public List<string> Stations = new List<string>();

        /// <summary>
        /// Lista przechowująca dostępne rodzaje zanieczyszczeń.
        /// </summary>
        public List<string> PollutionTypes = new List<string>();
    }

}
