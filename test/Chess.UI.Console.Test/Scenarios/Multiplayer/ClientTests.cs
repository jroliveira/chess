using Chess.UI.Console.Scenarios.Matches;
using Chess.UI.Console.Scenarios.Multiplayer;
using Moq;
using NUnit.Framework;

namespace Chess.UI.Console.Test.Scenarios.Multiplayer
{
    [TestFixture]
    public class ClientTests : ScenarioTests
    {
        private Mock<Online> _matchMock;

        [SetUp]
        public new void Setup()
        {
            base.Setup();

            ReaderMock
                .SetupSequence(text => text.ReadValue())
                .Returns("127.0.0.1")
                .Returns("11000");

            _matchMock = new Mock<Online>();
        }

        [Test]
        public void Start_DeveChamarWriteInsideTheBoxUmaVez()
        {
            Start();

            WriterMock.Verify(writer => writer.WriteInsideTheBox("connection data"), Times.Once);
        }

        [Test]
        public void Start_DeveChamarReadValueDuasVez()
        {
            Start();

            ReaderMock.Verify(reader => reader.ReadValue(), Times.Exactly(2));
        }

        [Test]
        public void Start_DadoUmaIpAddressEUmaPorta_DeveChamarGameConnectUmaVez()
        {
            ReaderMock
                .SetupSequence(text => text.ReadValue())
                .Returns("127.0.0.1")
                .Returns("11000");

            Start();

            GameMock.Verify(game => game.Connect("127.0.0.1", "11000"), Times.Once);
        }

        protected override void Start()
        {
            var client = new Client(GameMock.Object, _matchMock.Object, WriterMock.Object, ReaderMock.Object, ScreenMock.Object);
            client.Start();
        }
    }
}