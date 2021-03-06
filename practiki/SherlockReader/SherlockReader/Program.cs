using System;
using System.IO;
using System.Diagnostics;

namespace SherlockReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileToRead = @"C:\Users\Katya\source\repos\SherlockReader\fileToRead.txt"; 

            StreamReader reader = new StreamReader(fileToRead);
            StreamWriter writer = new StreamWriter("./Sherlock.txt");
            string line = "";
            while (true)
            {
                line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                if(line.Contains("Sherlock"))
                {
                    writer.WriteLine(line);
                }
            }
            reader.Close();
            writer.Close();
            Process proc = Process.GetCurrentProcess();
            long memBytes = proc.PeakWorkingSet64;
            TimeSpan cpuTime = proc.TotalProcessorTime;
            Console.WriteLine(">> Memory {0:n3} K; CPU {1:n} msec",
                memBytes / 1024.0, cpuTime.TotalMilliseconds);
        }
    }
}
