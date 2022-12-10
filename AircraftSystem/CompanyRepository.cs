using System.Data.SQLite;
using AircraftSystem.Models;

namespace AircraftSystem
{
    public class CompanyRepository
    {
        Database databaseObject;

        public CompanyRepository(Database databaseObject)
        {
            this.databaseObject = databaseObject;
        }

        public List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();

            string queryRetrieve = "SELECT * FROM companies";
            SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    companies.Add(new Company(
                        Convert.ToInt32(result["id"]),       
                        result["name"].ToString()
                        ));
                }
            }
            databaseObject.CloseConnection();
            return companies;
        }

        public Company GetCompany(string companyId)
        {
            Company company = null;
            string queryRetrieve = $"SELECT * FROM companies WHERE id = {companyId}";
            SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while(result.Read()) 
                {
                    company = new Company(
                        Convert.ToInt32(result["id"]),
                        result["name"].ToString()
                        );
                }
            }
            databaseObject.CloseConnection();
            return company;
        }

        public int AddCompany(Company company)
        {
            string queryInsert = "INSERT INTO companies (name) VALUES (@name)";
            SQLiteCommand myCommand = new SQLiteCommand(queryInsert, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@name", company.Name);
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
 