using Model;
using System;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipantsToCompetition();
            AddTracksToCompetition();
        }

        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();
            CurrentRace = nextTrack != null ? new Race(nextTrack, Competition.Participants) : null;
        }

        private static void AddParticipantsToCompetition()
        {
            Car car1 = new Car(10, 10, 10, false);
            Car car2 = new Car(10, 10, 10, false);

            Driver mark = new Driver("Mark", 0, car1, TeamColors.Red);
            Driver leroy = new Driver("Leroy", 0, car2, TeamColors.Blue);

            Competition.Participants.Add(mark);
            Competition.Participants.Add(leroy);

            Competition.ParticipantPoints.Add(new DataParticipantPoints(mark, mark.Points));
            Competition.ParticipantPoints.Add(new DataParticipantPoints(leroy, leroy.Points));

            Competition.ParticipantTimeBroken.Add(new DataParticipantTimeBroken(mark, new TimeSpan()));
            Competition.ParticipantTimeBroken.Add(new DataParticipantTimeBroken(leroy, new TimeSpan()));
        }

        private static void AddTracksToCompetition()
        {
            SectionTypes[] assenSections =
            {
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
            };

            SectionTypes[] zandvoortSections =
            {
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
            };

            Track assen = new Track("TT Circuit Assen", assenSections);
            Track zandvoort = new Track("Circuit Zandvoort", zandvoortSections);

            Competition.Tracks.Enqueue(assen);
            Competition.Tracks.Enqueue(zandvoort);
        }
    }
}