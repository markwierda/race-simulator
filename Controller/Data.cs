using Model;
using System;
using System.Collections.Generic;

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
            Car car = new Car(10, 10, 300, false);

            Driver mark = new Driver("Mark", 1, car, TeamColors.Red);
            Driver leroy = new Driver("Leroy", 2, car, TeamColors.Green);
            Driver pieter = new Driver("Pieter", 3, car, TeamColors.Blue);

            Competition.Participants.Add(mark);
            Competition.Participants.Add(leroy);
            Competition.Participants.Add(pieter);
        }

        public static void AddTracksToCompetition()
        {
            LinkedList<Section> section = new LinkedList<Section>();

            Track assen = new Track("TT Circuit Assen", section);
            Track zandvoort = new Track("Circuit Zandvoort", section);

            Competition.Tracks.Enqueue(assen);
            Competition.Tracks.Enqueue(zandvoort);
        }

        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();

            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
            }
        }
    }
}
