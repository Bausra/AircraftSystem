using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AircraftSystem;            //papildyti su try; catch;

public class CountryRepository
{
    Database databaseObject;

    public CountryRepository(Database databaseObject)
    {
        this.databaseObject = databaseObject;
    }

    public List<string> GetCountryShorthands()
    {
        List<string> countryShorthands = new List<string>();

        string queryRetrieve = "SELECT * FROM countries";
        SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
        databaseObject.OpenConnection();
        SQLiteDataReader result = myCommand.ExecuteReader();
        if (result.HasRows)
        {
            while (result.Read())
            {
                countryShorthands.Add(result["shorthand"].ToString());
            }
        }
        else
        {
            return countryShorthands;
;           }
        databaseObject.CloseConnection();

        return countryShorthands;
    }

    public List<string> countriesShorthandsAndNames()
    {
        List<string> countryShorthandsAndNames = new List<string>();

        string queryRetrieve = "SELECT * FROM countries";
        SQLiteCommand myCommand = new SQLiteCommand(queryRetrieve, databaseObject.myConnection);
        databaseObject.OpenConnection();
        SQLiteDataReader result = myCommand.ExecuteReader();
        if (result.HasRows)
        {
            while (result.Read())
            {
                countryShorthandsAndNames.Add($"[{result["shorthand"].ToString()}] {result["name"].ToString()}");
            }
        }
        else
        {
            return countryShorthandsAndNames;
        }
        databaseObject.CloseConnection();

        return countryShorthandsAndNames;
    }

    public int AddCountry(string shorthand, string name, int isEurope)
    {
        string queryInsert = "INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES (@shorthand, @name, @isEurope)";
        SQLiteCommand myCommand = new SQLiteCommand(queryInsert, databaseObject.myConnection);
        databaseObject.OpenConnection();
        myCommand.Parameters.AddWithValue("@shorthand", shorthand);
        myCommand.Parameters.AddWithValue("@name", name);
        myCommand.Parameters.AddWithValue("@isEurope", isEurope); //1-Europe, 0-Non Europe
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
