using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftSystem
{
    public class CompanyRepository
    {
        Database databaseObject;

        public CompanyRepository(Database databaseObject)
        {
            this.databaseObject = databaseObject;
        }

        public List<string> GetCompanyIds()
        {
            List<string> companyIds = new List<string>();

            string queryRetrieve = "SELECT * FROM companies";
            SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    companyIds.Add(result["id"].ToString());
                }
            }
            else
            {
                return companyIds;
                ;
            }
            databaseObject.CloseConnection();

            return companyIds;
        }

        public List<string> GetCompanyNames()
        {
            List<string> companyNames = new List<string>();

            string queryRetrieve = "SELECT * FROM companies";
            SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    companyNames.Add(result["name"].ToString());
                }
            }
            else
            {
                return companyNames;
                ;
            }
            databaseObject.CloseConnection();

            return companyNames;
        }

        public List<string> companyIdsAndNames()
        {
            List<string> companyIdsAndNames = new List<string>();

            string queryRetrieve = "SELECT * FROM companies";
            SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    companyIdsAndNames.Add($"[{result["id"].ToString()}] {result["name"].ToString()}");
                }
            }
            else
            {
                return companyIdsAndNames;
            }
            databaseObject.CloseConnection();

            return companyIdsAndNames;
        }

        public int AddCompany(string name)
        {
            string queryInsert = "INSERT INTO companies (name) VALUES (@name)";
            SQLiteCommand myCommand = new SQLiteCommand(queryInsert, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@name", name);
            int result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            return result;
        }

        public int DeleteCompany(string companyId)
        {
            databaseObject.OpenConnection();
            string queryDelete = $"DELETE FROM companies WHERE id = @id";
            SQLiteCommand myCommand = new SQLiteCommand(queryDelete, databaseObject.myConnection);
            myCommand.Parameters.AddWithValue("@id", companyId);
            int result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            return result;
        }
    }
}
 