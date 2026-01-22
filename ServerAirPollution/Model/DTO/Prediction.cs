using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAirPollution.Model
{
    /// <summary>
    /// Klasa reprezentująca wynik prognozy, zawierająca informacje o przewidywanej wartości pomiarowej,
    /// kategorii przewidywanej wartości oraz przewidywanej kategorii wartości.
    /// </summary>
    public class Prediction
    {
        /// <summary>
        /// Przewidywana wartość pomiarowa.
        /// </summary>
        public int prediction { get; set; }

        /// <summary>
        /// Kategoria przewidywanej wartości.
        /// </summary>
        public string predictionCategory { get; set; }

        /// <summary>
        /// Przewidywana kategoria wartości.
        /// </summary>
        public override string ToString()
        {
            return predictionCategory;
        }
    }

}