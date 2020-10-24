using Model;

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

        public static void AddParticipantsToCompetition()
        {
            Car car1 = new Car(10, 10, 10, false);
            Car car2 = new Car(10, 10, 10, false);

            Driver mark = new Driver("Mark", 0, car1, TeamColors.Red);
            Driver leroy = new Driver("Leroy", 0, car2, TeamColors.Green);

            Competition.Participants.Add(mark);
            Competition.Participants.Add(leroy);
        }

        public static void AddTracksToCompetition()
        {
            SectionTypes[] assenSections =
            {
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner
            };

            SectionTypes[] zandvoortSections =
            {
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.StartGrid,
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
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner
            };

            Track assen = new Track("TT Circuit Assen", assenSections);
            Track zandvoort = new Track("Circuit Zandvoort", zandvoortSections);

            Competition.Tracks.Enqueue(assen);
            Competition.Tracks.Enqueue(zandvoort);
        }

        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();
            CurrentRace = nextTrack != null ? new Race(nextTrack, Competition.Participants) : null;
        }
    }
}
