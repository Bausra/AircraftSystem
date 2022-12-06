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
        private bool newDbCreated = false;
        public bool NewDbCreated { get { return newDbCreated; } }

        public Database(string databaseName)
        {
            myConnection = new SQLiteConnection($"Data Source = {databaseName}.sqlite");

            if (!File.Exists($"./{databaseName}.sqlite"))
            {
                SQLiteConnection.CreateFile($"{databaseName}.sqlite");
                this.newDbCreated = true;
                Console.WriteLine("Database created"); 
            }
        }

        public void OpenConnection()
        {
            try
            {
                if (myConnection.State != System.Data.ConnectionState.Open)
                {
                    myConnection.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public void ExecuteNonQuery(string sql)
        {
            SQLiteCommand myCommand = new SQLiteCommand(sql, myConnection);
            this.OpenConnection();
            int result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }
    }
}
