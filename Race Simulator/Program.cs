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

            Visualize.Initialize(Data.CurrentRace);
            Visualize.DrawTrack();

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
