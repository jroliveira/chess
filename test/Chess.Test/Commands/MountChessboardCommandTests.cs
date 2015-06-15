using System;
using Chess.Commands;
using Chess.Pieces;
using Moq;
using NUnit.Framework;

namespace Chess.Test.Commands
{
    [TestFixture]
    public class MountChessboardCommandTests
    {
        private MountChessboardCommand _mountChessboard;
        private Mock<Chessboard> _chessboardMock;

        [SetUp]
        public void SetUp()
        {
            _chessboardMock = new Mock<Chessboard>();

            _mountChessboard = new MountChessboardCommand(_chessboardMock.Object);
        }

        [TestCase(typeof(King), 2)]
        [TestCase(typeof(Queen), 2)]
        [TestCase(typeof(Bishop), 4)]
        [TestCase(typeof(Knight), 4)]
        [TestCase(typeof(Rook), 4)]
        [TestCase(typeof(Pawn), 16)]
        public void Execute_DadoTabuleiroVazio_DeveChamarChessboardAddPieceParaOTipoEAQuantidade(Type type, int times)
        {
            _mountChessboard.Execute();

            _chessboardMock.Verify(m => m.AddPiece(It.Is<Piece>(p => p.GetType() == type)), Times.Exactly(times));
        }

        [TestCase('a', '1', typeof(Rook))]
        [TestCase('b', '1', typeof(Knight))]
        [TestCase('c', '1', typeof(Bishop))]
        [TestCase('d', '1', typeof(King))]
        [TestCase('e', '1', typeof(Queen))]
        [TestCase('f', '1', typeof(Bishop))]
        [TestCase('g', '1', typeof(Knight))]
        [TestCase('h', '1', typeof(Rook))]
        [TestCase('a', '2', typeof(Pawn))]
        [TestCase('b', '2', typeof(Pawn))]
        [TestCase('c', '2', typeof(Pawn))]
        [TestCase('d', '2', typeof(Pawn))]
        [TestCase('e', '2', typeof(Pawn))]
        [TestCase('f', '2', typeof(Pawn))]
        [TestCase('g', '2', typeof(Pawn))]
        [TestCase('h', '2', typeof(Pawn))]
        [TestCase('a', '7', typeof(Pawn))]
        [TestCase('b', '7', typeof(Pawn))]
        [TestCase('c', '7', typeof(Pawn))]
        [TestCase('d', '7', typeof(Pawn))]
        [TestCase('e', '7', typeof(Pawn))]
        [TestCase('f', '7', typeof(Pawn))]
        [TestCase('g', '7', typeof(Pawn))]
        [TestCase('h', '7', typeof(Pawn))]
        [TestCase('a', '8', typeof(Rook))]
        [TestCase('b', '8', typeof(Knight))]
        [TestCase('c', '8', typeof(Bishop))]
        [TestCase('d', '8', typeof(King))]
        [TestCase('e', '8', typeof(Queen))]
        [TestCase('f', '8', typeof(Bishop))]
        [TestCase('g', '8', typeof(Knight))]
        [TestCase('h', '8', typeof(Rook))]
        public void Execute_DadoTabuleiroVazio_DeveChamarChessboardAddPieceParaAPosicaoEOTipoUmaVez(char file, char rank, Type type)
        {
            _mountChessboard.Execute();

            _chessboardMock.Verify(m => m.AddPiece(It.Is<Piece>(p => p.Position.File == file && p.Position.Rank == rank && p.GetType() == type)), Times.Once);
        }

        [Test]
        public void Execute_DadoTabuleiroVazio_DeveChamarChessboardAddPiece32Vezes()
        {
            _mountChessboard.Execute();

            _chessboardMock.Verify(m => m.AddPiece(It.IsAny<Piece>()), Times.Exactly(32));
        }
    }
}
