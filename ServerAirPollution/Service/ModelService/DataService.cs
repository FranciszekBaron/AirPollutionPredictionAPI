using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirPollutionPrediction.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAirPollution.Model;
namespace ServerAirPollution.Service
{
    using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;

/// <summary>
/// Serwis obsługujący operacje na danych pomiarowych.
/// </summary>
public class DataService : IDataService
{
    private readonly AirPollutionDbContext _dbContext;
    private readonly ILogger<DataService> _logger;

    /// <summary>
    /// Inicjalizuje nową instancję serwisu DataService.
    /// </summary>
    /// <param name="dbContext">Kontekst bazy danych AirPollutionDbContext.</param>
    /// <param name="logger">Instancja loggera.</param>
    public DataService(AirPollutionDbContext dbContext, ILogger<DataService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _logger.LogInformation("Uruchomiono User S");
    }

    /// <summary>
    /// Pobiera dane pomiarowe.
    /// </summary>
    /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
    public IQueryable<Record> GetDataAsync()
    {
        return _dbContext.Set<Record>().AsNoTracking();
    }

    /// <summary>
    /// Aktualizuje istniejący rekord pomiarowy.
    /// </summary>
    /// <param name="entity">Obiekt pomiaru do zaktualizowania.</param>
    public void Update(Record entity)
    {
        _dbContext.Update(entity);
    }

    /// <summary>
    /// Tworzy nowy rekord pomiarowy.
    /// </summary>
    /// <param name="entity">Obiekt pomiaru do dodania.</param>
    public void Create(Record entity)
    {
        _dbContext.Add(entity);
    }

    /// <summary>
    /// Pobiera dane pomiarowe spełniające określone kryteria.
    /// </summary>
    /// <param name="condition">Warunek do spełnienia.</param>
    /// <returns>Kwerenda pozwalająca na pobranie danych.</returns>
    public IQueryable<Record> GetByCondition(Expression<Func<Record, bool>> condition)
    {
        return _dbContext.Set<Record>().Where(condition).AsNoTracking();
    }

    /// <summary>
    /// Usuwa istniejący rekord pomiarowy.
    /// </summary>
    /// <param name="entity">Obiekt pomiaru do usunięcia.</param>
    public void Delete(Record entity)
    {
        _dbContext.Remove(entity);
    }
}

}















