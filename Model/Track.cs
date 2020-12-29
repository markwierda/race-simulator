using System.Collections.Generic;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = ArrayToLinkedList(sections);
        }

        private LinkedList<Section> ArrayToLinkedList(SectionTypes[] sections)
        {
            LinkedList<Section> sectionsList = new LinkedList<Section>();

            foreach (SectionTypes section in sections)
            {
                sectionsList.AddLast(
                    new Section()
                    {
                        SectionType = section
                    }
                );
            }

            return sectionsList;
        }
    }
}