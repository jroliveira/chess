namespace Chess.Entities.Pieces
{
    using System;
    using Chess.Lib.Validations;

    internal abstract class Piece : IEquatable<Piece>
    {
        protected Piece(Models.Owner owner, Position position, Chessboard chessboard)
        {
            this.Owner = owner;
            this.Position = position;
            this.Chessboard = chessboard;
        }

        protected Piece()
        {
        }

        public virtual Chessboard Chessboard { get; }

        public virtual Position Position { get; }

        public virtual Models.Owner Owner { get; }

        public virtual string Name => this.Owner == Models.Owner.FirstPlayer ? this.Symbols.FirstPlayer : this.Symbols.SecondPlayer;

        protected abstract IValidator Validator { get; }

        protected abstract (string FirstPlayer, string SecondPlayer) Symbols { get; }

        public static Piece Clone(Piece piece, Position newPosition) => Activator.CreateInstance(piece.GetType(), piece.Owner, newPosition, piece.Chessboard) as Piece;

        public virtual bool CanMove(Position newPosition) => this.Validator.Validate(newPosition);

        public virtual bool Equals(Piece other) => this.Equals(other.Position);

        public virtual bool Equals(Position position) => this.Position.Equals(position);

        public override string ToString() => $"{this.Position.File}{this.Position.Rank}";
    }
}