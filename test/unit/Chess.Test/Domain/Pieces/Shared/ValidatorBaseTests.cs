namespace Chess.Test.Domain.Pieces.Shared
{
    using System.Collections.Generic;

    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;

    using FluentAssertions;

    using static Chess.Domain.Shared.Position;

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
            var piece = this.chessboard.GetPiece(CreatePosition(position));

            var actual = this.validator.Validate(piece, CreatePosition(newPosition), this.chessboard);

            actual.Should().Be(expected);
        }
    }
}
