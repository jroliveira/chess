namespace Chess.Test
{
    using System;
    using System.Collections.Generic;

    using Chess.Exceptions;
    using Chess.Pieces;
    using Chess.Queries;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class ChessboardTests
    {
        private readonly Chessboard chessboard;
        private readonly Mock<Piece> pieceMock;
        private readonly Mock<Position> positionStub;
        private readonly Mock<ICollection<Piece>> piecesMock;

        public ChessboardTests()
        {
            this.positionStub = new Mock<Position>();
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(false);

            this.pieceMock = new Mock<Piece>();
            this.pieceMock.Setup(p => p.Position).Returns(this.positionStub.Object);
            this.pieceMock.Setup(m => m.CanMove(It.IsAny<Position>())).Returns(true);

            this.piecesMock = new Mock<ICollection<Piece>>();

            var getPiecesMock = new Mock<GetPiecesQuery>();
            getPiecesMock.Setup(m => m.GetResult(It.IsAny<ICollection<Piece>>())).Returns(new[] { this.pieceMock.Object });

            this.chessboard = new Chessboard(this.piecesMock.Object, getPiecesMock.Object);
        }

        [Fact]
        public void AddPiece_DadoUmaNovaPeca_DeveTerUmItemNaListaDePecas()
        {
            this.chessboard.AddPiece(It.IsAny<Piece>());

            this.piecesMock.Verify(m => m.Add(It.IsAny<Piece>()), Times.Once);
        }

        [Fact]
        public void AddPiece_DadoUmaNovaPeca_DeveTerUmItemNaListaDePeca()
        {
            this.chessboard.AddPiece(It.IsAny<Piece>());

            this.piecesMock.Verify(m => m.Add(It.IsAny<Piece>()), Times.Once);
        }

        [Fact]
        public void MovePiece_DadaPecaEPosicaoQuePodeSerMovida_DeveChamarPieceMove()
        {
            this.chessboard.MovePiece(this.pieceMock.Object, It.IsAny<Position>());

            this.pieceMock.Verify(m => m.Move(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void MovePiece_DadaPecaEPosicaoQueNaoPodeSerMovida_DeveLancarExcecaoChessException()
        {
            this.pieceMock.Setup(m => m.CanMove(It.IsAny<Position>())).Returns(false);

            Action action = () => this.chessboard.MovePiece(this.pieceMock.Object, It.IsAny<Position>());

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Não é possível mover a peça.");
        }

        [Fact]
        public void MovePiece_DadaPecaQueNaoPodeSerRemovida_DeveLancarExcecaoChessException()
        {
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            this.piecesMock.Setup(m => m.Remove(It.IsAny<Piece>())).Returns(false);

            Action action = () => this.chessboard.MovePiece(this.pieceMock.Object, It.IsAny<Position>());

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Não é possível remover a peça.");
        }

        [Fact]
        public void MovePiece_DadaPecaQueEstaNaPosicaoPassada_DeveRemoverAPecaDaCollecao()
        {
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            this.piecesMock.Setup(m => m.Remove(It.IsAny<Piece>())).Returns(true);

            this.chessboard.MovePiece(this.pieceMock.Object, It.IsAny<Position>());

            this.piecesMock.Verify(m => m.Remove(It.IsAny<Piece>()), Times.Once);
        }

        [Fact]
        public void HasPiece_DadaPosicaoQueEstaNasPecas_DeveRetornarTrue()
        {
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var actual = this.chessboard.HasPiece(this.positionStub.Object);

            actual.Should().BeTrue();
        }

        [Fact]
        public void HasPiece_DadaPosicaoQueNaoEstaNasPecas_DeveRetornarFalse()
        {
            var actual = this.chessboard.HasPiece(this.positionStub.Object);

            actual.Should().BeFalse();
        }

        [Fact]
        public void GetPiece_DadaPosicaoQueEstaNasPecas_DeveRetornarPiece()
        {
            this.positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var actual = this.chessboard.GetPiece(this.positionStub.Object);

            actual.ShouldBeEquivalentTo(this.pieceMock.Object);
        }

        [Fact]
        public void GetPiece_DadaPosicaoQueNaoEstaNasPecas_DeveRetornarNull()
        {
            var actual = this.chessboard.GetPiece(this.positionStub.Object);

            actual.Should().BeNull();
        }
    }
}
