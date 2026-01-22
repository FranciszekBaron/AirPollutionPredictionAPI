using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerAirPollution.Model;
using ServerAirPollution.Service.ValidationService;

namespace ServerAirPollution.Service
{
    using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
    using ServerAirPollution.Service.ModelService;
    using System.Threading.Tasks;

/// <summary>
/// Klasa implementująca interfejs IRepositoryWrapper, zapewniająca dostęp do różnych repozytoriów.
/// </summary>
public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly AirPollutionDbContext _dbContext;
    private readonly ILogger<DataService> _logger;

    // Deklaracje pól, które będą przechowywać instancje repozytoriów
    IDataService _data;
    IAirLocService _airLoc;
    IValidService _validation;

    IUserService _user;

    /// <summary>
    /// Konstruktor klasy RepositoryWrapper.
    /// </summary>
    /// <param name="dbContext">Kontekst bazy danych.</param>
    /// <param name="logger">Logger służący do zapisu informacji.</param>
    public RepositoryWrapper(AirPollutionDbContext dbContext, ILogger<DataService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public IUserService user
    {
        get
        {
            if(_user is null)
            {
                _user = new UserService(_dbContext,_logger);
            }
            return _user;
        }
    }

    // Implementacja właściwości data
    public IDataService data
    {
        get
        {
            if (_data is null)
            {
                // Tworzenie instancji DataService tylko wtedy, gdy jest potrzebne
                _data = new DataService(_dbContext, _logger);
            }
            return _data;
        }
    }

    // Implementacja właściwości validation
    public IValidService validation
    {
        get
        {
            if (_validation is null)
            {
                // Tworzenie instancji ValidationService tylko wtedy, gdy jest potrzebne
                _validation = new ValidationService.ValidationService(_dbContext);
            }
            return _validation;
        }
    }

    // Implementacja właściwości airLoc
    public IAirLocService airLoc
    {
        get
        {
            if (_airLoc is null)
            {
                // Tworzenie instancji AirLocService tylko wtedy, gdy jest potrzebne
                _airLoc = new AirLocService(_dbContext);
            }
            return _airLoc;
        }
    }

    // Implementacja metody DataExists
    public async Task<bool> DataExists(Record body)
    {
        // Sprawdzanie, czy rekord o podanym Id_sql istnieje w bazie danych
        var x = await _data.GetByCondition(e => e.Id_sql == body.Id_sql).FirstOrDefaultAsync();
        return x != null;
    }

    // Implementacja metody saveAsync
    public async Task saveAsync()
    {
        // Zapisanie zmian w bazie danych
        await _dbContext.SaveChangesAsync();
    }
}

}