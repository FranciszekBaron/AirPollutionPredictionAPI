using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerAirPollution.Model;

namespace ServerAirPollution.Service.ModelService
{
    public class UserService : IUserService
    {
        private readonly AirPollutionDbContext _dbContext;
        private readonly ILogger<DataService> _logger;

        /// <summary>
        /// Inicjalizuje nową instancję serwisu DataService.
        /// </summary>
        /// <param name="dbContext">Kontekst bazy danych AirPollutionDbContext.</param>
        /// <param name="logger">Instancja loggera.</param>
        public UserService(AirPollutionDbContext dbContext, ILogger<DataService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _logger.LogInformation("Uruchomiono User S");
        }

        /// <summary>
        /// Pobiera dane pomiarowe.
        /// </summary>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        public IQueryable<User> GetDataAsync()
        {
            return _dbContext.Set<User>().AsNoTracking();
        }

        /// <summary>
        /// Aktualizuje istniejący rekord pomiarowy.
        /// </summary>
        /// <param name="entity">Obiekt pomiaru do zaktualizowania.</param>
        public void Update(User entity)
        {
            _dbContext.Update(entity);
        }

        /// <summary>
        /// Tworzy nowy rekord pomiarowy.
        /// </summary>
        /// <param name="entity">Obiekt pomiaru do dodania.</param>
        public void Create(User entity)
        {
            _dbContext.Add(entity);
        }

        /// <summary>
        /// Pobiera dane pomiarowe spełniające określone kryteria.
        /// </summary>
        /// <param name="condition">Warunek do spełnienia.</param>
        /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
        public IQueryable<User> GetByCondition(Expression<Func<User, bool>> condition)
        {
            return _dbContext.Set<User>().Where(condition).AsNoTracking();
        }

        /// <summary>
        /// Usuwa istniejący rekord pomiarowy.
        /// </summary>
        /// <param name="entity">Obiekt pomiaru do usunięcia.</param>
        public void Delete(User entity)
        {
            _dbContext.Remove(entity);
        }
    }
}