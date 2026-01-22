using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirPollutionPrediction.Model;
using Microsoft.EntityFrameworkCore;
using ServerAirPollution.Model.Validation;

namespace ServerAirPollution.Model
{
    using Microsoft.EntityFrameworkCore;

/// <summary>
/// Klasa DbContext to główna klasa, która umożliwia interakcję z bazą danych za pomocą modelu obiektowego.
/// Jest to szablon, na którego podstawie tworzy się baza danych, tabele z odpowiednimi zależnościami.
/// </summary>
public class AirPollutionDbContext : DbContext
{
    /// <summary>
    /// Konstruktor klasy, przyjmuje 'dbContextOptions' przekazywane do konstruktora klasy bazowej.
    /// Zawiera informacje dotyczące połączenia z bazą danych oraz opcje konfiguracyjne.
    /// </summary>
    /// <param name="dbContextOptions">Opcje konfiguracyjne dla DbContext.</param>
    public AirPollutionDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    { }

    /// <summary>
    /// Utworzenie zbioru, który będzie mapowany do tabeli reprezentującej rekordy w klasie Record.
    /// </summary>
    public DbSet<Record> data { get; set; }

    /// <summary>
    /// Nadpisanie metody z klasy bazowej w celu utworzenia struktury bazy danych.
    /// </summary>
    /// <param name="modelBuilder">Obiekt do konfiguracji modelu danych Entity Framework.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracje mapowań dla poszczególnych klas modelu:
        
        // Konfiguracja mapowania obiektu InputData na tabelę w bazie danych.
        modelBuilder.Entity<InputData>(e =>
        {
            // Ustalenie klucza głównego tabeli jako pole "Id" w obiekcie InputData.
            e.HasKey(e => e.Id);

            // Konfiguracja poszczególnych właściwości obiektu InputData jako kolumn w tabeli.
            e.Property(e => e.Station_Name);
            e.Property(e => e.Station_Timestamp);
            e.Property(e => e.Lat);
            e.Property(e => e.Lon);
            e.Property(e => e.Location);
            e.Property(e => e.MinimalnaTemperaturaPrzyGruncie).IsRequired();
            e.Property(e => e.RóznicaMaksymalnejIMinimalnejTemperatury).IsRequired();
            e.Property(e => e.ŚredniaTemperaturaDobowa).IsRequired();
            e.Property(e => e.ŚredniaDobowaWilgotnośćWzględna).IsRequired();
            e.Property(e => e.ŚredniaDobowaPrędkośćWiatru).IsRequired();
            e.Property(e => e.ŚrednieDoboweZachmurzenieOgólne).IsRequired();
            e.Property(e => e.ŚredniaDoboweCiśnienieNaPoziomieStacji).IsRequired();
            e.Property(e => e.SumaOpaduDzień).IsRequired();
            e.Property(e => e.DzieńRoku).IsRequired();
            e.Property(e => e.DzieńTygodnia).IsRequired();
            e.Property(e => e.Value).IsRequired();
            e.Property(e => e.ValueCat).IsRequired();

            // Określenie nazwy tabeli w bazie danych jako "InputData".
            e.ToTable("InputData");
        });

        // Konfiguracja mapowania obiektu Record na tabelę w bazie danych.
        modelBuilder.Entity<Record>(e =>
        {
            // Ustalenie klucza głównego tabeli Record jako pole "Id_sql" 
            e.HasKey(e => e.Id_sql);

            // Konfiguracja poszczególnych właściwości obiektu Record jako kolumn w tabeli.
            e.Property(e => e.Id);
            e.Property(e => e.Station_Name);
            e.Property(e => e.Station);
            e.Property(e => e.Station_Timestamp);
            e.Property(e => e.Lat);
            e.Property(e => e.Lon);
            e.Property(e => e.TemperaturaDobowa).IsRequired();
            e.Property(e => e.WilgotnośćWzględna).IsRequired();
            e.Property(e => e.PrędkośćWiatru).IsRequired();
            e.Property(e => e.ZachmurzenieOgólne).IsRequired();
            e.Property(e => e.CiśnienieNaPoziomieStacji).IsRequired();
            e.Property(e => e.SumaOpaduDzień).IsRequired();
            e.Property(e => e.ValuePm10).IsRequired();
            e.Property(e => e.ValueCatPm10).IsRequired();
            e.Property(e => e.ValuePm25).IsRequired();
            e.Property(e => e.ValueCatPm25).IsRequired();
            e.Property(e => e.ValueNo2).IsRequired();
            e.Property(e => e.ValueCatNo2).IsRequired();
            e.Property(e => e.ValueO3).IsRequired();
            e.Property(e => e.ValueCatO3).IsRequired();
            e.Property(e => e.ValueSo2).IsRequired();
            e.Property(e => e.ValueCatSo2).IsRequired();

            // Określenie nazwy tabeli w bazie danych jako "Record".
            e.ToTable("Record");
        });

        // Konfiguracja mapowania obiektu AirLoc na tabelę w bazie danych.
        modelBuilder.Entity<AirLoc>(e =>
        {
            // Ustalenie klucza głównego tabeli AirLoc jako pole "Id".
            e.HasKey(e => e.Id);

            // Konfiguracja poszczególnych właściwości obiektu AirLoc jako kolumn w tabeli.
            e.Property(e => e.PollutionType);
            e.Property(e => e.Name);
            e.Property(e => e.Longitude);
            e.Property(e => e.Latitude);
            e.Property(e => e.CategoryNumber);

            // Określenie nazwy tabeli w bazie danych jako "AirLoc".
            e.ToTable("AirLoc");
        });

        modelBuilder.Entity<User>(e=>
        {
            e.HasKey(e=>e.Email);
            e.Property(e=>e.IP);
            e.Property(e=>e.TimeStamp);

            e.ToTable("Users");
        });
            


        
    }
}

}

                
                
                
                
                
                




