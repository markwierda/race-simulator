using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_RandomizeEquipmentShould
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
        public void RandomizeEquipment_ParticipantsEquipment_AreNotEqual()
        {
            IParticipant participant1 = _race.Participants[0];
            IParticipant participant2 = _race.Participants[1];
            Assert.AreNotEqual(participant1.Equipment, participant2.Equipment);
        }
    }
}
