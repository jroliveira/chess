using Chess.Pieces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Test
{
    [TestFixture]
    public class ChessboardTests
    {
        private Chessboard _chessboard;

        [SetUp]
        public void SetUp()
        {
            _chessboard = new Chessboard();
        }

        [Test]
        public void AddPiece_DadoUmaNovaPeca_DeveTerUmItemNaListaDePecas()
        {
            var positionStub = new Mock<Position>();
            positionStub.Setup(p => p.File).Returns('c');
            positionStub.Setup(p => p.Rank).Returns('3');

            var newPieceStub = new Mock<Piece>();
            newPieceStub.Setup(p => p.Position).Returns(positionStub.Object);

            _chessboard.AddPiece(newPieceStub.Object);

            _chessboard.Pieces.Count.Should().Be(1);
        }
    }
}
