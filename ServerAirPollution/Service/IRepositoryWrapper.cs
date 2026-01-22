using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ServerAirPollution.Model;
using ServerAirPollution.Service.ModelService;
using ServerAirPollution.Service.ValidationService;

namespace ServerAirPollution.Service
{
    /// <summary>
    /// Interfejs reprezentujący wrapper dla repozytoriów, umożliwiający dostęp do różnych rodzajów usług danych.
    /// </summary>
    public interface IRepositoryWrapper
    {
        /// <summary>
        /// Repozytorium dostępu do danych pomiarowych.
        /// </summary>
        IDataService data { get; }

        /// <summary>
        /// Repozytorium dostępu do danych użytkowników.
        /// </summary>
        IUserService user { get; }

        /// <summary>
        /// Repozytorium dostępu do danych o lokalizacjach powietrza.
        /// </summary>
        IAirLocService airLoc { get; }

        /// <summary>
        /// Repozytorium dostępu do usług walidacyjnych.
        /// </summary>
        IValidService validation { get; }

        /// <summary>
        /// Asynchronicznie zapisuje zmiany w bazie danych.
        /// </summary>
        /// <returns>Task reprezentujący operację asynchroniczną.</returns>
        Task saveAsync();

        /// <summary>
        /// Sprawdza, czy podany rekord pomiarowy istnieje w bazie danych.
        /// </summary>
        /// <param name="body">Rekord pomiarowy do sprawdzenia.</param>
        /// <returns>True, jeśli rekord istnieje, w przeciwnym razie false.</returns>
        Task<bool> DataExists(Record body);
    }

}