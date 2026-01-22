using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAirPollution.Service.GlobalService
{
    /// <summary>
    /// Klasa implementująca interfejs <c>IGlobalService</c>, zapewniająca funkcjonalność globalnej listy.
    /// </summary>
    public class GlobalService : IGlobalService
    {
        private List<string> globalList = new List<string>();

        /// <summary>
        /// Pobiera zawartość globalnej listy.
        /// </summary>
        /// <returns>Zawartość globalnej listy.</returns>
        public List<string> GetGlobalList()
        {
            return globalList;
        }

        /// <summary>
        /// Dodaje wartość do globalnej listy.
        /// </summary>
        /// <param name="value">Wartość do dodania.</param>
        public void AddToGlobalList(string value)
        {
            globalList.Add(value);
        }
    }


}