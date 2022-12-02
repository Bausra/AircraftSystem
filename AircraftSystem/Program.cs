using AircraftSystem.Models;
using AircraftSystem;
using System.Data.SQLite;
using System.Data.Common;

//Database object
AircraftSystem.Database databaseObject = new AircraftSystem.Database("aircraftDB");
databaseObject.CreateTable("countries", "shorthand TEXT, name TEXT, isEurope INTEGER");
databaseObject.CreateTable("companies", "id INTEGER, name TEXT");
databaseObject.CreateTable("aircraftModels", "id INTEGER, description TEXT, number TEXT");
databaseObject.CreateTable("aircrafts", "tailNumber TEXT, aircraftModelID INTEGER, companyID INTEGER, countryID TEXT");

//while (true)
//{
//    switch (Menu.GetActionType())
//    {
//        case ActionType.Report:
//            break;
//        case ActionType.Database:
//            break;
//        case ActionType.Exit:
//            return;
//    }
//}