using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAirPollution.Service.GlobalService
{
    /// <summary>
    /// Interfejs definiujący operacje dostępu do globalnej listy.
    /// </summary>
    public interface IGlobalService
    {
        /// <summary>
        /// Pobiera zawartość globalnej listy.
        /// </summary>
        /// <returns>Zawartość globalnej listy.</returns>
        List<string> GetGlobalList();

        /// <summary>
        /// Dodaje wartość do globalnej listy.
        /// </summary>
        /// <param name="value">Wartość do dodania.</param>
        void AddToGlobalList(string value);
    }


}