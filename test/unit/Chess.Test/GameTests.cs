namespace Chess.Test
{
    using System;

    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Data.Commands;
    using Chess.Lib.Exceptions;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class GameTests
    {
        private readonly Mock<Chessboard> chessboardMock;
        private readonly Mock<MountChessboardCommand> mountChessboardMock;
        private Game game;

        public GameTests()
        {
            var pieceMock = new Mock<Piece>();

            this.chessboardMock = new Mock<Chessboard>();
            this.chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(pieceMock.Object);

            this.mountChessboardMock = new Mock<MountChessboardCommand>();

            this.game = new Game(this.chessboardMock.Object, this.mountChessboardMock.Object);
        }

        [Fact]
        public void FilesDadoUmNovoJogoDeveRetornarAsColunasDoTabuleiro()
        {
            this.game = new Game();

            var expectation = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

            this.game.Files.ShouldAllBeEquivalentTo(expectation);
        }

        [Fact]
        public void RanksDadoUmNovoJogoDeveRetornarAsLinhasDoTabuleiro()
        {
            this.game = new Game();

            var expectation = new[] { '8', '7', '6', '5', '4', '3', '2', '1' };

            this.game.Ranks.ShouldAllBeEquivalentTo(expectation);
        }

        [Fact]
        public void StartDeveChamarChessboardExecuteUmaVez()
        {
            this.game.Start();

            this.mountChessboardMock.Verify(m => m.Execute(this.chessboardMock.Object), Times.Once);
        }

        [Fact]
        public void MoveDadaPosicaoEPecaDeveChamarChessboardGetPieceUmaVez()
        {
            this.game.Move("a5", "a6");

            this.chessboardMock.Verify(m => m.GetPiece(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void MoveDadaPosicaoEPecaDeveChamarChessboardMovePieceUmaVez()
        {
            this.game.Move("a5", "a6");

            this.chessboardMock.Verify(m => m.MovePiece(It.IsAny<Piece>(), It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void MoveDadaPosicaoEPecaQueNaoExisteDeveLancarAExcecaoChessException()
        {
            this.chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(default(Piece));

            Action action = () => this.game.Move("a5", "a6");

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Piece 'a5' don't exist.");
        }

        [Fact]
        public void GetPieceDadaLinhaEColunaDeveChamarChessboardGetPieceUmaVez()
        {
            this.game.GetPiece('a', '6');

            this.chessboardMock.Verify(m => m.GetPiece(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void GetPieceDadaLinhaEColunaDeveRetornarUmModeloDePeca()
        {
            var actual = this.game.GetPiece('a', '6');

            actual.Should().BeOfType<Models.Piece>().And.NotBeNull();
        }

        [Fact]
        public void GetPieceDadaLinhaEColunaQueNaoExisteDeveRetornarNull()
        {
            this.chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(default(Piece));

            var actual = this.game.GetPiece('a', '6');

            actual.Should().BeNull();
        }
    }
}
