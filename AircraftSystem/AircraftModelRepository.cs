using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AircraftSystem.Models;

namespace AircraftSystem
{
    public class AircraftModelRepository
    {
        Database databaseObject;

        public AircraftModelRepository(Database databaseObject)
        {
            this.databaseObject = databaseObject;
        }

        public List<AircraftModel> GetAllAircraftModels()
        {
            List<AircraftModel> aircraftModels = new List<AircraftModel>();

            string queryRetrieve = "SELECT * FROM aircraftModels";
            SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    aircraftModels.Add(new AircraftModel(
                        Convert.ToInt32(result["id"]),
                        result["description"].ToString(),
                        result["number"].ToString()
                        ));
                }
            }
            databaseObject.CloseConnection();
            return aircraftModels;
        }

        public int AddAircraftModel(AircraftModel aircraftModel)
        {
            string queryInsert = "INSERT INTO aircraftModels (description, number) VALUES (@description, @number)";
            SQLiteCommand myCommand = new SQLiteCommand(queryInsert, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@description", aircraftModel.Description);
            myCommand.Parameters.AddWithValue("@number", aircraftModel.Number);
            int result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            return result;
        }

        public int DeleteAircraftModel(string aircraftModelId)
        {
            databaseObject.OpenConnection();
            string queryDelete = $"DELETE FROM aircraftModels WHERE id = @id";
            SQLiteCommand myCommand = new SQLiteCommand(queryDelete, databaseObject.myConnection);
            myCommand.Parameters.AddWithValue("@id", aircraftModelId);
            int result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            return result;
        }
    }
}
