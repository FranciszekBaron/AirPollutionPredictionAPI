using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirPollutionPrediction.Model;
using Microsoft.AspNetCore.Mvc;
using ServerAirPollution.Model;

namespace ServerAirPollution.Service
{
   
    /// <summary>
    /// Interfejs serwisu obsługującego operacje na danych rekordów pomiarowych.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Pobiera dane dotyczące rekordów pomiarowych.
        /// </summary>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        IQueryable<Record> GetDataAsync();

        /// <summary>
        /// Aktualizuje istniejący rekord pomiarowy.
        /// </summary>
        /// <param name="enity">Obiekt rekordu do zaktualizowania.</param>
        void Update(Record enity);

        /// <summary>
        /// Tworzy nowy rekord pomiarowy.
        /// </summary>
        /// <param name="entity">Obiekt rekordu do dodania.</param>
        void Create(Record entity);

        /// <summary>
        /// Usuwa istniejący rekord pomiarowy.
        /// </summary>
        /// <param name="entity">Obiekt rekordu do usunięcia.</param>
        void Delete(Record entity);

        /// <summary>
        /// Pobiera dane rekordów pomiarowych spełniające określone kryteria.
        /// </summary>
        /// <param name="condition">Warunek do spełnienia.</param>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        IQueryable<Record> GetByCondition(Expression<Func<Record, bool>> condition);
    }

}