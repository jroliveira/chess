using Chess.UI.Console.Scenarios.Matches;
using Chess.UI.Console.Scenarios.Multiplayer;
using Moq;
using NUnit.Framework;

namespace Chess.UI.Console.Test.Scenarios.Multiplayer
{
    [TestFixture]
    public class ServerTests : ScenarioTests
    {
        private Mock<Online> _matchMock;

        [SetUp]
        public new void Setup()
        {
            base.Setup();

            _matchMock = new Mock<Online>();
        }

        [Test]
        public void Start_DeveChamarWriteInsideTheBoxUmaVez()
        {
            Start();

            WriterMock.Verify(writer => writer.WriteInsideTheBox("waiting for oppoenent"), Times.Once);
        }

        [Test]
        public void Start_DeveChamarGameWaitingForOpponentUmaVez()
        {
            Start();

            GameMock.Verify(game => game.WaitingForOpponent(), Times.Once);
        }

        protected override void Start()
        {
            var server = new Server(GameMock.Object, _matchMock.Object, WriterMock.Object, ReaderMock.Object, ScreenMock.Object);
            server.Start();
        }
    }
}