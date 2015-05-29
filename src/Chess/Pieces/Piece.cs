using System;
using Chess.Validations;

namespace Chess.Pieces
{
    public abstract class Piece : IEquatable<Piece>
    {
        public Chessboard Chessboard { get; protected set; }
        public Position Position { get; private set; }

        protected Piece(Position position, Chessboard chessboard)
        {
            Position = position;
            Chessboard = chessboard;
        }

        public void Move(Position newPosition)
        {
            Position = newPosition;
        }

        public abstract bool CanMove(Position position);

        public bool Equals(Piece other)
        {
            return Position.Equals(other.Position);
        }
    }

    public class Pawn : Piece
    {
        private readonly PawnValidator _pawnValidator;

        public Pawn(Position position, Chessboard chessboard)
            : base(position, chessboard)
        {
            _pawnValidator = new PawnValidator(this);
        }

        public override bool CanMove(Position newPosition)
        {
            return _pawnValidator.Validate(newPosition);
        }
    }

    public class King : Piece
    {
        public King(Position position, Chessboard chessboard)
            : base(position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }

    public class Queen : Piece
    {
        public Queen(Position position, Chessboard chessboard)
            : base(position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }

    public class Rook : Piece
    {
        public Rook(Position position, Chessboard chessboard)
            : base(position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }

    public class Bishop : Piece
    {
        public Bishop(Position position, Chessboard chessboard)
            : base(position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }

    public class Knight : Piece
    {
        public Knight(Position position, Chessboard chessboard)
            : base(position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }
}