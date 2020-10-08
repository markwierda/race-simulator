using System;

namespace Model
{
    public class DriversChangedEventArgs : EventArgs
    {
        public Track Track { get; set; }

        public delegate void DriversChanged(DriversChangedEventArgs e);

        public DriversChangedEventArgs(Track track)
        {
            Track = track;
        }
    }
}
