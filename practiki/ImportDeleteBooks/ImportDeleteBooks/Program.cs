using System;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Text;
namespace ImportDeleteBooks
{
    class Book
    {
        public int id;
        public int pages;
        public string createdAt;
        public string title;
        public Book(int id, string title, int pages, string createdAt)
        {
            this.id = id;
            this.pages = pages;
            this.createdAt = createdAt;
            this.title = title;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool working = true;
            SqliteConnection connection = new SqliteConnection(@"Data Source=C:\Users\Katya\MishaProga\practiki\BookGenerator\BookData.db");
            while(working)
            {
                string command = Console.ReadLine();
                string[] commandParts = command.Split(' ');
                if (commandParts[0] == "import" && commandParts.Length == 2)
                {
                    connection.Open();
                    SqliteCommand commandSql = connection.CreateCommand();
                    long result = Insert(GenerateSqlRequest(commandParts[1]), connection);
                    Console.WriteLine("Items added: " + result);
                    connection.Close();
                }
                else if(command == "deleteAll")
                {
                    connection.Open();
                    SqliteCommand commandSql = connection.CreateCommand();
                    commandSql.CommandText =
                    @"
                        DELETE FROM books
                    ";
                    int nChanged = commandSql.ExecuteNonQuery();
                    Console.WriteLine("Items deleted: " + nChanged);
                    connection.Close();
                }
                else if (command == "exit")
                {
                    working = false;
                }
                else
                {
                    Console.WriteLine("Wrong");
                }
            }
        }
        static long Insert(string request, SqliteConnection connection)
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = request;
            return (long)command.ExecuteNonQuery();
        }
        static string GenerateSqlRequest(string filePath)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@$"
                INSERT INTO books (id, title, pages, createdAt) 
                VALUES ");
            StreamReader reader = new StreamReader(filePath);
            string line = "";
            while (true)
            {
                line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                string[] lineParsed = line.Split(",");
                builder.Append("(").Append(lineParsed[0]).Append(",\"").Append(lineParsed[1]).Append("\",").Append(lineParsed[2]).Append(",\"").Append(lineParsed[3]).Append("\"),").Append(Environment.NewLine);
            }
            builder.Remove(builder.Length - 3, 3).Append(";");
            return builder.ToString();
        }

    }
}
