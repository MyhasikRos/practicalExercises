using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        const string url = "https://www.ietf.org/rfc/rfc3339.txt";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.UserAgent = "ConsoleTest";
        Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();

        const char let = 'B';
        bool readingAppend = false;
        StringBuilder builder = new StringBuilder();

        StreamReader streamReader = new StreamReader(responseStream);
        string line = "";
        while (line != null)
        {
            line = streamReader.ReadLine();
            if (line != null)
            {
                line = WebUtility.HtmlDecode(Regex.Replace(line, "<[^>]*(>|$)", ""));
                if (line.StartsWith($"Appendix {let}."))
                {
                    readingAppend = true;
                }
                else if (line.StartsWith($"Appendix {(char)((int)let + 1)}."))
                {
                    break;
                }
                if (readingAppend)
                {
                    builder.Append(line).Append(Environment.NewLine);
                }
            }
        }
        Console.WriteLine(builder.ToString());
        streamReader.Close();
    }
}

