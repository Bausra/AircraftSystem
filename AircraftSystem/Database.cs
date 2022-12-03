using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Entity.ModelConfiguration.Conventions;

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
                Console.WriteLine("Database created"); //needed?
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

        public void CreateTable(string tableName, string tableRows) //correct tableRows 
        {
            OpenConnection();
            string table = $"CREATE TABLE IF NOT EXISTS {tableName} ({tableRows})"; //correct tableRows 
            SQLiteCommand command = new SQLiteCommand(table, myConnection);
            command.ExecuteNonQuery();
            CloseConnection();
        }
    }
}
