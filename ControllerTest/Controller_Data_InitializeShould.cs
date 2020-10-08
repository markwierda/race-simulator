using Controller;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Data_InitializeShould
    {
        [Test]
        public void Initialize_Competition_NotNull()
        {
            Data.Initialize();
            Assert.NotNull(Data.Competition);
        }

        [Test]
        public void Initialize_ParticipantsInCompetition_IsNotEmpty()
        {
            Data.Initialize();
            Assert.IsNotEmpty(Data.Competition.Participants);
        }

        [Test]
        public void Initialize_TracksInCompetition_IsNotEmpty()
        {
            Data.Initialize();
            Assert.IsNotEmpty(Data.Competition.Tracks);
        }
    }
}
