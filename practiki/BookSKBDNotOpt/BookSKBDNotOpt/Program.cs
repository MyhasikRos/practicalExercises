using System;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace BookSKBDNotOpt
{
    class BookSKBD
    {
        private string filePath;
        public BookSKBD(string filePath)
        {
            this.filePath = filePath;
        }
        public void Add(Book book)
        {
            StringBuilder buider = new StringBuilder();
            StreamWriter writer = new StreamWriter(filePath, true);
            buider.Append(book.id).Append(",").Append(book.name).Append(",").Append(book.author).Append(",").Append(book.year);
            writer.WriteLine();
        }
        public bool DeleteById(int id)
        {
            string tempFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
            StreamReader sr = new StreamReader(filePath);
            StreamWriter sw = new StreamWriter(tempFile);
            string s = "";
            bool deleted = false;
            while (true)
            {
                s = sr.ReadLine();
                if (s == null)
                {
                    break;
                }
                else if (!deleted)
                {
                    if (int.Parse(s.Split(',')[0]) != id)
                    {
                        sw.WriteLine(s);
                    }
                    else
                    {
                        deleted = true;
                    }
                }
                else
                {
                    sw.WriteLine(s);
                }
            }
            sr.Close();
            sw.Close();
            File.Delete(filePath);
            File.Move(tempFile, filePath);
            return deleted;
        }
        public Book GetById(int id)
        {
            StreamReader sr = new StreamReader(filePath);
            string s = "";
            while (true)
            {
                s = sr.ReadLine();
                if (s == null)
                {
                    break;
                }
                else if(s.Split(',')[0] == id.ToString())
                {
                    string[] parts = s.Split(',');
                    return new Book(int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]));
                }
            }
            return null;
        }
        public Book[] GetAll()
        {
            Book[] result = new Book[0];
            StreamReader sr = new StreamReader(filePath);
            string s = "";
            while (true)
            {
                s = sr.ReadLine();
                if (s == null)
                {
                    break;
                }
                string[] parts = s.Split(',');
                Array.Resize(ref result, result.Length + 1);
                result[result.Length - 1] = new Book(int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]));
            }
            return result;
        }
    }
    class Book
    {
        public int id;
        public string name;
        public string author;
        public int year;
        public Book()
        {

        }
        public Book(int id, string name, string author, int year)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.year = year;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
