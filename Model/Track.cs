﻿using System.Collections.Generic;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, LinkedList<Section> sections)
        {
            Name = name;
            Sections = sections;
        }
    }
}
