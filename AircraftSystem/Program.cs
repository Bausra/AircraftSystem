using AircraftSystem.Models;
using AircraftSystem;
using System.Data.SQLite;
using System.Data.Common;

//Database object
Database databaseObject = new Database("aircraftDB");
if (databaseObject.NewDbCreated)
{
    databaseObject.CreateTable("countries", "shorthand TEXT, name TEXT, isEurope INTEGER");
    databaseObject.CreateTable("companies", "id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT");
    databaseObject.CreateTable("aircraftModels", "id INTEGER PRIMARY KEY AUTOINCREMENT, description TEXT, number TEXT");
    databaseObject.CreateTable("aircrafts", "id INTEGER PRIMARY KEY AUTOINCREMENT, tailNumber TEXT, aircraftModelID INTEGER, companyID INTEGER, countryShorthand TEXT");

    //countries
    databaseObject.ExecuteNonQuery("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('LT', 'Lithuania', 1)");
    databaseObject.ExecuteNonQuery("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('EE', 'Estonia', 1)");
    databaseObject.ExecuteNonQuery("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('LV', 'Latvia', 1)");
    databaseObject.ExecuteNonQuery("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('AL', 'Albania', 0)");
    databaseObject.ExecuteNonQuery("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('CN', 'China', 0)");
    //companies
    databaseObject.ExecuteNonQuery("INSERT INTO companies (name) VALUES ('SkySchool')");
    databaseObject.ExecuteNonQuery("INSERT INTO companies (name) VALUES ('Arise Areo')");
    databaseObject.ExecuteNonQuery("INSERT INTO companies (name) VALUES ('Select Sky')");
    databaseObject.ExecuteNonQuery("INSERT INTO companies (name) VALUES ('Sky Signal')");
    databaseObject.ExecuteNonQuery("INSERT INTO companies (name) VALUES ('Fly Now')");

    //Aircraft models
    databaseObject.ExecuteNonQuery("INSERT INTO aircraftModels (description, number) VALUES ('Boeing' , 'N904BU')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircraftModels (description, number) VALUES ('Fairchild' , 'LVY77')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircraftModels (description, number) VALUES ('Douglas' , '998')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircraftModels (description, number) VALUES ('Boeing' , 'RMN7T')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircraftModels (description, number) VALUES ('Aibus' , 'PT1')");

    //Aircrafts
    databaseObject.ExecuteNonQuery("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('R777', 1, 3, 'LT')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('MRU123', 4, 2, 'EE')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('77RW', 4, 1, 'AL')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('RDGTD', 5, 3, 'LT')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('TFV8', 2, 4, 'CN')");
    databaseObject.ExecuteNonQuery("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('CN77458', 3, 1, 'CN')");
}


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
ReportGenerator reportGenerator = new ReportGenerator(aircraftRepository);

while (true)
{
    switch (Menu.GetActionType())
    {
        case ActionType.Report:
            ReportType reportType = Menu.GetReportType();

            switch (reportType)
            {
                case ReportType.Europe:
                    reportGenerator.ExecuteHTMLReportAircraftInEurope(true);
                    break;

                case ReportType.NotEurope:
                    reportGenerator.ExecuteHTMLReportAircraftInEurope(false);
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