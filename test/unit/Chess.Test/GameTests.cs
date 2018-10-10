namespace Chess.Test
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Data.Commands;

    using FluentAssertions;

    using Moq;

    using Xunit;

    using static Moq.It;
    using static Moq.Times;

    public class GameTests
    {
        private readonly Mock<Chessboard> chessboardMock;
        private readonly Mock<MountChessboardCommand> mountChessboardMock;
        private readonly Game game;

        public GameTests()
        {
            var pieceMock = new Mock<Piece>();

            this.chessboardMock = new Mock<Chessboard>();
            this.chessboardMock.Setup(m => m.GetPiece(IsAny<Position>())).Returns(pieceMock.Object);

            this.mountChessboardMock = new Mock<MountChessboardCommand>();

            this.game = new Game(this.chessboardMock.Object, this.mountChessboardMock.Object);
        }

        [Fact]
        public void StartDeveChamarChessboardExecuteUmaVez()
        {
            this.game.Start();

            this.mountChessboardMock.Verify(m => m.Execute(this.chessboardMock.Object), Once);
        }

        [Fact]
        public void MoveDadaPosicaoEPecaDeveChamarChessboardMovePieceUmaVez()
        {
            this.game.Move("a5", "a6");

            this.chessboardMock.Verify(m => m.MovePiece(IsAny<Piece>(), IsAny<Position>()), Once);
        }

        [Fact]
        public void MoveDadaPosicaoEPecaQueNaoExisteDeveLancarAExcecaoChessException()
        {
            this.chessboardMock.Setup(m => m.GetPiece(IsAny<Position>())).Returns(default(Piece));

            var actual = this.game.Move("a5", "a6");

            actual.IsFailure.Should().BeTrue();
        }
    }
}
