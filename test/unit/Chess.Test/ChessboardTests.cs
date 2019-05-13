namespace Chess.Test
{
    using System.Collections.ObjectModel;

    using Chess.Entities;
    using Chess.Entities.Pieces;

    using FluentAssertions;

    using Moq;

    using Xunit;

    using static Moq.It;

    public class ChessboardTests
    {
        private readonly Chessboard chessboard;
        private readonly Mock<Piece> pieceMock;
        private readonly Mock<Position> positionStub;

        public ChessboardTests()
        {
            this.positionStub = new Mock<Position>();

            this.pieceMock = new Mock<Piece>();
            this.pieceMock.Setup(p => p.Position).Returns(this.positionStub.Object);
            this.pieceMock.Setup(m => m.CanMove(IsAny<Position>())).Returns(true);
            this.pieceMock.Setup(m => m.Equals(IsAny<Position>())).Returns(false);

            var piecesFake = new ObservableCollection<Piece> { this.pieceMock.Object };

            this.chessboard = new Chessboard(piecesFake);
        }

        [Fact]
        public void MovePieceDadaPecaEPosicaoQueNaoPodeSerMovidaDeveLancarExcecaoChessException()
        {
            this.pieceMock.Setup(m => m.CanMove(IsAny<Position>())).Returns(false);

            var actual = this.chessboard.MovePiece(this.pieceMock.Object, IsAny<Position>());

            actual.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void MovePieceDadaPecaQueNaoPodeSerRemovidaDeveLancarExcecaoChessException()
        {
            this.pieceMock.Setup(m => m.Equals(IsAny<Position>())).Returns(true);

            var actual = this.chessboard.MovePiece(this.pieceMock.Object, IsAny<Position>());

            actual.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Random()
        {
            var chessboard = new Chessboard();
            var piece = new Pawn(new Position('a', '7'), chessboard);
            chessboard.AddPiece(piece);

            chessboard.MovePiece(piece, new Position('a', '6'));
        }

        [Fact]
        public void HasPieceDadaPosicaoQueEstaNasPecasDeveRetornarTrue()
        {
            this.pieceMock.Setup(m => m.Equals(IsAny<Position>())).Returns(true);

            var actual = this.chessboard.HasPiece(this.positionStub.Object);

            actual.Should().BeTrue();
        }

        [Fact]
        public void HasPieceDadaPosicaoQueNaoEstaNasPecasDeveRetornarFalse()
        {
            var actual = this.chessboard.HasPiece(this.positionStub.Object);

            actual.Should().BeFalse();
        }

        [Fact]
        public void GetPieceDadaPosicaoQueEstaNasPecasDeveRetornarPiece()
        {
            this.pieceMock.Setup(m => m.Equals(IsAny<Position>())).Returns(true);

            var actual = this.chessboard.GetPiece(this.positionStub.Object);

            actual.Get().Should().BeEquivalentTo(this.pieceMock.Object);
        }

        [Fact]
        public void GetPieceDadaPosicaoQueNaoEstaNasPecasDeveRetornarNull()
        {
            var actual = this.chessboard.GetPiece(this.positionStub.Object);

            actual.IsDefined.Should().BeFalse();
        }
    }
}
