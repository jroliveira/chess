namespace Chess.Terminal.Test.Scenarios
{
    using Chess.Multiplayer;
    using Chess.Terminal.Lib;

    using Moq;

    using Xunit;

    public abstract class ScenarioTests
    {
        protected ScenarioTests()
        {
            this.GameMock = new Mock<IGameMultiplayer>();
            this.ScreenMock = new Mock<IScreen>();
        }

        protected Mock<IGameMultiplayer> GameMock { get; set; }

        protected Mock<IScreen> ScreenMock { get; set; }

        [Fact]
        public void StartDeveChamarCleanUmaVez()
        {
            this.Start();

            this.ScreenMock.Verify(screen => screen.ClearScreen(), Times.Once);
        }

        protected abstract void Start();
    }
}