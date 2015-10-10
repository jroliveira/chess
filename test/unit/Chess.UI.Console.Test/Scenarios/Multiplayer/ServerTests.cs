namespace Chess.UI.Console.Test.Scenarios.Multiplayer
{
    using Chess.UI.Console.Scenarios.Matches;
    using Chess.UI.Console.Scenarios.Multiplayer;

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
        public void Start_DeveChamarWriteInsideTheBoxUmaVez()
        {
            this.Start();

            this.WriterMock.Verify(writer => writer.WriteInsideTheBox("waiting for oppoenent"), Times.Once);
        }

        [Fact]
        public void Start_DeveChamarGameWaitingForOpponentUmaVez()
        {
            this.Start();

            this.GameMock.Verify(game => game.WaitingForOpponent(), Times.Once);
        }

        protected override void Start()
        {
            var server = new Server(this.GameMock.Object, this.matchMock.Object, this.WriterMock.Object, this.ReaderMock.Object, this.ScreenMock.Object);
            server.Start();
        }
    }
}