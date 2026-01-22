using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerAirPollution.Model;

namespace ServerAirPollution.Service
{
   

    /// <summary>
    /// Serwis obsługujący operacje na danych dotyczących lokalizacji zanieczyszczeń powietrza.
    /// </summary>
    public class AirLocService : IAirLocService
    {
        private readonly AirPollutionDbContext _dbContext;

        /// <summary>
        /// Inicjalizuje nową instancję serwisu AirLocService.
        /// </summary>
        /// <param name="dbContext">Kontekst bazy danych AirPollutionDbContext.</param>
        public AirLocService(AirPollutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Pobiera dane dotyczące lokalizacji zanieczyszczeń powietrza.
        /// </summary>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        public IQueryable<AirLoc> GetDataAsync()
        {
            return _dbContext.Set<AirLoc>().AsNoTracking();
        }

        /// <summary>
        /// Aktualizuje istniejący rekord lokalizacji zanieczyszczeń powietrza.
        /// </summary>
        /// <param name="entity">Obiekt lokalizacji do zaktualizowania.</param>
        public void Update(AirLoc entity)
        {
            _dbContext.Update(entity);
        }

        /// <summary>
        /// Tworzy nowy rekord lokalizacji zanieczyszczeń powietrza.
        /// </summary>
        /// <param name="entity">Obiekt lokalizacji do dodania.</param>
        public void Create(AirLoc entity)
        {
            _dbContext.Add(entity);
        }

        /// <summary>
        /// Pobiera dane lokalizacji zanieczyszczeń powietrza spełniające określone kryteria.
        /// </summary>
        /// <param name="condition">Warunek do spełnienia.</param>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        public IQueryable<AirLoc> GetByCondition(Expression<Func<AirLoc, bool>> condition)
        {
            return _dbContext.Set<AirLoc>().Where(condition).AsNoTracking();
        }
    }

}