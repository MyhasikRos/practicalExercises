using System;
using System.Text;
using System.IO;
namespace BookGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            const int generateNum = 1000;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < generateNum;i++)
            {
                builder.AppendLine(GenerateBook(i+1));
            }
            File.WriteAllText(@"C:\Users\Katya\MishaProga\practiki\BookGenerator\booksTest.csv", builder.ToString());
        }
        static string GenerateBook(int id)
        {
            string[] titles = new string[] { "Novel", "Magazine", "Book", "Tom1" };
            int maxPage = 1000, minPage = 10;
            StringBuilder builder = new StringBuilder();
            Random rand = new Random();
            builder.Append(id).Append(",").Append(titles[rand.Next(0, titles.Length)]).Append(",").Append(rand.Next(minPage, maxPage)).Append(",").Append(rand.Next(1990, 2021)).Append("-").Append(rand.Next(1, 31).ToString("D2")).Append("-").Append(rand.Next(1, 13).ToString("D2"));
            return builder.ToString();
        }
    }
}
