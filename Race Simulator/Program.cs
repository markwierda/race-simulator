using Controller;
using System.Threading;

namespace Race_Simulator
{
    public class Program
    {
        public static void Main()
        {
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.DriversChanged += Visualize.OnDriversChanged;

            Visualize.Initialize();
            Visualize.DrawTrack(Data.CurrentRace.Track);

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
