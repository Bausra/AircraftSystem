using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircraftSystem.Models;

namespace AircraftSystem
{
    public class Menu
    {
        public static ActionType GetActionType()
        {
            do
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("[1] Generate report");
                Console.WriteLine("[2] Modify database information");
                Console.WriteLine("[3] Exit\n");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        return ActionType.Report;
                    case "2":
                        return ActionType.Database;
                    case "3":
                        return ActionType.Exit;
                    default:
                        Console.WriteLine("\nIncorrect input!\n");
                        break;
                }
            } while (true);
        }

        public static ReportType GetReportType()
        { 
            do
            {
                Console.WriteLine("\nWhat type of report you would like to get?");
                Console.WriteLine("[1] Aircrafts registered in Europe");
                Console.WriteLine("[2] Aircrafts registered in non Europe country\n");

                string reportTypeChoice = Console.ReadLine();

                switch (reportTypeChoice)
                {
                    case "1":
                        return ReportType.Europe;
                    case "2":
                        return ReportType.NotEurope;
                    default:
                        Console.WriteLine("\nIncorrect input!\n");
                        break;
                }
            } while (true);
        }

        public static DatabaseTableType GetAdjustableDatabaseTableType()
        {
            do
            {
                Console.WriteLine("\nWhat type of database table you would like to modify?");
                Console.WriteLine("[1] Aircrafts");
                Console.WriteLine("[2] Aircraft models");
                Console.WriteLine("[3] Companies");
                Console.WriteLine("[4] Countries\n");

                string databaseTableTypeChoice = Console.ReadLine();

                switch (databaseTableTypeChoice)
                {
                    case "1":
                        return DatabaseTableType.Aircrafts;
                    case "2":
                        return DatabaseTableType.AircraftModels;
                    case "3":
                        return DatabaseTableType.Companies;
                    case "4":
                        return DatabaseTableType.Countries;
                    default:
                        Console.WriteLine("\nIncorrect input!\n");
                        break;
                }
            } while (true);
        }

        public static DatabaseModificationType GetDatabaseModificationType()
        {
            do
            {
                Console.WriteLine("\nWhat wold you like to do?");
                Console.WriteLine("[1] Add data to database");
                Console.WriteLine("[2] Delete data from database\n");

                string databaseModificationTypeChoice = Console.ReadLine();

                switch (databaseModificationTypeChoice)
                {
                    case "1":
                        return DatabaseModificationType.Add;
                    case "2":
                        return DatabaseModificationType.Delete;
                    default:
                        Console.WriteLine("\nIncorrect input!\n");
                        break;
                }
            } while (true);
        }
    }
}
