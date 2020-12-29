﻿namespace Model
{
    public enum SectionTypes
    {
        Straight,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish
    }

    public class Section
    {
        public SectionTypes SectionType { get; set; }
    }
}