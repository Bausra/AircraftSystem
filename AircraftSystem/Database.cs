using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Entity.ModelConfiguration.Conventions;
using AircraftSystem.Models;

namespace AircraftSystem
{
    public class Database
    {
        public SQLiteConnection myConnection;

        public Database(string databaseName)
        {
            myConnection = new SQLiteConnection($"Data Source = {databaseName}.sqlite");

            if (!File.Exists($"./{databaseName}.sqlite"))
            {
                SQLiteConnection.CreateFile($"{databaseName}.sqlite");
                Console.WriteLine("Database created"); 
            }
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }
        public void CloseConnection()
        {

            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }

        public void CreateTable(string tableName, string tableColumns) 
        {
            OpenConnection();
            string table = $"CREATE TABLE IF NOT EXISTS {tableName} ({tableColumns})"; 
            SQLiteCommand command = new SQLiteCommand(table, myConnection);
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public void ExecuteNonQuery(List<string> sql)
        { 
            this.OpenConnection();
            foreach (string sqlItem in sql)
            {
                SQLiteCommand myCommand = new SQLiteCommand(sqlItem, myConnection);
                myCommand.ExecuteNonQuery();
            }
            this.CloseConnection();
        }

        private List<string> InitCountrySQLs()
        {
            List<string> countriesSQLs = new List<string>();
            countriesSQLs.Add("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('LT', 'Lithuania', 1)");
            countriesSQLs.Add("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('EE', 'Estonia', 1)");
            countriesSQLs.Add("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('LV', 'Latvia', 1)");
            countriesSQLs.Add("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('AL', 'Albania', 0)");
            countriesSQLs.Add("INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES ('CN', 'China', 0)");
            
            return countriesSQLs;
        }

        private List<string> InitCompanySQLs()
        {
            List<string> companiesSQLs = new List<string>();
            companiesSQLs.Add("INSERT INTO companies (name) VALUES ('SkySchool')");
            companiesSQLs.Add("INSERT INTO companies (name) VALUES ('Arise Areo')");
            companiesSQLs.Add("INSERT INTO companies (name) VALUES ('Select Sky')");
            companiesSQLs.Add("INSERT INTO companies (name) VALUES ('Sky Signal')");
            companiesSQLs.Add("INSERT INTO companies (name) VALUES ('Fly Now')");

            return companiesSQLs;
        }

        private List<string> InitAircraftModelSQLs()
        {
            List<string> aircraftModelSQLs = new List<string>();
            aircraftModelSQLs.Add("INSERT INTO aircraftModels (description, number) VALUES ('Boeing' , 'N904BU')");
            aircraftModelSQLs.Add("INSERT INTO aircraftModels (description, number) VALUES ('Fairchild' , 'LVY77')");
            aircraftModelSQLs.Add("INSERT INTO aircraftModels (description, number) VALUES ('Douglas' , '998')");
            aircraftModelSQLs.Add("INSERT INTO aircraftModels (description, number) VALUES ('Boeing' , 'RMN7T')");
            aircraftModelSQLs.Add("INSERT INTO aircraftModels(description, number) VALUES('Aibus', 'PT1')");

            return aircraftModelSQLs;
        }

        private List<string> InitAircraftSQLs()
        {
            List<string> aircraftSQLs = new List<string>();
            aircraftSQLs.Add("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('R777', 1, 3, 'LT')");
            aircraftSQLs.Add("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('MRU123', 4, 2, 'EE')");
            aircraftSQLs.Add("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('77RW', 4, 1, 'AL')");
            aircraftSQLs.Add("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('RDGTD', 5, 3, 'LT')");
            aircraftSQLs.Add("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('TFV8', 2, 4, 'CN')");
            aircraftSQLs.Add("INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES ('CN77458', 3, 1, 'CN')");

            return aircraftSQLs;
        }

        public void CreateInitialData()
        {
            ExecuteNonQuery(InitCountrySQLs());
            ExecuteNonQuery(InitCompanySQLs());
            ExecuteNonQuery(InitAircraftModelSQLs());
            ExecuteNonQuery(InitAircraftSQLs());
        }
    }
}
