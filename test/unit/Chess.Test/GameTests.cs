namespace Chess.Test
{
    using System;

    using Chess.Commands;
    using Chess.Exceptions;
    using Chess.Pieces;

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
        public void Files_DadoUmNovoJogo_DeveRetornarAsColunasDoTabuleiro()
        {
            this.game = new Game();

            var expectation = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

            this.game.Files.ShouldAllBeEquivalentTo(expectation);
        }

        [Fact]
        public void Ranks_DadoUmNovoJogo_DeveRetornarAsLinhasDoTabuleiro()
        {
            this.game = new Game();

            var expectation = new[] { '8', '7', '6', '5', '4', '3', '2', '1' };

            this.game.Ranks.ShouldAllBeEquivalentTo(expectation);
        }

        [Fact]
        public void Start_DeveChamarChessboardExecuteUmaVez()
        {
            this.game.Start();

            this.mountChessboardMock.Verify(m => m.Execute(), Times.Once);
        }

        [Fact]
        public void Move_DadaPosicaoEPeca_DeveChamarChessboardGetPieceUmaVez()
        {
            this.game.Move("a5", "a6");

            this.chessboardMock.Verify(m => m.GetPiece(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void Move_DadaPosicaoEPeca_DeveChamarChessboardMovePieceUmaVez()
        {
            this.game.Move("a5", "a6");

            this.chessboardMock.Verify(m => m.MovePiece(It.IsAny<Piece>(), It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void Move_DadaPosicaoEPecaQueNaoExiste_DeveLancarAExcecaoChessException()
        {
            this.chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(default(Piece));

            Action action = () => this.game.Move("a5", "a6");

            action
                .ShouldThrow<ChessException>()
                .WithMessage("Peça não existe.");
        }

        [Fact]
        public void GetPiece_DadaLinhaEColuna_DeveChamarChessboardGetPieceUmaVez()
        {
            this.game.GetPiece('a', '6');

            this.chessboardMock.Verify(m => m.GetPiece(It.IsAny<Position>()), Times.Once);
        }

        [Fact]
        public void GetPiece_DadaLinhaEColuna_DeveRetornarUmModeloDePeca()
        {
            var actual = this.game.GetPiece('a', '6');

            actual.Should().BeOfType<Models.Piece>().And.NotBeNull();
        }

        [Fact]
        public void GetPiece_DadaLinhaEColunaQueNaoExiste_DeveRetornarNull()
        {
            this.chessboardMock.Setup(m => m.GetPiece(It.IsAny<Position>())).Returns(default(Piece));

            var actual = this.game.GetPiece('a', '6');

            actual.Should().BeNull();
        }
    }
}
