namespace Chess.Test
{
    using System;

    using Chess.Commands;
    using Chess.Exceptions;
    using Chess.Pieces;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class GameTests
    {
        private Game game;
        private Mock<Piece> pieceMock;
        private Mock<Chessboard> chessboardMock;
        private Mock<MountChessboardCommand> mountChessboardMock;

        [SetUp]
        public void SetUp()
        {
            this.pieceMock = new Mock<Piece>();

            this.chessboardMock = new Mock<Chessboard>();
            this.chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(this.pieceMock.Object);

            this.mountChessboardMock = new Mock<MountChessboardCommand>();

            this.game = new Game(this.chessboardMock.Object, this.mountChessboardMock.Object);
        }

        [Test]
        public void Files_DadoUmNovoJogo_DeveRetornarAsColunasDoTabuleiro()
        {
            this.game = new Game();

            var expectation = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

            this.game.Files.ShouldAllBeEquivalentTo(expectation);
        }

        [Test]
        public void Ranks_DadoUmNovoJogo_DeveRetornarAsLinhasDoTabuleiro()
        {
            this.game = new Game();

            var expectation = new[] { '8', '7', '6', '5', '4', '3', '2', '1' };

            this.game.Ranks.ShouldAllBeEquivalentTo(expectation);
        }

        [Test]
        public void Start_DeveChamarChessboardExecuteUmaVez()
        {
            this.game.Start();

            this.mountChessboardMock.Verify(m => m.Execute(), Times.Once);
        }

        [Test]
        public void Move_DadaPosicaoEPeca_DeveChamarChessboardGetPieceUmaVez()
        {
            this.game.Move("a5", "a6");

            this.chessboardMock.Verify(m => m.GetPiece(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Move_DadaPosicaoEPeca_DeveChamarChessboardMovePieceUmaVez()
        {
            this.game.Move("a5", "a6");

            this.chessboardMock.Verify(m => m.MovePiece(It.IsAny<Piece>(), It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Move_DadaPosicaoEPecaQueNaoExiste_DeveLancarAExcecaoChessException()
        {
            this.chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(default(Piece));

            Action action = () => this.game.Move("a5", "a6");

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Peça não existe.");
        }

        [Test]
        public void GetPiece_DadaLinhaEColuna_DeveChamarChessboardGetPieceUmaVez()
        {
            this.game.GetPiece('a', '6');

            this.chessboardMock.Verify(m => m.GetPiece(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void GetPiece_DadaLinhaEColuna_DeveRetornarUmModeloDePeca()
        {
            var actual = this.game.GetPiece('a', '6');

            actual.Should().BeOfType<Models.Piece>().And.NotBeNull();
        }

        [Test]
        public void GetPiece_DadaLinhaEColunaQueNaoExiste_DeveRetornarNull()
        {
            this.chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(default(Piece));

            var actual = this.game.GetPiece('a', '6');

            actual.Should().BeNull();
        }
    }
}
