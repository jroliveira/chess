using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Chess.Exceptions;
using Chess.Pieces;
using Chess.Queries;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test
{
    [TestFixture]
    public class ChessboardTests
    {
        private Chessboard _chessboard;
        private Mock<Piece> _pieceMock;
        private Mock<Position> _positionStub;
        private Mock<ICollection<Piece>> _piecesMock;
        private Mock<GetPiecesQuery> _getPiecesMock;

        [SetUp]
        public void SetUp()
        {
            _positionStub = new Mock<Position>();
            _positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(false);

            _pieceMock = new Mock<Piece>();
            _pieceMock.Setup(p => p.Position).Returns(_positionStub.Object);
            _pieceMock.Setup(m => m.CanMove(It.IsAny<Position>())).Returns(true);

            _piecesMock = new Mock<ICollection<Piece>>();

            _getPiecesMock = new Mock<GetPiecesQuery>();
            _getPiecesMock.Setup(m => m.GetResult(It.IsAny<ICollection<Piece>>())).Returns(new[] { _pieceMock.Object });

            _chessboard = new Chessboard(_piecesMock.Object, _getPiecesMock.Object);
        }

        [Test]
        public void AddPiece_DadoUmaNovaPeca_DeveTerUmItemNaListaDePecas()
        {
            _chessboard.AddPiece(It.IsAny<Piece>());

            _piecesMock.Verify(m => m.Add(It.IsAny<Piece>()), Times.Once);
        }

        [Test]
        public void MovePiece_DadaPecaEPosicaoQuePodeSerMovida_DeveChamarPieceMove()
        {
            _chessboard.MovePiece(_pieceMock.Object, It.IsAny<Position>());

            _pieceMock.Verify(m => m.Move(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void MovePiece_DadaPecaEPosicaoQueNaoPodeSerMovida_DeveLancarExcecaoChessException()
        {
            _pieceMock.Setup(m => m.CanMove(It.IsAny<Position>())).Returns(false);

            Action action = () => _chessboard.MovePiece(_pieceMock.Object, It.IsAny<Position>());

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Não é possível mover a peça");
        }

        [Test]
        public void MovePiece_DadaPecaQueEstaNaPosicaoPassada_DeveRemoverAPecaDaCollecao()
        {
            _positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            _piecesMock.Setup(m => m.Remove(It.IsAny<Piece>())).Returns(true);

            _chessboard.MovePiece(_pieceMock.Object, It.IsAny<Position>());

            _piecesMock.Verify(m => m.Remove(It.IsAny<Piece>()), Times.Once);
        }

        [Test]
        public void HasPiece_DadaPosicaoQueEstaNasPecas_DeveRetornarTrue()
        {
            _positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var actual = _chessboard.HasPiece(_positionStub.Object);

            actual.Should().BeTrue();
        }

        [Test]
        public void HasPiece_DadaPosicaoQueNaoEstaNasPecas_DeveRetornarFalse()
        {
            var actual = _chessboard.HasPiece(_positionStub.Object);

            actual.Should().BeFalse();
        }

        [Test]
        public void GetPiece_DadaPosicaoQueEstaNasPecas_DeveRetornarPiece()
        {
            _positionStub.Setup(m => m.Equals(It.IsAny<Position>())).Returns(true);

            var actual = _chessboard.GetPiece(_positionStub.Object);

            actual.ShouldBeEquivalentTo(_pieceMock.Object);
        }

        [Test]
        public void GetPiece_DadaPosicaoQueNaoEstaNasPecas_DeveRetornarNull()
        {
            var actual = _chessboard.GetPiece(_positionStub.Object);

            actual.Should().BeNull();
        }
    }
}
