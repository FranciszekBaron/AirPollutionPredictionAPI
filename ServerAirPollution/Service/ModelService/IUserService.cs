using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ServerAirPollution.Model;

namespace ServerAirPollution.Service.ModelService
{
    /// <summary>
    /// Interfejs serwisu obsługującego operacje na użytkownikach korzystających z serwisu
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Pobiera dane dotyczące rekordów użytkowników.
        /// </summary>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        IQueryable<User> GetDataAsync();

        /// <summary>
        /// Aktualizuje istniejący rekord o użytkowniku.
        /// </summary>
        /// <param name="enity">Obiekt rekordu do zaktualizowania.</param>
        void Update(User enity);

        /// <summary>
        /// Tworzy nowego użytkownika.
        /// </summary>
        /// <param name="entity">Obiekt rekordu do dodania.</param>
        void Create(User entity);

        /// <summary>
        /// Usuwa istniejącego użytkownika.
        /// </summary>
        /// <param name="entity">Obiekt rekordu do usunięcia.</param>
        void Delete(User entity);

        /// <summary>
        /// Pobiera dane rekordów spełniające określone kryteria.
        /// </summary>
        /// <param name="condition">Warunek do spełnienia.</param>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        IQueryable<User> GetByCondition(Expression<Func<User, bool>> condition);
    }
}