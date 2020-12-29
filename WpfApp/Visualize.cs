using Controller;
using Model;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace WpfApp
{
    public static class Visualize
    {
        #region graphics

        private const string _StartGrid = ".\\Images\\StartGrid.png";
        private const string _Finish = ".\\Images\\Finish.png";

        private const string _StraightVertical = ".\\Images\\StraightVertical.png";
        private const string _StraightHorizontal = ".\\Images\\StraightHorizontal.png";

        private const string _Turn0 = ".\\Images\\Turn0.png";
        private const string _Turn1 = ".\\Images\\Turn1.png";
        private const string _Turn2 = ".\\Images\\Turn2.png";
        private const string _Turn3 = ".\\Images\\Turn3.png";

        private const string _CarRed = ".\\Images\\CarRed.png";
        private const string _CarBlue = ".\\Images\\CarBlue.png";
        private const string _Broken = ".\\Images\\Broken.png";

        #endregion graphics

        private static int Width, Height, CurrentDirection, CurrentX, CurrentY, MaxX, MaxY;
        private static readonly int SectionSize = 64;

        public static BitmapSource DrawTrack(Track track)
        {
            CalculateDimensions(track);

            CurrentX = 0;
            CurrentY = 0;
            CurrentDirection = 0;

            Bitmap bitmap = Images.GetEmptyBitmap(Width * SectionSize, Height * SectionSize);
            Graphics graphics = Graphics.FromImage(bitmap);

            if (Data.CurrentRace != null)
            {
                IParticipant mark;
                IParticipant leroy;

                foreach (Section section in track.Sections)
                {
                    mark = Data.CurrentRace.GetSectionData(section).Left;
                    leroy = Data.CurrentRace.GetSectionData(section).Right;

                    switch (section.SectionType)
                    {
                        case SectionTypes.StartGrid:
                            graphics.DrawImage(Images.Get(_StartGrid), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            DrawPlayer(graphics, mark, leroy);
                            Move();
                            break;

                        case SectionTypes.Finish:
                            graphics.DrawImage(Images.Get(_Finish), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            DrawPlayer(graphics, mark, leroy);
                            Move();
                            break;

                        case SectionTypes.Straight:
                            if (CurrentDirection == 0)
                                graphics.DrawImage(Images.Get(_StraightVertical), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 1)
                                graphics.DrawImage(Images.Get(_StraightHorizontal), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 2)
                                graphics.DrawImage(Images.Get(_StraightVertical), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 3)
                                graphics.DrawImage(Images.Get(_StraightHorizontal), new Point(CurrentX * SectionSize, CurrentY * SectionSize));

                            DrawPlayer(graphics, mark, leroy);
                            Move();
                            break;

                        case SectionTypes.RightCorner:
                            if (CurrentDirection == 0)
                                graphics.DrawImage(Images.Get(_Turn3), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 1)
                                graphics.DrawImage(Images.Get(_Turn0), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 2)
                                graphics.DrawImage(Images.Get(_Turn1), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 3)
                                graphics.DrawImage(Images.Get(_Turn2), new Point(CurrentX * SectionSize, CurrentY * SectionSize));

                            DrawPlayer(graphics, mark, leroy);
                            Right();
                            Move();
                            break;

                        case SectionTypes.LeftCorner:
                            if (CurrentDirection == 0)
                                graphics.DrawImage(Images.Get(_Turn0), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 1)
                                graphics.DrawImage(Images.Get(_Turn1), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 2)
                                graphics.DrawImage(Images.Get(_Turn2), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                            else if (CurrentDirection == 3)
                                graphics.DrawImage(Images.Get(_Turn3), new Point(CurrentX * SectionSize, CurrentY * SectionSize));

                            DrawPlayer(graphics, mark, leroy);
                            Left();
                            Move();
                            break;
                    }
                }
            }

            return Images.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

        private static void DrawPlayer(Graphics graphics, IParticipant mark, IParticipant leroy)
        {
            if (mark != null)
            {
                graphics.DrawImage(new Bitmap(Images.Get(_CarRed), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));

                if (mark.Equipment.IsBroken)
                    graphics.DrawImage(new Bitmap(Images.Get(_Broken), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
            }

            if (leroy != null)
            {
                graphics.DrawImage(new Bitmap(Images.Get(_CarBlue), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));

                if (leroy.Equipment.IsBroken)
                    graphics.DrawImage(new Bitmap(Images.Get(_Broken), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));
            }
        }

        private static void CalculateDimensions(Track track)
        {
            CurrentX = 0;
            CurrentY = 0;
            CurrentDirection = 0;

            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                    case SectionTypes.Finish:
                    case SectionTypes.Straight:
                        Move();
                        break;

                    case SectionTypes.RightCorner:
                        Right();
                        Move();
                        break;

                    case SectionTypes.LeftCorner:
                        Left();
                        Move();
                        break;
                }
            }

            Width = MaxX + 1;
            Height = MaxY + 1;
        }

        private static void Move()
        {
            if (CurrentDirection == 0)
                CurrentY--;
            else if (CurrentDirection == 1)
                CurrentX++;
            else if (CurrentDirection == 2)
                CurrentY++;
            else if (CurrentDirection == 3)
                CurrentX--;

            if (CurrentX > MaxX)
                MaxX = CurrentX;

            if (CurrentY > MaxY)
                MaxY = CurrentY;
        }

        public static void Right()
        {
            if (CurrentDirection == 0)
                CurrentDirection = 1;
            else if (CurrentDirection == 1)
                CurrentDirection = 2;
            else if (CurrentDirection == 2)
                CurrentDirection = 3;
            else if (CurrentDirection == 3)
                CurrentDirection = 0;
        }

        public static void Left()
        {
            if (CurrentDirection == 0)
                CurrentDirection = 3;
            else if (CurrentDirection == 1)
                CurrentDirection = 0;
            else if (CurrentDirection == 2)
                CurrentDirection = 1;
            else if (CurrentDirection == 3)
                CurrentDirection = 2;
        }
    }
}