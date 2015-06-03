using System;
using Chess.Game.Validations;

namespace Chess.Game.Pieces
{
    internal abstract class Piece : IEquatable<Piece>
    {
        public Chessboard Chessboard { get; protected set; }
        public Position Position { get; private set; }
        public int Player { get; set; }
        public string Name { get { return GetType().Name.Substring(0, 4); } }

        protected Piece(int player, Position position, Chessboard chessboard)
        {
            Player = player;
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

    internal class Pawn : Piece
    {
        private readonly PawnValidator _pawnValidator;

        public Pawn(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            _pawnValidator = new PawnValidator(this);
        }

        public override bool CanMove(Position newPosition)
        {
            return _pawnValidator.Validate(newPosition);
        }
    }

    internal class King : Piece
    {
        public King(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }

    internal class Queen : Piece
    {
        public Queen(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }

    internal class Rook : Piece
    {
        public Rook(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }

    internal class Bishop : Piece
    {
        public Bishop(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }

    internal class Knight : Piece
    {
        public Knight(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }

        public override bool CanMove(Position position)
        {
            throw new NotImplementedException();
        }
    }
}