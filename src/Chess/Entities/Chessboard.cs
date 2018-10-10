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

    using static Chess.Lib.Monad.Utils.Util;

    internal class Chessboard : Observable<Chessboard>
    {
        private readonly ObservableCollection<Piece> pieces;

        internal Chessboard()
            : this(new ObservableCollection<Piece>())
        {
        }

        internal Chessboard(ObservableCollection<Piece> pieces)
        {
            this.pieces = pieces;
            this.pieces.CollectionChanged += (_, args) => this.OnUpdate(this);
        }

        internal IReadOnlyCollection<char> Files => new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
        };

        internal IReadOnlyCollection<uint> Ranks => new[]
        {
            1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u,
        };

        internal virtual IReadOnlyCollection<Piece> Pieces => this.pieces
            .OrderBy(piece => piece.Position.Rank)
            .ThenBy(piece => piece.Position.File)
            .ToList();

        internal virtual void AddPiece(Piece piece) => this.pieces.Add(piece);

        internal virtual Try<Unit> MovePiece(Piece piece, Position newPosition)
        {
            if (!piece.CanMove(newPosition))
            {
                return new ChessException($"Cannot move the piece '{piece}'.");
            }

            var otherPieceOption = this.GetPiece(newPosition);
            var otherPiece = otherPieceOption.GetOrElse(default);
            if (otherPiece != null && !this.pieces.Remove(otherPiece))
            {
                return new ChessException($"Cannot move the piece '{piece}'.");
            }

            var newPiece = piece.Move(newPosition);
            this.pieces.Remove(piece);
            this.AddPiece(newPiece);

            return Unit();
        }

        internal virtual bool HasPiece(Position position) => this.GetPiece(position).GetOrElse(default) != null;

        internal virtual Option<Piece> GetPiece(Position position) => this.Pieces.FirstOrDefault(piece => piece.Equals(position));
    }
}
