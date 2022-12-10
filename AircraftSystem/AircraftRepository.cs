using System.Data.SQLite;
using AircraftSystem.Models;

namespace AircraftSystem
{
    public class AircraftRepository
    {
        Database databaseObject;

        public AircraftRepository(Database databaseObject)
        {
            this.databaseObject = databaseObject;
        }

        public List<Aircraft> GetAllAircraftData()
        {
            List<Aircraft> aircraftData = new List<Aircraft>();

            string queryRetrieve = @"SELECT 
                                    aircrafts.id, 
                                    aircrafts.tailNumber, 
                                    aircraftModels.id AS aircraftsModelID,
                                    aircraftModels.description AS aircraftsModelDescription,
                                    aircraftModels.number AS aircraftsModelNumber,
                                    companies.id AS companyID,
                                    companies.name AS companyName,
                                    countries.shorthand AS countryShorthand,
                                    countries.name AS countryName,
                                    countries.isEurope AS countryIsEurope

                                    FROM aircrafts
                                    INNER JOIN aircraftModels
                                    ON aircrafts.aircraftModelID = aircraftModels.id

                                    INNER JOIN companies
                                    ON aircrafts.companyID = companies.id

                                    INNER JOIN countries
                                    ON aircrafts.countryShorthand = countries.shorthand";          
            SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    aircraftData.Add(new Aircraft(
                        Convert.ToInt32(result["id"]),
                        result["tailNumber"].ToString(),
                        new AircraftModel(
                            Convert.ToInt32(result["aircraftsModelID"]), 
                            result["aircraftsModelDescription"].ToString(), 
                            result["aircraftsModelNumber"].ToString()),
                        new Company(
                            Convert.ToInt32(result["companyID"]), 
                            result["companyName"].ToString()),
                        new Country(
                            result["countryShorthand"].ToString(), 
                            result["countryName"].ToString(), 
                            Convert.ToBoolean(result["countryIsEurope"]))
                        ));
                }
            }
 
            databaseObject.CloseConnection();
            return aircraftData;
        }

        public int AddAircraft(Aircraft aircraft)
        {
            databaseObject.OpenConnection();
            string queryInsert = "INSERT INTO aircrafts (tailNumber,aircraftModelID, companyID, countryShorthand) VALUES (@tailNumber, @aircraftModelID, @companyID, @countryShorthand)";
            SQLiteCommand myCommand = new SQLiteCommand(queryInsert, databaseObject.myConnection);
            myCommand.Parameters.AddWithValue("@tailNumber", aircraft.TailNumber);
            myCommand.Parameters.AddWithValue("@aircraftModelID", aircraft.AircraftModel.ID);
            myCommand.Parameters.AddWithValue("@companyID", aircraft.Company.ID);
            myCommand.Parameters.AddWithValue("@countryShorthand", aircraft.Country.Shorthand);
            int result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            return result;
        }

        public int DeleteAircraft(string aircraftId)
        {
            databaseObject.OpenConnection();
            string queryDelete = $"DELETE FROM aircrafts WHERE id = @id";
            SQLiteCommand myCommand = new SQLiteCommand(queryDelete, databaseObject.myConnection);
            myCommand.Parameters.AddWithValue("@id", aircraftId);
            int result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            return result;
        }
    }
}
