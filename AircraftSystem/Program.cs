using AircraftSystem.Models;
using AircraftSystem;
using System.Data.SQLite;

//Database object
AircraftSystem.Database databaseObject = new AircraftSystem.Database();

while (true)
{
    switch (Menu.GetActionType())
    {
        case ActionType.Report:
            break;
        case ActionType.Database:
            break;
        case ActionType.Exit:
            return;
    }
}