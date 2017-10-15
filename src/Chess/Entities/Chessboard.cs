namespace Chess.Entities
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Chess.Entities.Pieces;
    using Chess.Lib;
    using Chess.Lib.Exceptions;

    internal class Chessboard : Observable<Chessboard>
    {
        private readonly ObservableCollection<Piece> pieces;

        public Chessboard()
            : this(new ObservableCollection<Piece>())
        {
        }

        internal Chessboard(ObservableCollection<Piece> pieces)
        {
            this.pieces = pieces;
            this.pieces.CollectionChanged += (_, args) => this.OnUpdate(this);
        }

        public IReadOnlyCollection<char> Files => new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'
        };

        public IReadOnlyCollection<char> Ranks => new[]
        {
            '8', '7', '6', '5', '4', '3', '2', '1'
        };

        public virtual IReadOnlyCollection<Piece> Pieces => this.pieces
            .OrderBy(piece => piece.Position.Rank)
            .ThenBy(piece => piece.Position.File)
            .ToList();

        public virtual void AddPiece(Piece piece)
        {
            this.pieces.Add(piece);
        }

        public virtual void MovePiece(Piece piece, Position newPosition)
        {
            if (!piece.CanMove(newPosition))
            {
                throw new ChessException($"Cannot move the piece '{piece}'.");
            }

            var otherPiece = this.GetPiece(newPosition);
            if (otherPiece != null && !this.pieces.Remove(otherPiece))
            {
                throw new ChessException($"Cannot move the piece '{piece}'.");
            }

            var newPiece = Piece.Clone(piece, newPosition);
            this.pieces.Remove(piece);
            this.AddPiece(newPiece);
        }

        public virtual bool HasPiece(Position position)
        {
            return this.GetPiece(position) != null;
        }

        public virtual Piece GetPiece(Position position)
        {
            return this.Pieces.FirstOrDefault(piece => piece.Equals(position));
        }
    }
}
