using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_SetStartPositionParticipantsShould
    {
        private Race _race;

        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();

            _race = new Race(Data.CurrentRace.Track, Data.CurrentRace.Participants);
        }

        [Test]
        public void SetStartPositionParticipants_ParticipantsInStartGrid_NotNull()
        {
            foreach (Section section in _race.Track.Sections)
            {
                SectionData sectionData = _race.GetSectionData(section);

                if (section.SectionType == SectionTypes.StartGrid)
                {
                    Assert.NotNull(sectionData.Left);
                    Assert.NotNull(sectionData.Right);
                }
            }
        }
    }
}
