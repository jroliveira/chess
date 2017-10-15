namespace Chess.Terminal.Test.Scenarios.Multiplayer
{
    using Chess.Terminal.Scenarios.Matches;
    using Chess.Terminal.Scenarios.Multiplayer;

    using Moq;

    using Xunit;

    public class ClientTests : ScenarioTests
    {
        private readonly Mock<Online> matchMock;

        public ClientTests()
        {
            this.ScreenMock
                .SetupSequence(text => text.GetString())
                .Returns("127.0.0.1")
                .Returns("11000");

            this.matchMock = new Mock<Online>();
        }

        [Fact]
        public void StartDeveChamarWriteInsideTheBoxUmaVez()
        {
            this.Start();

            this.ScreenMock.Verify(writer => writer.WriteTitle("connection data"), Times.Once);
        }

        [Fact]
        public void StartDeveChamarReadValueDuasVez()
        {
            this.Start();

            this.ScreenMock.Verify(reader => reader.GetString(), Times.Exactly(2));
        }

        [Fact]
        public void StartDadoUmaIpAddressEUmaPortaDeveChamarGameConnectUmaVez()
        {
            this.ScreenMock
                .SetupSequence(text => text.GetString())
                .Returns("127.0.0.1")
                .Returns("11000");

            this.Start();

            this.GameMock.Verify(game => game.Connect("127.0.0.1", "11000"), Times.Once);
        }

        protected override void Start()
        {
            var client = new Client(this.GameMock.Object, this.ScreenMock.Object, this.matchMock.Object);
            client.Start();
        }
    }
}