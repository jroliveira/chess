using System;
using Chess.Validations;

namespace Chess.Pieces
{
    internal abstract class Piece : IEquatable<Piece>
    {
        public virtual Chessboard Chessboard { get; protected set; }
        public virtual Position Position { get; private set; }
        public virtual int Player { get; set; }
        public string Name { get { return GetType().Name.Substring(0, 4); } }

        protected abstract IValidator Validator { get; }

        protected Piece()
        {
            
        }

        protected Piece(int player, Position position, Chessboard chessboard)
        {
            Player = player;
            Position = position;
            Chessboard = chessboard;
        }

        public virtual void Move(Position newPosition)
        {
            Position = newPosition;
        }

        public virtual bool CanMove(Position newPosition)
        {
            return Validator.Validate(newPosition);
        }

        public bool Equals(Piece other)
        {
            return Position.Equals(other.Position);
        }
    }
}