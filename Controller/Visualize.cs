﻿using Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Controller
{
    public static class Visualize
    {
        #region graphics
        private static readonly string[] _StartGrid0 = { "|  |", "|--|", "|--|", "|12|" };
        private static readonly string[] _StartGrid1 = { "----", "1-- ", "2-- ", "----" };
        private static readonly string[] _StartGrid2 = { "|12|", "|--|", "|--|", "|  |" };
        private static readonly string[] _StartGrid3 = { "----", " --1", " --2", "----" };

        private static readonly string[] _Straight0 = { "|  |", "|12|", "|  |", "|  |" };
        private static readonly string[] _Straight1 = { "----", " 1  ", " 2  ", "----" };
        private static readonly string[] _Straight2 = { "|  |", "|  |", "|12|", "|  |" };
        private static readonly string[] _Straight3 = { "----", "  1 ", "  2 ", "----" };

        private static readonly string[] _RightCorner0 = { " /--", "/   ", "|1 2", "|  |" };
        private static readonly string[] _RightCorner1 = { "--\\ ", "   \\", "1 2|", "|  |" };
        private static readonly string[] _RightCorner2 = { "|  |", "1 2|", "   /", "--/ " };
        private static readonly string[] _RightCorner3 = { "|  |", "|1 2", "\\   ", " \\--" };

        private static readonly string[] _LeftCorner0 = { "--\\ ", "   \\", "1 2|", "-  |" };
        private static readonly string[] _LeftCorner1 = { "-  |", "1 2|", "   /", "--/ " };
        private static readonly string[] _LeftCorner2 = { "|  -", "|1 2", "\\   ", " \\--" };
        private static readonly string[] _LeftCorner3 = { " /--", "/   ", "|1  2", "|  -" };

        private static readonly string[] _Finish0 = { "|12|", "|##|", "|##|", "|  |" };
        private static readonly string[] _Finish1 = { "----", "  #1", "  #2", "----" };
        private static readonly string[] _Finish2 = { "|  |", "|##|", "|##|", "|12|" };
        private static readonly string[] _Finish3 = { "----", " 1# ", " 2# ", "----" };
        #endregion

        private static int CursorLeft;
        private static int CursorTop;
        private static int CurrentDirection;
        private static Dictionary<string, string[]> Graphics;

        public static void Initialize()
        {
            CursorLeft = Console.CursorLeft;
            CursorTop = Console.CursorTop;
            CurrentDirection = 1;
            Graphics = new Dictionary<string, string[]>();
            FillGraphicsDictionary();
        }

        public static void DrawTrack(Track track)
        {
            Console.Clear();

            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.LeftCorner:
                        UpdateCursorPosition(CurrentDirection);
                        DrawSection(section);

                        CurrentDirection--;
                        if (CurrentDirection == -1)
                            CurrentDirection = 3;
                        break;
                    case SectionTypes.RightCorner:
                        UpdateCursorPosition(CurrentDirection);
                        DrawSection(section);

                        CurrentDirection++;
                        if (CurrentDirection == 4)
                            CurrentDirection = 0;
                        break;
                    default:
                        UpdateCursorPosition(CurrentDirection);
                        DrawSection(section);
                        break;
                }
            }
        }

        public static void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            DrawTrack(e.Track);
        }

        public static void OnRaceFinished(object sender, RaceFinishedEventArgs e)
        {
            Data.Competition.AwardPoints(e.FinishedParticipants);
            DrawStats();
            Data.NextRace();
            if (Data.CurrentRace != null)
            {
                DrawTrack(Data.CurrentRace.Track);
            }
        }

        private static void DrawSection(Section section)
        {
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            string[] lines = AddParticipantsToGraphics(Graphics[$"_{section.SectionType}{CurrentDirection}"], sectionData);

            foreach (string line in lines)
            {
                Console.SetCursorPosition(CursorTop, CursorLeft);
                Console.Write(line);
                CursorLeft++;
            }

            CursorLeft -= 4;
        }

        private static void DrawStats()
        {
            Console.Clear();
            Console.WriteLine(Data.Competition.ParticipantPoints.GetBestParticipant());
            Console.WriteLine(Data.Competition.ParticipantLapTime.GetBestParticipant());
            Console.WriteLine(Data.Competition.ParticipantTimeBroken.GetBestParticipant());
            Console.WriteLine(Data.Competition.ParticipantPerformanceBeforeAndAfter.GetBestParticipant());
            Thread.Sleep(3000);
        }

        private static string[] AddParticipantsToGraphics(string[] graphics, SectionData sectionData)
        {
            string[] newGraphics = new string[graphics.Length];

            for (int i = 0; i < graphics.Length; i++)
                newGraphics[i] = graphics[i];

            if (sectionData.Left != null)
                for (int i = 0; i < newGraphics.Length; i++)
                    newGraphics[i] = newGraphics[i].Replace("1", sectionData.Left.Equipment.IsBroken ? "x" : sectionData.Left.Name.Substring(0, 1));
            else
                for (int i = 0; i < newGraphics.Length; i++)
                    newGraphics[i] = newGraphics[i].Replace("1", " ");

            if (sectionData.Right != null)
                for (int i = 0; i < newGraphics.Length; i++)
                    newGraphics[i] = newGraphics[i].Replace("2", sectionData.Right.Equipment.IsBroken ? "x" : sectionData.Right.Name.Substring(0, 1));
            else
                for (int i = 0; i < newGraphics.Length; i++)
                    newGraphics[i] = newGraphics[i].Replace("2", " ");

            return newGraphics;
        }

        private static void UpdateCursorPosition(int currentDirection)
        {
            if (currentDirection == 0)
                CursorLeft -= 4;
            else if (currentDirection == 1)
                CursorTop += 4;
            else if (currentDirection == 2)
                CursorLeft += 4;
            else if (currentDirection == 3)
                CursorTop -= 4;
        }

        private static void FillGraphicsDictionary()
        {
            Graphics.Add("_StartGrid0", _StartGrid0);
            Graphics.Add("_StartGrid1", _StartGrid1);
            Graphics.Add("_StartGrid2", _StartGrid2);
            Graphics.Add("_StartGrid3", _StartGrid3);

            Graphics.Add("_Straight0", _Straight0);
            Graphics.Add("_Straight1", _Straight1);
            Graphics.Add("_Straight2", _Straight2);
            Graphics.Add("_Straight3", _Straight3);

            Graphics.Add("_RightCorner0", _RightCorner0);
            Graphics.Add("_RightCorner1", _RightCorner1);
            Graphics.Add("_RightCorner2", _RightCorner2);
            Graphics.Add("_RightCorner3", _RightCorner3);

            Graphics.Add("_LeftCorner0", _LeftCorner0);
            Graphics.Add("_LeftCorner1", _LeftCorner1);
            Graphics.Add("_LeftCorner2", _LeftCorner2);
            Graphics.Add("_LeftCorner3", _LeftCorner3);

            Graphics.Add("_Finish0", _Finish0);
            Graphics.Add("_Finish1", _Finish1);
            Graphics.Add("_Finish2", _Finish2);
            Graphics.Add("_Finish3", _Finish3);
        }
    }
}