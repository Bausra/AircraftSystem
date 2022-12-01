using System.Data.SQLite;

//Database object
var databaseObject = new AircraftSystem.Database();

string queryInsert = "INSERT INTO countries (`shorthand`, `name`, `isEurope`) VALUES (@shorthand, @name, @isEurope)";
SQLiteCommand myCommand = new SQLiteCommand(queryInsert, databaseObject.myConnection);
databaseObject.OpenConnection();
myCommand.Parameters.AddWithValue("@shorthand", "LT");
myCommand.Parameters.AddWithValue("@name", "Lithuania");
myCommand.Parameters.AddWithValue("@isEurope", 1);
var result = myCommand.ExecuteNonQuery(); 
databaseObject.CloseConnection();