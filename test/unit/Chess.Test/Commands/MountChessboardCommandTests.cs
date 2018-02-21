namespace Chess.Test.Commands
{
    using System;
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Data.Commands;
    using Moq;
    using Xunit;

    public class MountChessboardCommandTests
    {
        private readonly MountChessboardCommand mountChessboard;
        private readonly Mock<Chessboard> chessboardMock;

        public MountChessboardCommandTests()
        {
            this.chessboardMock = new Mock<Chessboard>();

            this.mountChessboard = new MountChessboardCommand();
        }

        [Theory]
        [InlineData(typeof(King), 2)]
        [InlineData(typeof(Queen), 2)]
        [InlineData(typeof(Bishop), 4)]
        [InlineData(typeof(Knight), 4)]
        [InlineData(typeof(Rook), 4)]
        [InlineData(typeof(Pawn), 16)]
        public void ExecuteDadoTabuleiroVazioDeveChamarChessboardAddPieceParaOTipoEaQuantidade(Type type, int times)
        {
            this.mountChessboard.Execute(this.chessboardMock.Object);

            this.chessboardMock.Verify(m => m.AddPiece(It.Is<Piece>(p => p.GetType() == type)), Times.Exactly(times));
        }

        [Theory]
        [InlineData('a', '1', typeof(Rook))]
        [InlineData('b', '1', typeof(Knight))]
        [InlineData('c', '1', typeof(Bishop))]
        [InlineData('d', '1', typeof(King))]
        [InlineData('e', '1', typeof(Queen))]
        [InlineData('f', '1', typeof(Bishop))]
        [InlineData('g', '1', typeof(Knight))]
        [InlineData('h', '1', typeof(Rook))]
        [InlineData('a', '2', typeof(Pawn))]
        [InlineData('b', '2', typeof(Pawn))]
        [InlineData('c', '2', typeof(Pawn))]
        [InlineData('d', '2', typeof(Pawn))]
        [InlineData('e', '2', typeof(Pawn))]
        [InlineData('f', '2', typeof(Pawn))]
        [InlineData('g', '2', typeof(Pawn))]
        [InlineData('h', '2', typeof(Pawn))]
        [InlineData('a', '7', typeof(Pawn))]
        [InlineData('b', '7', typeof(Pawn))]
        [InlineData('c', '7', typeof(Pawn))]
        [InlineData('d', '7', typeof(Pawn))]
        [InlineData('e', '7', typeof(Pawn))]
        [InlineData('f', '7', typeof(Pawn))]
        [InlineData('g', '7', typeof(Pawn))]
        [InlineData('h', '7', typeof(Pawn))]
        [InlineData('a', '8', typeof(Rook))]
        [InlineData('b', '8', typeof(Knight))]
        [InlineData('c', '8', typeof(Bishop))]
        [InlineData('d', '8', typeof(King))]
        [InlineData('e', '8', typeof(Queen))]
        [InlineData('f', '8', typeof(Bishop))]
        [InlineData('g', '8', typeof(Knight))]
        [InlineData('h', '8', typeof(Rook))]
        public void ExecuteDadoTabuleiroVazioDeveChamarChessboardAddPieceParaAPosicaoEoTipoUmaVez(char file, char rank, Type type)
        {
            this.mountChessboard.Execute(this.chessboardMock.Object);

            this.chessboardMock.Verify(m => m.AddPiece(It.Is<Piece>(p => p.Position.File == file && p.Position.Rank == rank && p.GetType() == type)), Times.Once);
        }

        [Fact]
        public void ExecuteDadoTabuleiroVazioDeveChamarChessboardAddPiece32Vezes()
        {
            this.mountChessboard.Execute(this.chessboardMock.Object);

            this.chessboardMock.Verify(m => m.AddPiece(It.IsAny<Piece>()), Times.Exactly(32));
        }
    }
}
