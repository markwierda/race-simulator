using System.Collections.Generic;
using System.Linq;

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

        public void AwardPoints(Stack<IParticipant> FinishedParticipants)
        {
            int points = 1;

            while (FinishedParticipants.Count > 0)
            {
                IParticipant participant = FinishedParticipants.Pop();
                ParticipantPoints.Add(new DataParticipantPoints(participant, points));
                points += points;
            }
        }
    }
}
