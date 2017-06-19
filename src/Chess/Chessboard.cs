namespace Chess
{
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Exceptions;
    using Chess.Pieces;
    using Chess.Queries;

    internal class Chessboard
    {
        private readonly ICollection<Piece> pieces;
        private readonly GetPiecesQuery getPieces;

        public Chessboard()
            : this(new List<Piece>(), new GetPiecesQuery())
        {
        }

        internal Chessboard(ICollection<Piece> pieces, GetPiecesQuery getPieces)
        {
            this.pieces = pieces;
            this.getPieces = getPieces;
        }

        public char[] Files => new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'
        };

        public char[] Ranks => new[]
        {
            '8', '7', '6', '5', '4', '3', '2', '1'
        };

        public ICollection<Piece> Pieces => this.getPieces.GetResult(this.pieces);

        public virtual void AddPiece(Piece piece)
        {
            this.pieces.Add(piece);
        }

        public virtual void MovePiece(Piece piece, Position position)
        {
            if (!piece.CanMove(position))
            {
                throw new ChessException("Não é possível mover a peça.");
            }

            if (this.HasPiece(position))
            {
                if (!this.pieces.Remove(piece))
                {
                    throw new ChessException("Não é possível remover a peça.");
                }
            }

            piece.Move(position);
        }

        public virtual bool HasPiece(Position position)
        {
            return this.GetPiece(position) != null;
        }

        public virtual Piece GetPiece(Position position)
        {
            return this.Pieces.FirstOrDefault(piece => piece.Position.Equals(position));
        }
    }
}
