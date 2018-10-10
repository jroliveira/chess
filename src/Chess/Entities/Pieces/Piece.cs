namespace Chess.Entities.Pieces
{
    using System;

    using Chess.Lib.Validations;

    using static System.Activator;

    internal abstract class Piece : IEquatable<Piece>
    {
        private readonly string whiteSymbol;
        private readonly string blackSymbol;

        protected Piece(
            Position position,
            Chessboard chessboard,
            string whiteSymbol,
            string blackSymbol)
        {
            this.whiteSymbol = whiteSymbol;
            this.blackSymbol = blackSymbol;
            this.Position = position;
            this.Chessboard = chessboard;

            this.IsWhite = position.Rank < 5;
        }

        protected Piece()
        {
        }

        internal virtual Chessboard Chessboard { get; }

        internal virtual Position Position { get; }

        internal virtual bool IsWhite { get; }

        protected abstract IValidator Validator { get; }

        public static implicit operator string(Piece piece) => piece.IsWhite
            ? piece.whiteSymbol
            : piece.blackSymbol;

        public override string ToString() => $"{this.Position.File}{this.Position.Rank}";

        public virtual bool Equals(Piece other) => this.Equals(other.Position);

        internal virtual Piece Move(Position newPosition) => CreateInstance(this.GetType(), newPosition, this.Chessboard) as Piece;

        internal virtual bool CanMove(Position newPosition) => this.Validator.Validate(newPosition);

        internal virtual bool Equals(Position position) => this.Position.Equals(position);
    }
}
