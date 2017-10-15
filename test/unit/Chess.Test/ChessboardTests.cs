namespace Chess.Test
{
    using System;
    using System.Collections.ObjectModel;

    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Exceptions;

    using FluentAssertions;

    using Moq;

    using Xunit;

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
            this.pieceMock.Setup(m => m.CanMove(It.IsAny<Position>())).Returns(true);
            this.pieceMock.Setup(m => m.Equals(It.IsAny<Position>())).Returns(false);

            var piecesFake = new ObservableCollection<Piece> { this.pieceMock.Object };

            this.chessboard = new Chessboard(piecesFake);
        }

        [Fact]
        public void MovePieceDadaPecaEPosicaoQueNaoPodeSerMovidaDeveLancarExcecaoChessException()
        {
            this.pieceMock.Setup(m => m.CanMove(It.IsAny<Position>())).Returns(false);

            Action action = () => this.chessboard.MovePiece(this.pieceMock.Object, It.IsAny<Position>());

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Cannot move the piece ''.");
        }

        [Fact]
        public void MovePieceDadaPecaQueNaoPodeSerRemovidaDeveLancarExcecaoChessException()
        {
            this.pieceMock.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            Action action = () => this.chessboard.MovePiece(this.pieceMock.Object, It.IsAny<Position>());

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Cannot move the piece ''.");
        }

        [Fact]
        public void HasPieceDadaPosicaoQueEstaNasPecasDeveRetornarTrue()
        {
            this.pieceMock.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

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
            this.pieceMock.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var actual = this.chessboard.GetPiece(this.positionStub.Object);

            actual.ShouldBeEquivalentTo(this.pieceMock.Object);
        }

        [Fact]
        public void GetPieceDadaPosicaoQueNaoEstaNasPecasDeveRetornarNull()
        {
            var actual = this.chessboard.GetPiece(this.positionStub.Object);

            actual.Should().BeNull();
        }
    }
}
