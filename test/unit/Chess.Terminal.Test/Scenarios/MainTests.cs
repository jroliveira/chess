namespace Chess.Terminal.Test.Scenarios
{
    using Chess.Terminal.Scenarios;
    using Chess.Terminal.Scenarios.Matches;
    using Chess.Terminal.Scenarios.Multiplayer;

    using Moq;

    using Xunit;

    public class MainTests : ScenarioTests
    {
        private readonly Mock<Offline> matchMock;
        private readonly Mock<Client> clientMock;
        private readonly Mock<Server> serverMock;

        public MainTests()
        {
            this.ScreenMock.Setup(text => text.GetChar()).Returns('1');

            this.matchMock = new Mock<Offline>();
            this.clientMock = new Mock<Client>();
            this.serverMock = new Mock<Server>();
        }

        [Fact]
        public void StartDeveChamarWriteInsideTheBoxUmaVez()
        {
            this.Start();

            this.ScreenMock.Verify(writer => writer.WriteTitle("choose an option"), Times.Once);
        }

        [Fact]
        public void StartDeveChamarWriteOptionTresVez()
        {
            this.Start();

            this.ScreenMock.Verify(writer => writer.WriteOption(It.IsAny<char>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public void StartDeveChamarReadKeyUmaVez()
        {
            this.Start();

            this.ScreenMock.Verify(reader => reader.GetChar(), Times.Once);
        }

        [Fact]
        public void StartDadoUmaOpcaoUmDeveChamarMatchStartUmaVez()
        {
            this.ScreenMock.Setup(text => text.GetChar()).Returns('1');

            this.Start();

            this.matchMock.Verify(match => match.Start(), Times.Once);
        }

        [Fact]
        public void StartDadoUmaOpcaoDoisDeveChamarServerStartUmaVez()
        {
            this.ScreenMock.Setup(text => text.GetChar()).Returns('2');

            this.Start();

            this.serverMock.Verify(server => server.Start(), Times.Once);
        }

        [Fact]
        public void StartDadoUmaOpcaoDoisDeveChamarClientStartUmaVez()
        {
            this.ScreenMock.Setup(text => text.GetChar()).Returns('3');

            this.Start();

            this.clientMock.Verify(client => client.Start(), Times.Once);
        }

        protected override void Start()
        {
            var main = new Main(this.GameMock.Object, this.ScreenMock.Object, this.matchMock.Object, this.clientMock.Object, this.serverMock.Object);
            main.Start();
        }
    }
}
