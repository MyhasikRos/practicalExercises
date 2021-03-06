using System;
using System.Diagnostics;

namespace ClassesAndObjects
{
    class ETiket
    {
        public string performName;
        public int rowNum;
        public int placeNum;
        public DateTime concertStart;
        public ETiket()
        {

        }
        public ETiket(string performName, int rowNum, int placeNum, DateTime concertStart)
        {
            this.performName = performName;
            this.rowNum = rowNum;
            this.placeNum = placeNum;
            this.concertStart = concertStart;
        }
        public TimeSpan TimeLeft()
        {
            return concertStart - DateTime.Now;
        }
        public override string ToString()
        {
            return $"Concert \"{performName}\". Row {rowNum} place {placeNum}. Starts at {concertStart}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            RunTests();
            DateTime tomorrow = DateTime.Now.AddDays(1);
            DateTime inAWeek = DateTime.Now.AddDays(7);
            ETiket tiket0 = new ETiket();
            Console.WriteLine(tiket0.ToString());
            Console.WriteLine("Time left: " + tiket0.TimeLeft());
            ETiket tiket1 = new ETiket("Titanik", 10, 15, tomorrow);
            Console.WriteLine(tiket1.ToString());
            Console.WriteLine("Time left: " + tiket1.TimeLeft());
            ETiket tiket2 = new ETiket("Romeo&Juliet", 22, 13, inAWeek);
            Console.WriteLine(tiket2.ToString());
            Console.WriteLine("Time left: " + tiket2.TimeLeft());
        }
        static void RunTests()
        {
            ETiket test = new ETiket();
            test.concertStart = DateTime.Now.AddHours(3);
            Debug.Assert(test.TimeLeft() >  new TimeSpan(2, 59, 59) && test.TimeLeft() < new TimeSpan(3, 0, 1), "Wrong method"); //погрешность
            test.concertStart = DateTime.Now.AddMinutes(10);
            Debug.Assert(test.TimeLeft() > new TimeSpan(0, 9, 59) && test.TimeLeft() < new TimeSpan(0, 10, 1), "Wrong method");
            test.concertStart = DateTime.Now.AddDays(4);
            Debug.Assert(test.TimeLeft() > new TimeSpan(3, 23, 59, 59) && test.TimeLeft() < new TimeSpan(4, 0, 0, 1), "Wrong method");
        }
    }
}
