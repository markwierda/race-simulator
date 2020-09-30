using Model;
using System;
using System.Collections.Generic;

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

        private static Race Race;
        private static int CursorLeft;
        private static int CursorTop;
        private static int CurrentDirection;
        private static Dictionary<string, string[]> Graphics;

        public static void Initialize(Race race)
        {
            Race = race;
            CursorLeft = Console.CursorLeft;
            CursorTop = Console.CursorTop;
            CurrentDirection = 1;
            Graphics = new Dictionary<string, string[]>();
            FillGraphicsDictionary();
        }

        public static void DrawTrack()
        {
            foreach (Section section in Race.Track.Sections)
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

        private static void DrawSection(Section section)
        {
            AddParticipantsToSection(section);

            foreach (string line in Graphics[$"_{section.SectionType}{CurrentDirection}"])
            {
                Console.SetCursorPosition(CursorTop, CursorLeft);
                Console.Write(line);
                CursorLeft++;
            }

            CursorLeft -= 4;
        }

        private static void AddParticipantsToSection(Section section)
        {
            SectionData sectionData = Race.GetSectionData(section);
            string[] sectionArray = Graphics[$"_{section.SectionType}{CurrentDirection}"];

            if (sectionData.Left != null)
            {
                for (int i = 0; i < sectionArray.Length; i++)
                {
                    sectionArray[i] = sectionArray[i].Replace("1", sectionData.Left.Name.Substring(0, 1));
                }

                Graphics[$"_{section.SectionType}{CurrentDirection}"] = sectionArray;
            }
            else
            {
                for (int i = 0; i < sectionArray.Length; i++)
                {
                    sectionArray[i] = sectionArray[i].Replace("1", " ");
                }

                Graphics[$"_{section.SectionType}{CurrentDirection}"] = sectionArray;
            }

            if (sectionData.Right != null)
            {
                for (int i = 0; i < sectionArray.Length; i++)
                {
                    sectionArray[i] = sectionArray[i].Replace("2", sectionData.Right.Name.Substring(0, 1));
                }

                Graphics[$"_{section.SectionType}{CurrentDirection}"] = sectionArray;
            }
            else
            {
                for (int i = 0; i < sectionArray.Length; i++)
                {
                    sectionArray[i] = sectionArray[i].Replace("2", " ");
                }

                Graphics[$"_{section.SectionType}{CurrentDirection}"] = sectionArray;
            }
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