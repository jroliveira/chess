using Chess.UI.Console.Scenarios;
using Chess.UI.Console.Scenarios.Matches;
using Chess.UI.Console.Scenarios.Multiplayer;
using Moq;
using NUnit.Framework;

namespace Chess.UI.Console.Test.Scenarios
{
    [TestFixture]
    public class MainTests : ScenarioTests
    {
        private Mock<Offline> _matchMock;
        private Mock<Client> _clientMock;
        private Mock<Server> _serverMock;

        [SetUp]
        public new void Setup()
        {
            base.Setup();

            ReaderMock.Setup(text => text.ReadKey()).Returns('1');

            _matchMock = new Mock<Offline>();
            _clientMock = new Mock<Client>();
            _serverMock = new Mock<Server>();
        }

        [Test]
        public void Start_DeveChamarWriteInsideTheBoxUmaVez()
        {
            Start();

            WriterMock.Verify(writer => writer.WriteInsideTheBox("choose an option"), Times.Once);
        }

        [Test]
        public void Start_DeveChamarWriteOptionTresVez()
        {
            Start();

            WriterMock.Verify(writer => writer.WriteOption(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        public void Start_DeveChamarReadKeyUmaVez()
        {
            Start();

            ReaderMock.Verify(reader => reader.ReadKey(), Times.Once);
        }

        [Test]
        public void Start_DadoUmaOpcaoUm_DeveChamarMatchStartUmaVez()
        {
            ReaderMock.Setup(text => text.ReadKey()).Returns('1');

            Start();

            _matchMock.Verify(match => match.Start(), Times.Once);
        }

        [Test]
        public void Start_DadoUmaOpcaoDois_DeveChamarServerStartUmaVez()
        {
            ReaderMock.Setup(text => text.ReadKey()).Returns('2');

            Start();

            _serverMock.Verify(server => server.Start(), Times.Once);
        }

        [Test]
        public void Start_DadoUmaOpcaoDois_DeveChamarClientStartUmaVez()
        {
            ReaderMock.Setup(text => text.ReadKey()).Returns('3');

            Start();

            _clientMock.Verify(client => client.Start(), Times.Once);
        }

        protected override void Start()
        {
            var main = new Main(GameMock.Object, _matchMock.Object, _clientMock.Object, _serverMock.Object, WriterMock.Object, ReaderMock.Object, ScreenMock.Object);
            main.Start();
        }
    }
}
