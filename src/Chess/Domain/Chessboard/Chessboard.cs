namespace Chess.Domain.Chessboard
{
    using System.Collections.Generic;
    using System.Linq;

    using Chess;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;
    using Chess.Infra.Monad.Linq;

    using static Chess.Constants.Chessboard;
    using static Chess.Constants.ErrorMessages.Piece;
    using static Chess.Pieces;

    internal sealed class Chessboard
    {
        private readonly IReadOnlyCollection<PieceBase> pieces;

        private Chessboard(IEnumerable<PieceBase> pieces) => this.pieces = new List<PieceBase>(pieces);

        public static implicit operator Chessboard(List<PieceBase> pieces) => new Chessboard(pieces);

        public static implicit operator Pieces(Chessboard chessboard) => chessboard.ToPieces();

        internal Try<Chessboard> MovePiece(PieceBase piece, Position newPosition)
        {
            if (!piece.CanMove(newPosition, this))
            {
                return CannotMove(piece);
            }

            IReadOnlyCollection<PieceBase> newPieces = new List<PieceBase>(this.pieces);

            this.GetPiece(newPosition)
                .Select(otherPiece => newPieces = newPieces.RemoveItem(otherPiece));

            return piece
                .Move(newPosition)
                .Select(newPiece =>
                {
                    newPieces = newPieces.RemoveItem(piece);
                    newPieces = newPieces.AddItem(newPiece);

                    return new Chessboard(newPieces);
                });
        }

        internal Option<PieceBase> GetPiece(Position position) => this.pieces.FirstOrDefault(piece => piece.Equals(position));

        internal Pieces ToPieces()
        {
            var newPieces = new Piece[Files.Count, Ranks.Count];

            for (var fileIndex = 0; fileIndex < Files.Count; fileIndex++)
            {
                for (var rankIndex = 0; rankIndex < Ranks.Count; rankIndex++)
                {
                    var file = Files.ElementAt(fileIndex);
                    var rank = Ranks.ElementAt(rankIndex);

                    var fileLocalIndex = fileIndex;
                    var rankLocalIndex = rankIndex;

                    this.GetPiece($"{file}{rank}")
                        .Select(piece => newPieces[fileLocalIndex, rankLocalIndex] = piece);
                }
            }

            return CreatePieces(newPieces);
        }
    }
}
