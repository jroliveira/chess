namespace Chess.UI.Console.Test.Scenarios
{
    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;

    using Moq;

    using Xunit;

    public abstract class ScenarioTests
    {
        protected ScenarioTests()
        {
            this.GameMock = new Mock<IGameMultiplayer>();
            this.ScreenMock = new Mock<IScreen>();
            this.WriterMock = new Mock<IWriter>();
            this.ReaderMock = new Mock<IReader>();
        }

        protected Mock<IGameMultiplayer> GameMock { get; set; }

        protected Mock<IScreen> ScreenMock { get; set; }

        protected Mock<IWriter> WriterMock { get; set; }

        protected Mock<IReader> ReaderMock { get; set; }

        [Fact]
        public void Start_DeveChamarCleanUmaVez()
        {
            this.Start();

            this.ScreenMock.Verify(screen => screen.Clean(), Times.Once);
        }

        protected abstract void Start();
    }
}