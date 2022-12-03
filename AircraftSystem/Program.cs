using AircraftSystem.Models;
using AircraftSystem;
using System.Data.SQLite;
using System.Data.Common;

//Database object
Database databaseObject = new Database("aircraftDB");
databaseObject.CreateTable("countries", "shorthand TEXT, name TEXT, isEurope INTEGER");
databaseObject.CreateTable("companies", "id INTEGER, name TEXT");
databaseObject.CreateTable("aircraftModels", "id INTEGER, description TEXT, number TEXT");
databaseObject.CreateTable("aircrafts", "tailNumber TEXT, aircraftModelID INTEGER, companyID INTEGER, countryID TEXT");

CountryRepository countryRepository = new CountryRepository(databaseObject);
CountryService countryService = new CountryService(countryRepository);


while (true)
{
    switch (Menu.GetActionType())
    {
        case ActionType.Report:
            break;

        case ActionType.Database:
            DatabaseTableType databaseTableType = Menu.GetAdjustableDatabaseTableType();
            DatabaseModificationType databaseModificationType = Menu.GetDatabaseModificationType();

            switch (databaseTableType)
            {
                case DatabaseTableType.Aircrafts:
                    switch (databaseModificationType)
                    {
                        case DatabaseModificationType.Add:
                            break;
                        case DatabaseModificationType.Delete:
                            break;
                    }
                    break;

                case DatabaseTableType.AircraftModels:
                    switch (databaseModificationType)
                    {
                        case DatabaseModificationType.Add:
                            break;
                        case DatabaseModificationType.Delete:
                            break;
                    }
                    break;

                case DatabaseTableType.Companies:
                    switch (databaseModificationType)
                    {
                        case DatabaseModificationType.Add:
                            break;
                        case DatabaseModificationType.Delete:
                            break;
                    }
                    break;

                case DatabaseTableType.Countries:
                    switch (databaseModificationType)
                    {
                        case DatabaseModificationType.Add:
                            countryService.ExecuteAddCountryProcedure();
                            break;
                        case DatabaseModificationType.Delete:
                            countryService.ExecuteDeleteCountryProcedure();
                            break;
                    }
                    break;
            }
            break;

        case ActionType.Exit:
            return;
    }
}