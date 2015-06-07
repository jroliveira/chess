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

        protected abstract IValidator Validator { get; }

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

        public bool CanMove(Position newPosition)
        {
            return Validator.Validate(newPosition);
        }

        public bool Equals(Piece other)
        {
            return Position.Equals(other.Position);
        }
    }

    internal class Pawn : Piece
    {
        protected override IValidator Validator { get { return new PawnValidator(this); } }

        public Pawn(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }
    }

    internal class King : Piece
    {
        protected override IValidator Validator { get { return new KingValidator(this); } }

        public King(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }
    }

    internal class Queen : Piece
    {
        protected override IValidator Validator { get { return new QueenValidator(this); } }

        public Queen(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }
    }

    internal class Rook : Piece
    {
        protected override IValidator Validator { get { return new RookValidator(this); } }

        public Rook(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }
    }

    internal class Bishop : Piece
    {
        protected override IValidator Validator { get { return new BishopValidator(this); } }

        public Bishop(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }
    }

    internal class Knight : Piece
    {
        protected override IValidator Validator { get { return new KnightValidator(this); } }

        public Knight(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        { }
    }
}