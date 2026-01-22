using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAirPollution.Model.DTO
{
    /// <summary>
    /// Klasa reprezentująca pojedynczą wartość pomiarową, która zawiera informacje o rodzaju zanieczyszczenia,
    /// wartości pomiarowej, kategorii wartości oraz prognozie na kolejny dzień.
    /// Klasa ma na celu ułatwienie wyświetlania efektów pracy określonych endpointów.
    /// </summary>
    public class Values
    {
        /// <summary>
        /// Rodzaj zanieczyszczenia.
        /// </summary>
        public string pollution_Type { get; set; }

        /// <summary>
        /// Wartość pomiarowa.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Kategoria wartości.
        /// </summary>
        public string ValueCat { get; set; }

        /// <summary>
        /// Prognoza wartości na kolejny dzień.
        /// </summary>
        public string ValueForNextDay { get; set; }
    }

}
