﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        Convert.ToInt32(result["id"]),       //casting
                        result["name"].ToString()
                        ));
                }
            }
            databaseObject.CloseConnection();
            return companies;
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
 