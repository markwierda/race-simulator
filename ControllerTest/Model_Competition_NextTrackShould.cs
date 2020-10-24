using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish
            };
            
            Track assen = new Track("TT Circuit Assen", sections);
            _competition.Tracks.Enqueue(assen);

            Track result = _competition.NextTrack();
            Assert.AreEqual(assen, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish
            };
            
            Track assen = new Track("TT Circuit Assen", sections);
            _competition.Tracks.Enqueue(assen);
            _ = _competition.NextTrack();
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish
            };
            
            Track assen = new Track("TT Circuit Assen", sections);
            Track zandvoort = new Track("Circuit Zandvoort", sections);

            _competition.Tracks.Enqueue(assen); 
            _competition.Tracks.Enqueue(zandvoort);
            _ = _competition.NextTrack();
            Track result = _competition.NextTrack();
            Assert.AreEqual(zandvoort, result);
        }
    }
}
