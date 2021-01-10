using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using RestCountriesApp.Models;
using System.Collections.ObjectModel;

namespace RestCountriesApp.Utilities
{
    /// <summary>
    /// Logic to create SQL requests for interaction with Database
    /// </summary>
    public static class DBManager
    {
        public static string ConnectionString { get; } =
            //"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WPFProjectDB;Data Source=HPLAPTOP\\SQLEXPRESS";
            Properties.Resources.DbConnectionString;

        public static int Counter { get; private set; } = 0;


        public static void InsertCountries(List<JObject> restCountries)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                foreach (JObject countryObj in restCountries)
                {
                    string name = countryObj["name"].ToString().Replace("'", "''");
                    string capital = countryObj["capital"].ToString().Replace("'", "''");
                    string base64Flag = DataParser.EncodeSvgFileToBase64(countryObj["flag"].ToString());

                    string commandString = $"INSERT INTO Countries " +
                        $"VALUES({++Counter}, '{name}', '{countryObj["alpha3Code"]}', '{capital}', " +
                        $"'{countryObj["region"]}', {countryObj["population"]}, '{base64Flag}', '{countryObj["flag"]}');";
                    SqlCommand command = new SqlCommand(commandString, sqlConnection);

                    command.ExecuteNonQuery();
                }

                SqlConnection.ClearPool(sqlConnection);
            }
        }


        public static ObservableCollection<CountryDAO> GetCountries()
        {
            ObservableCollection<CountryDAO> countries = new ObservableCollection<CountryDAO>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                string commandStr = "SELECT * FROM [Countries]";
                SqlCommand command = new SqlCommand(commandStr, sqlConnection);

                using (var reader = command.ExecuteReader())
                {
                    WriteSqlRowsIntoCollection(countries, reader);
                }

                SqlConnection.ClearPool(sqlConnection);
            }
            return countries;
        }


        public static bool CheckCountriesExist()
        {
            bool isExist = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();

                string commandStr = "SELECT * FROM [Countries]";
                SqlCommand command = new SqlCommand(commandStr, sqlConnection);

                using (var reader = command.ExecuteReader())
                {
                    isExist = reader.HasRows;
                }

                SqlConnection.ClearPool(sqlConnection);
            }
            return isExist;
        }


        private static void WriteSqlRowsIntoCollection(Collection<CountryDAO> countriesCollection, SqlDataReader reader)
        {
            while (reader.Read())
            {
                int CountryId = Convert.ToInt32(reader[0]);
                long CountryPopulation = Convert.ToInt64(reader[5]);

                countriesCollection.Add(new CountryDAO(
                    CountryId, reader[1].ToString(), reader[2].ToString(),
                    reader[3].ToString(), reader[4].ToString(), CountryPopulation,
                    reader[6].ToString(), reader[7].ToString())
                );
            }
        }
    }
}