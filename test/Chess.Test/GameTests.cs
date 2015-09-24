using System;
using Chess.Commands;
using Chess.Exceptions;
using Chess.Pieces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test
{
    [TestFixture]
    public class GameTests
    {
        private Game _game;
        private Mock<Piece> _pieceMock;
        private Mock<Chessboard> _chessboardMock;
        private Mock<MountChessboardCommand> _mountChessboardMock;

        [SetUp]
        public void SetUp()
        {
            _pieceMock = new Mock<Piece>();

            _chessboardMock = new Mock<Chessboard>();
            _chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(_pieceMock.Object);

            _mountChessboardMock = new Mock<MountChessboardCommand>();

            _game = new Game(_chessboardMock.Object, _mountChessboardMock.Object);
        }

        [Test]
        public void Start_DeveChamarChessboardExecuteUmaVez()
        {
            _game.Start();

            _mountChessboardMock.Verify(m => m.Execute(), Times.Once);
        }

        [Test]
        public void Move_DadaPosicaoEPeca_DeveChamarChessboardGetPieceUmaVez()
        {
            _game.Move("a5", "a6");

            _chessboardMock.Verify(m => m.GetPiece(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Move_DadaPosicaoEPeca_DeveChamarChessboardMovePieceUmaVez()
        {
            _game.Move("a5", "a6");

            _chessboardMock.Verify(m => m.MovePiece(It.IsAny<Piece>(), It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Move_DadaPosicaoEPecaQueNaoExiste_DeveLancarAExcecaoChessException()
        {
            _chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(default(Piece));

            Action action = () => _game.Move("a5", "a6");

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Peça não existe.");
        }

        [Test]
        public void GetPiece_DadaLinhaEColuna_DeveChamarChessboardGetPieceUmaVez()
        {
            _game.GetPiece('a', '6');

            _chessboardMock.Verify(m => m.GetPiece(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void GetPiece_DadaLinhaEColuna_DeveRetornarUmModeloDePeca()
        {
            var actual = _game.GetPiece('a', '6');

            actual.Should().BeOfType<Models.Piece>().And.NotBeNull();
        }

        [Test]
        public void GetPiece_DadaLinhaEColunaQueNaoExiste_DeveRetornarNull()
        {
            _chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(default(Piece));

            var actual = _game.GetPiece('a', '6');

            actual.Should().BeNull();
        }
    }
}
