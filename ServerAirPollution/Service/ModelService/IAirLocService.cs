using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ServerAirPollution.Model;

namespace ServerAirPollution.Service
{

    /// <summary>
    /// Interfejs serwisu obsługującego operacje na danych lokalizacji powietrza.
    /// </summary>
    public interface IAirLocService
    {
        /// <summary>
        /// Pobiera dane dotyczące lokalizacji powietrza.
        /// </summary>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        IQueryable<AirLoc> GetDataAsync();

        /// <summary>
        /// Aktualizuje istniejącą lokalizację powietrza.
        /// </summary>
        /// <param name="enity">Obiekt lokalizacji do zaktualizowania.</param>
        void Update(AirLoc enity);

        /// <summary>
        /// Tworzy nową lokalizację powietrza.
        /// </summary>
        /// <param name="entity">Obiekt lokalizacji do dodania.</param>
        void Create(AirLoc entity);

        /// <summary>
        /// Pobiera dane lokalizacji powietrza spełniające określone kryteria.
        /// </summary>
        /// <param name="condition">Warunek do spełnienia.</param>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        IQueryable<AirLoc> GetByCondition(Expression<Func<AirLoc, bool>> condition);
    }

}