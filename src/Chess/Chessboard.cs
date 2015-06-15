using System.Collections.Generic;
using System.Linq;
using Chess.Exceptions;
using Chess.Pieces;
using Chess.Queries;

namespace Chess
{
    internal class Chessboard
    {
        private readonly ICollection<Piece> _pieces;
        private readonly GetPiecesQuery _getPieces;

        public char[] Files { get { return new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }; } }
        public char[] Ranks { get { return new[] { '8', '7', '6', '5', '4', '3', '2', '1' }; } }
        public ICollection<Piece> Pieces { get { return _getPieces.GetResult(_pieces); } }

        internal Chessboard(ICollection<Piece> pieces, GetPiecesQuery getPieces)
        {
            _pieces = pieces;
            _getPieces = getPieces;
        }

        public Chessboard()
            : this(new List<Piece>(), new GetPiecesQuery())
        {

        }

        public virtual void AddPiece(Piece piece)
        {
            _pieces.Add(piece);
        }

        public virtual void MovePiece(Piece piece, Position position)
        {
            if (!piece.CanMove(position))
            {
                throw new ChessException("Não é possível mover a peça.");
            }

            if (HasPiece(position))
            {
                if (!_pieces.Remove(piece))
                {
                    throw new ChessException("Não é possível remover a peça.");
                }
            }

            piece.Move(position);
        }

        public virtual bool HasPiece(Position position)
        {
            return GetPiece(position) != null;
        }

        public virtual Piece GetPiece(Position position)
        {
            return Pieces.FirstOrDefault(piece => piece.Position.Equals(position));
        }
    }
}
