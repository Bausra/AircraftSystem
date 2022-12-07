using System.Data.SQLite;
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

        public AircraftModel GetAircraftModel(string aircraftModelId)
        {
            AircraftModel aircraftModel = null;
            string queryRetrieve = $"SELECT * FROM aircraftModels WHERE id = {aircraftModelId}";
            SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    aircraftModel = new AircraftModel(
                    Convert.ToInt32(result["id"]),
                    result["description"].ToString(),
                    result["number"].ToString()
                    );
                }
                
            }
            databaseObject.CloseConnection();
            return aircraftModel;
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
