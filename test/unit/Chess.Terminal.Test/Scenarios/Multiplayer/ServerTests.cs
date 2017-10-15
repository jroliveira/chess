namespace Chess.Terminal.Test.Scenarios.Multiplayer
{
    using Chess.Terminal.Scenarios.Matches;
    using Chess.Terminal.Scenarios.Multiplayer;

    using Moq;

    using Xunit;

    public class ServerTests : ScenarioTests
    {
        private readonly Mock<Online> matchMock;

        public ServerTests()
        {
            this.matchMock = new Mock<Online>();
        }

        [Fact]
        public void StartDeveChamarWriteInsideTheBoxUmaVez()
        {
            this.Start();

            this.ScreenMock.Verify(writer => writer.WriteTitle("waiting for oppoenent"), Times.Once);
        }

        [Fact]
        public void StartDeveChamarGameWaitingForOpponentUmaVez()
        {
            this.Start();

            this.GameMock.Verify(game => game.WaitingForOpponent(), Times.Once);
        }

        protected override void Start()
        {
            var server = new Server(this.GameMock.Object, this.ScreenMock.Object, this.matchMock.Object);
            server.Start();
        }
    }
}