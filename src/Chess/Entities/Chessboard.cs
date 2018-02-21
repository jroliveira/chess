namespace Chess.Entities
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Chess.Entities.Pieces;
    using Chess.Lib;
    using Chess.Lib.Exceptions;
    using Chess.Lib.Monad;
    using Chess.Lib.Monad.Extensions;

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
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
        };

        public IReadOnlyCollection<char> Ranks => new[]
        {
            '1', '2', '3', '4', '5', '6', '7', '8',
        };

        public virtual IReadOnlyCollection<Piece> Pieces => this.pieces
            .OrderBy(piece => piece.Position.Rank)
            .ThenBy(piece => piece.Position.File)
            .ToList();

        public virtual void AddPiece(Piece piece) => this.pieces.Add(piece);

        public virtual void MovePiece(Piece piece, Position newPosition)
        {
            if (!piece.CanMove(newPosition))
            {
                throw new ChessException($"Cannot move the piece '{piece}'.");
            }

            var otherPieceOption = this.GetPiece(newPosition);
            var otherPiece = otherPieceOption.GetOrElse(default);
            if (otherPiece != null && !this.pieces.Remove(otherPiece))
            {
                throw new ChessException($"Cannot move the piece '{piece}'.");
            }

            var newPiece = Piece.Clone(piece, newPosition);
            this.pieces.Remove(piece);
            this.AddPiece(newPiece);
        }

        public virtual bool HasPiece(Position position) => this.GetPiece(position).GetOrElse(default) != null;

        public virtual Option<Piece> GetPiece(Position position) => this.Pieces.FirstOrDefault(piece => piece.Equals(position));
    }
}
