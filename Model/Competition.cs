using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public RaceData<DataParticipantPoints> ParticipantPoints { get; set; }
        public RaceData<DataParticipantLapTime> ParticipantLapTime { get; set; }
        public RaceData<DataParticipantTimeBroken> ParticipantTimeBroken { get; set; }
        public RaceData<DataParticipantPerformanceBeforeAndAfter> ParticipantPerformanceBeforeAndAfter { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            ParticipantPoints = new RaceData<DataParticipantPoints>();
            ParticipantLapTime = new RaceData<DataParticipantLapTime>();
            ParticipantTimeBroken = new RaceData<DataParticipantTimeBroken>();
            ParticipantPerformanceBeforeAndAfter = new RaceData<DataParticipantPerformanceBeforeAndAfter>();
            Tracks = new Queue<Track>();
        }

        public Track NextTrack()
        {
            if (Tracks.Any())
            {
                return Tracks.Dequeue();
            }

            return null;
        }

        public void OnRaceFinished(object sender, RaceFinishedEventArgs e)
        {
            AwardPoints(e.FinishedParticipants);
            PrintStats();
        }

        public void AwardPoints(Stack<IParticipant> FinishedParticipants)
        {
            int points = 1;

            while (FinishedParticipants.Count > 0)
            {
                IParticipant participant = FinishedParticipants.Pop();
                ParticipantPoints.Add(new DataParticipantPoints(participant, points));
                participant.Points = points;
                points += points;
            }
        }

        private void PrintStats()
        {
            Console.Clear();
            Console.WriteLine(ParticipantPoints.GetBestParticipant());
            Console.WriteLine(ParticipantLapTime.GetBestParticipant());
            Console.WriteLine(ParticipantTimeBroken.GetBestParticipant());
            Console.WriteLine(ParticipantPerformanceBeforeAndAfter.GetBestParticipant());
            Thread.Sleep(3000);
        }
    }
}