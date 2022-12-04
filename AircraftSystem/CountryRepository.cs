using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AircraftSystem.Models;

namespace AircraftSystem;            //papildyti su try; catch;

public class CountryRepository
{
    Database databaseObject;

    public CountryRepository(Database databaseObject)
    {
        this.databaseObject = databaseObject;
    }

    public List<Country> GetAllCountries()
    {
        List<Country> countries = new List<Country>();

        string queryRetrieve = "SELECT * FROM countries";
        SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
        databaseObject.OpenConnection();
        SQLiteDataReader result = myCommand.ExecuteReader();
        if (result.HasRows)
        {
            while (result.Read())
            {
                countries.Add(new Country(
                    result["shorthand"].ToString(),
                    result["name"].ToString(),
                    result["isEurope"].ToString() == "1" ? true : false
                    )) ;
            }
        }
        databaseObject.CloseConnection();
        return countries;
    }

    public int AddCountry(Country country)
    {
        string queryInsert = "INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES (@shorthand, @name, @isEurope)";
        SQLiteCommand myCommand = new SQLiteCommand(queryInsert, databaseObject.myConnection);
        databaseObject.OpenConnection();
        myCommand.Parameters.AddWithValue("@shorthand", country.Shorthand);
        myCommand.Parameters.AddWithValue("@name", country.Name);
        myCommand.Parameters.AddWithValue("@isEurope", country.IsEurope ? 1 : 0); //1-Europe, 0-Non Europe
        int result = myCommand.ExecuteNonQuery();
        databaseObject.CloseConnection();

        return result;
    }

    public int DeleteCountry(string countryShorthand)
    {
        databaseObject.OpenConnection();
        string queryDelete = $"DELETE FROM countries WHERE shorthand = @shorthand";
        SQLiteCommand myCommand = new SQLiteCommand(queryDelete, databaseObject.myConnection);
        myCommand.Parameters.AddWithValue("@shorthand", countryShorthand);
        int result = myCommand.ExecuteNonQuery();
        databaseObject.CloseConnection();

        return result;
    }

}
