using System;
using Microsoft.Data.Sqlite;
using System.IO;
namespace NetflixDatabase
{
    class ShowRepos
    {
        private SqliteConnection connection;
        public ShowRepos(string filePath)
        {
            connection = new SqliteConnection($"Data Source={filePath}");
        }
        public void GetAll()
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM netflix";
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Show show = new Show();
                show.id = reader.GetString(0);
                show.title = reader.GetString(2);
                if(reader.IsDBNull(5))
                {
                    show.country = "unknown";
                }
                else
                {
                    show.country = reader.GetString(5);
                }
                show.year = int.Parse(reader.GetString(7));
                Console.WriteLine(show.ToString());
            }
        }
        public void GetExport(string country)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM netflix WHERE country = $country";
            command.Parameters.AddWithValue("$country", country);
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Show show = new Show();
                show.id = reader.GetString(0);
                show.title = reader.GetString(2);
                if (reader.IsDBNull(5))
                {
                    show.country = "unknown";
                }
                else
                {
                    show.country = reader.GetString(5);
                }
                show.year = int.Parse(reader.GetString(7));
                Console.WriteLine(show.ToString());
            }
        }
        public void Export(string country)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM netflix WHERE country = $country";
            command.Parameters.AddWithValue("$country", country);
            SqliteDataReader reader = command.ExecuteReader();
            StreamWriter writer = new StreamWriter(@"C:\Users\Katya\MishaProga\practiki\NetflixDatabase\export.csv");
            while (reader.Read())
            {
                Show show = new Show();
                show.id = reader.GetString(0);
                show.title = reader.GetString(2);
                if (reader.IsDBNull(5))
                {
                    show.country = "unknown";
                }
                else
                {
                    show.country = reader.GetString(5);
                }
                show.year = int.Parse(reader.GetString(7));
                writer.WriteLine(show.ToCSV());
            }
        }
    }
        
    class Show
    {
        public string id;
        public string title;
        public int year;
        public string country;
        public Show()
        {

        }
        public Show(string id, string title, int year, string country)
        {
            this.id = id;
            this.title = title;
            this.year = year;
            this.country = country;
        }
        public override string ToString()
        {
            return $"[{id}] \"{title}\" from {country}. Year: {year}";
        }
        public string ToCSV()
        {
            return $"{id},{title},{country},{year}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ShowRepos show = new ShowRepos(@"C:\Users\Katya\MishaProga\practiki\NetflixDatabase\netflix.db");
            show.Export("India");
            Console.WriteLine("Ok");
        }
    }
}
