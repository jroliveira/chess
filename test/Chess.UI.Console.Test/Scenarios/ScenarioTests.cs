using Chess.UI.Console.Libs;
using Moq;
using NUnit.Framework;

namespace Chess.UI.Console.Test.Scenarios
{
    public abstract class ScenarioTests
    {
        protected Mock<ChessGame> GameMock;
        protected Mock<IScreen> ScreenMock;
        protected Mock<IWriter> WriterMock;
        protected Mock<IReader> ReaderMock;

        public void Setup()
        {
            GameMock = new Mock<ChessGame>();
            ScreenMock = new Mock<IScreen>();
            WriterMock = new Mock<IWriter>();
            ReaderMock = new Mock<IReader>();
        }

        [Test]
        public void Start_DeveChamarCleanUmaVez()
        {
            Start();

            ScreenMock.Verify(screen => screen.Clean(), Times.Once);
        }

        protected abstract void Start();
    }
}