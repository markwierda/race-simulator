using Controller;
using System;
using System.Threading;

namespace Race_Simulator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();

            Console.WriteLine($"Current track: {Data.CurrentRace.Track.Name}");

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
