namespace Chess.Test
{
    using System.Collections.Generic;

    using Chess.Entities;
    using Chess.Lib.Data.Commands;
    using Chess.Models;

    using FluentAssertions;

    using Moq;

    using Xunit;

    using static Moq.It;
    using static Moq.Times;

    using Chessboard = Chess.Entities.Chessboard;
    using Piece = Chess.Entities.Pieces.Piece;

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

            this.game = new Game(
                this.chessboardMock.Object,
                this.mountChessboardMock.Object,
                new Dictionary<string, Models.Player>
                {
                    { "aj", new Models.Player("aj", true) },
                    { "jr", new Models.Player("jr", false) },
                });
        }

        [Fact]
        public void StartDeveChamarChessboardExecuteUmaVez()
        {
            this.game.Start();

            this.mountChessboardMock.Verify(m => m.Execute(this.chessboardMock.Object), Once);
        }

        [Fact]
        public void MovePieceDadaPosicaoEPecaDeveChamarChessboardMovePieceUmaVez()
        {
            this.game.MovePiece("a5", "a6", "jr");

            this.chessboardMock.Verify(m => m.MovePiece(IsAny<Piece>(), IsAny<Position>()), Once);
        }

        [Fact]
        public void MovePieceDadaPosicaoEPecaQueNaoExisteDeveLancarAExcecaoChessException()
        {
            this.chessboardMock.Setup(m => m.GetPiece(IsAny<Position>())).Returns(default(Piece));

            var actual = this.game.MovePiece("a5", "a6", "jr");

            actual.IsFailure.Should().BeTrue();
        }
    }
}
