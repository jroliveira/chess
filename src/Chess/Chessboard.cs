using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Chess.Exceptions;
using Chess.Pieces;

namespace Chess
{
    public class Chessboard
    {
        private readonly ICollection<Piece> _pieces;
        private readonly MountChessboard _mountChessboard;

        public char[] Files { get { return new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }; } }
        public char[] Ranks { get { return new[] { '8', '7', '6', '5', '4', '3', '2', '1' }; } }
        public ICollection<Piece> Pieces { get { return _pieces.OrderBy(piece => piece.Position.Rank).ThenBy(piece => piece.Position.File).ToList(); } }

        public Chessboard()
        {
            _mountChessboard = new MountChessboard(this);
            _pieces = new Collection<Piece>();
        }

        public void Start()
        {
            _mountChessboard.Mount();
        }

        public virtual Piece GetPiece(string position)
        {
            return Pieces.FirstOrDefault(piece => piece.Position.Equals(position.ToPosition()));
        }

        public bool HasPiece(string position)
        {
            return GetPiece(position) != null;
        }

        private void RemovePiece(string position)
        {
            var piece = GetPiece(position);

            if (piece == null)
            {
                throw new ChessException("");
            }

            _pieces.Remove(piece);
        }

        public void AddPiece(Piece piece)
        {
            _pieces.Add(piece);
        }

        public void MovePiece(Piece piece, string position)
        {
            if (!piece.CanMove(position.ToPosition()))
            {
                throw new ChessException("");
            }

            if (HasPiece(position))
            {
                RemovePiece(position);
            }

            piece.Move(position.ToPosition());
        }
    }
}
