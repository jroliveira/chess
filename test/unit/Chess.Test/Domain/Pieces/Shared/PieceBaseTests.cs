namespace Chess.Test.Domain.Pieces.Shared
{
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;

    using FluentAssertions;

    using Xunit;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.PieceColor;

    public abstract class PieceBaseTests
    {
        [Theory]
        [InlineData(new string[] { }, true)]
        [InlineData(new[] { "a2" }, false)]
        [InlineData(new[] { "a2", "a3" }, false)]
        public void IsFirstMove_GivenPieceBase_ShouldReturn(string[] logMoves, bool expected)
        {
            var piece = new FakePiece(
                "a2",
                pieceColor: WhitePiece,
                logMoves.Select(item => (Position)item).ToList());

            piece.IsFirstMove.Should().Be(expected);
        }

        [Theory]
        [InlineData(WhitePiece)]
        [InlineData(BlackPiece)]
        public void PieceColor_GivenPlayer_ShouldReturn(PieceColor pieceColor)
        {
            var piece = new FakePiece("a2", pieceColor);

            piece.Color.Should().Be(pieceColor);
        }

        public abstract void ImplicitOperatorChar_GivenPieceColor_ShouldReturn(PieceColor pieceColor, char expected);

        public abstract void ImplicitOperatorPiece_GivenPieceColor_ShouldReturn(PieceColor pieceColor, char symbol);

        public abstract void ToString_GivenPieceBase_ShouldReturn(string position, PieceColor pieceColor, char symbol);

        [Theory]
        [InlineData("d6", BlackPiece, false)]
        [InlineData("d5", WhitePiece, true)]
        [InlineData("d5", BlackPiece, true)]
        [InlineData("a5", WhitePiece, false)]
        public void Equals_GivenOtherPieceBase_ShouldReturn(string position, PieceColor pieceColor, bool expected)
        {
            var piece = new FakePiece("d5");

            var actual = piece.Equals(new FakePiece(position, pieceColor));

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("d6", false)]
        [InlineData("d5", true)]
        [InlineData("a5", false)]
        public void Equals_GivenOtherPosition_ShouldReturn(string position, bool expected)
        {
            var piece = new FakePiece("d5");

            var actual = piece.Equals(position);

            actual.Should().Be(expected);
        }

        internal void ImplicitOperatorChar(PieceBase piece, char expected)
        {
            char actual = piece;

            actual.Should().Be(expected);
        }

        internal void ImplicitOperatorPiece(PieceBase piece, char symbol)
        {
            Piece expected = symbol;
            Piece actual = piece;

            actual.ToString().Should().Be(expected.ToString());
        }

        internal void ToString(PieceBase piece, string position, char symbol)
        {
            var expected = $"{symbol}{position}";
            var actual = piece.ToString();

            actual.Should().Be(expected);
        }

        private sealed class FakePiece : PieceBase
        {
            internal FakePiece(Position position)
                : base(position, default, new List<Position>(), ('○', '●'), new FakeValidator())
            {
            }

            internal FakePiece(
                Position position,
                PieceColor pieceColor)
                : base(position, pieceColor, new List<Position>(), ('○', '●'), new FakeValidator())
            {
            }

            internal FakePiece(
                Position position,
                PieceColor pieceColor,
                IReadOnlyCollection<Position> logMoves)
                : base(position, pieceColor, logMoves, ('○', '●'), new FakeValidator())
            {
            }
        }

        private sealed class FakeValidator : ValidatorBase
        {
            public FakeValidator()
                : base(new FakeValidate())
            {
            }

            private sealed class FakeValidate : ValidateBase
            {
                public FakeValidate()
                    : base(None())
                {
                }

                protected override bool IsValidRule(PieceBase piece, Position newPosition, Chessboard chessboard) => false;
            }
        }
    }
}
