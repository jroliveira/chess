using System.Collections.Generic;
using System.Linq;
using Chess.Exceptions;

namespace Chess
{
    internal class Chessboard
    {
        private readonly ICollection<Pieces.Piece> _pieces;

        public char[] Files { get { return new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }; } }
        public char[] Ranks { get { return new[] { '8', '7', '6', '5', '4', '3', '2', '1' }; } }

        public ICollection<Pieces.Piece> Pieces
        {
            get
            {
                return _pieces.OrderBy(piece => piece.Position.Rank)
                              .ThenBy(piece => piece.Position.File)
                              .ToList();
            }
        }

        public Chessboard()
        {
            _pieces = new List<Pieces.Piece>();
        }

        public void AddPiece(Pieces.Piece piece)
        {
            _pieces.Add(piece);
        }

        public void MovePiece(Pieces.Piece piece, Position position)
        {
            if (!piece.CanMove(position))
            {
                throw new ChessException("");
            }

            if (HasPiece(position))
            {
                _pieces.Remove(piece);
            }

            piece.Move(position);
        }

        public bool HasPiece(Position position)
        {
            return GetPiece(position) != null;
        }

        public virtual Pieces.Piece GetPiece(Position position)
        {
            return Pieces.FirstOrDefault(piece => piece.Position.Equals(position));
        }
    }
}
