﻿using AircraftSystem.Models;
using AircraftSystem;
using System.Data.SQLite;
using System.Data.Common;

//Database object
Database databaseObject = new Database("aircraftDB");
databaseObject.CreateTable("countries", "shorthand TEXT, name TEXT, isEurope INTEGER");
databaseObject.CreateTable("companies", "id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT");
databaseObject.CreateTable("aircraftModels", "id INTEGER PRIMARY KEY AUTOINCREMENT, description TEXT, number TEXT");
databaseObject.CreateTable("aircrafts", "id INTEGER PRIMARY KEY AUTOINCREMENT, tailNumber TEXT, aircraftModelID INTEGER, companyID INTEGER, countryShorthand TEXT");

//Country objects
CountryRepository countryRepository = new CountryRepository(databaseObject);
CountryService countryService = new CountryService(countryRepository);

//Company objects
CompanyRepository companyRepository = new CompanyRepository(databaseObject);
CompanyService companyService = new CompanyService(companyRepository);

//Aircraft model objects
AircraftModelRepository aircraftModelRepository = new AircraftModelRepository(databaseObject);
AircraftModelService aircraftModelService = new AircraftModelService(aircraftModelRepository);

//Aircraft objects
AircraftRepository aircraftRepository = new AircraftRepository(databaseObject);
AircraftService aircraftService = new AircraftService(aircraftRepository, aircraftModelRepository, aircraftModelService, companyRepository, companyService, countryRepository, countryService);

//Report Generator objects
ReportGenerator reportGenerator = new ReportGenerator(aircraftRepository, aircraftModelRepository, companyRepository, countryRepository);

while (true)
{
    switch (Menu.GetActionType())
    {
        case ActionType.Report:
            ReportType reportType = Menu.GetReportType();

            switch (reportType)
            {
                case ReportType.Europe:
                    reportGenerator.GenerateReportAircraftInEurope();
                    break;

                case ReportType.NotEurope:
                    reportGenerator.GenerateReportAircraftNotInEurope();
                    break;
            }
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
                            aircraftService.ExecuteAddAircraftProcedure();
                            break;
                        case DatabaseModificationType.Delete:
                            aircraftService.ExecuteDeleteAircraftProcedure();
                            break;
                    }
                    break;

                case DatabaseTableType.AircraftModels:
                    switch (databaseModificationType)
                    {
                        case DatabaseModificationType.Add:
                            aircraftModelService.ExecuteAddAircraftModelProcedure();
                            break;
                        case DatabaseModificationType.Delete:
                            aircraftModelService.ExecuteDeleteAircraftModelProcedure();
                            break;
                    }
                    break;

                case DatabaseTableType.Companies:
                    switch (databaseModificationType)
                    {
                        case DatabaseModificationType.Add:
                            companyService.ExecuteAddCompanyProcedure();
                            break;
                        case DatabaseModificationType.Delete:
                            companyService.ExecuteDeleteCompanyProcedure();
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