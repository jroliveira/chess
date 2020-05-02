namespace Chess.Test.Domain.Pieces.Shared
{
    using System.Collections.Generic;

    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;

    using FluentAssertions;

    public abstract class ValidatorBaseTests
    {
        private readonly ValidatorBase validator;
        private readonly Chessboard chessboard;

        internal ValidatorBaseTests(ValidatorBase validator, IReadOnlyCollection<PieceBase> pieces)
        {
            this.validator = validator;
            this.chessboard = new List<PieceBase>(pieces);
        }

        protected virtual void Validate(string position, string newPosition, bool expected)
        {
            var piece = this.chessboard.GetPiece(position).Get();

            var actual = this.validator.Validate(piece, newPosition, this.chessboard);

            actual.Should().Be(expected);
        }
    }
}
