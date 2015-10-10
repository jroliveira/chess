namespace Chess.UI.Console.Test.Scenarios
{
    using Chess.UI.Console.Scenarios;
    using Chess.UI.Console.Scenarios.Matches;
    using Chess.UI.Console.Scenarios.Multiplayer;

    using Moq;

    using Xunit;

    public class MainTests : ScenarioTests
    {
        private readonly Mock<Offline> matchMock;
        private readonly Mock<Client> clientMock;
        private readonly Mock<Server> serverMock;

        public MainTests()
        {
            this.ReaderMock.Setup(text => text.ReadKey()).Returns('1');

            this.matchMock = new Mock<Offline>();
            this.clientMock = new Mock<Client>();
            this.serverMock = new Mock<Server>();
        }

        [Fact]
        public void Start_DeveChamarWriteInsideTheBoxUmaVez()
        {
            this.Start();

            this.WriterMock.Verify(writer => writer.WriteInsideTheBox("choose an option"), Times.Once);
        }

        [Fact]
        public void Start_DeveChamarWriteOptionTresVez()
        {
            this.Start();

            this.WriterMock.Verify(writer => writer.WriteOption(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public void Start_DeveChamarReadKeyUmaVez()
        {
            this.Start();

            this.ReaderMock.Verify(reader => reader.ReadKey(), Times.Once);
        }

        [Fact]
        public void Start_DadoUmaOpcaoUm_DeveChamarMatchStartUmaVez()
        {
            this.ReaderMock.Setup(text => text.ReadKey()).Returns('1');

            this.Start();

            this.matchMock.Verify(match => match.Start(), Times.Once);
        }

        [Fact]
        public void Start_DadoUmaOpcaoDois_DeveChamarServerStartUmaVez()
        {
            this.ReaderMock.Setup(text => text.ReadKey()).Returns('2');

            this.Start();

            this.serverMock.Verify(server => server.Start(), Times.Once);
        }

        [Fact]
        public void Start_DadoUmaOpcaoDois_DeveChamarClientStartUmaVez()
        {
            this.ReaderMock.Setup(text => text.ReadKey()).Returns('3');

            this.Start();

            this.clientMock.Verify(client => client.Start(), Times.Once);
        }

        protected override void Start()
        {
            var main = new Main(this.GameMock.Object, this.matchMock.Object, this.clientMock.Object, this.serverMock.Object, this.WriterMock.Object, this.ReaderMock.Object, this.ScreenMock.Object);
            main.Start();
        }
    }
}
