namespace Chess.Domain.Chessboard
{
    using System.Collections.Generic;
    using System.Linq;

    using Chess;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Domain.User;
    using Chess.Infra.Monad;
    using Chess.Infra.Monad.Extensions;

    using static System.String;

    using static Chess.Constants.Chessboard;
    using static Chess.Constants.ErrorMessages;
    using static Chess.Constants.ErrorMessages.PieceError;
    using static Chess.Constants.ErrorMessages.UserError;
    using static Chess.Domain.Shared.Position;
    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Pieces;

    internal sealed class Chessboard
    {
        private readonly IReadOnlyCollection<PieceBase> pieces;

        private Chessboard(IEnumerable<PieceBase> pieces) => this.pieces = new List<PieceBase>(pieces);

        public static implicit operator Chessboard(List<PieceBase> pieces) => new Chessboard(pieces);

        public static implicit operator Pieces(Chessboard chessboard) => chessboard.ToPieces();

        internal Try<Chessboard> MovePiece(
            Option<Player> playerOption,
            Option<Position> piecePositionOption,
            Option<Position> newPositionOption)
        {
            var piecePosition = piecePositionOption.GetOrElse(Empty);
            if (piecePosition == Empty)
            {
                return CannotBeNullOrEmpty("Piece position");
            }

            return this.GetPiece(piecePositionOption)
                .Select(piece =>
                {
                    if (!piece.BelongsTo(playerOption))
                    {
                        return DoesNotBelongToYou(piece);
                    }

                    if (!piece.CanMove(newPositionOption, this))
                    {
                        return CannotMove(piece);
                    }

                    IReadOnlyCollection<PieceBase> newPieces = new List<PieceBase>(this.pieces);

                    this.GetPiece(newPositionOption)
                        .ForEach(otherPiece => newPieces = newPieces.RemoveItem(otherPiece));

                    return piece
                        .Move(newPositionOption)
                        .Select(newPiece =>
                        {
                            newPieces = newPieces.RemoveItem(piece);
                            newPieces = newPieces.AddItem(newPiece);

                            return new Chessboard(newPieces);
                        });
                });
        }

        internal Try<PieceBase> GetPiece(Option<Position> positionOption) => positionOption
            .Fold(Failure<PieceBase>(CannotBeNullOrEmpty("Position")))(position =>
            {
                var piece = this.pieces.FirstOrDefault(item => item.Equals(position));

                return piece == null
                    ? Failure<PieceBase>(DoesNotExist(position))
                    : Success(piece);
            });

        internal Pieces ToPieces()
        {
            var newPieces = new Chess.Piece[Files.Count, Ranks.Count];

            for (var fileIndex = 0; fileIndex < Files.Count; fileIndex++)
            {
                for (var rankIndex = 0; rankIndex < Ranks.Count; rankIndex++)
                {
                    var file = Files.ElementAt(fileIndex);
                    var rank = Ranks.ElementAt(rankIndex);

                    var fileLocalIndex = fileIndex;
                    var rankLocalIndex = rankIndex;

                    this.GetPiece(CreatePosition($"{file}{rank}"))
                        .ForEach(piece => newPieces[fileLocalIndex, rankLocalIndex] = piece);
                }
            }

            return CreatePieces(newPieces);
        }
    }
}
