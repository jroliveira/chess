namespace Chess.Pieces
{
    using System;

    using Chess.Validations;

    internal abstract class Piece : IEquatable<Piece>
    {
        protected Piece(int player, Position position, Chessboard chessboard)
        {
            this.Player = player;
            this.Position = position;
            this.Chessboard = chessboard;
        }

        protected Piece()
        {
        }

        public virtual Chessboard Chessboard { get; protected set; }

        public virtual Position Position { get; private set; }

        public virtual int Player { get; set; }

        public abstract string Name { get; }

        protected abstract IValidator Validator { get; }

        public virtual void Move(Position newPosition)
        {
            this.Position = newPosition;
        }

        public virtual bool CanMove(Position newPosition)
        {
            return this.Validator.Validate(newPosition);
        }

        public bool Equals(Piece other)
        {
            return this.Position.Equals(other.Position);
        }
    }
}