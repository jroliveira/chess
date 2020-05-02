namespace Chess.Domain.Chessboard
{
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Domain.Pieces.Shared;
    using Chess.Infra.Monad.Linq;

    using static Chess.Constants.Chessboard;
    using static Chess.Domain.Chessboard.InitialChessboardSetup;
    using static Chess.Domain.Pieces.Shared.PieceBase;
    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.PieceColor;

    internal static class MountChessboardCommand
    {
        internal static Chessboard MountChessboard()
        {
            var pieces = new List<PieceBase>();

            for (var rankIndex = 0; rankIndex < 8; rankIndex++)
            {
                for (var fileIndex = 0; fileIndex < 8; fileIndex++)
                {
                    var file = Files.ElementAt(fileIndex);
                    var rank = Ranks.ElementAt(rankIndex);

                    GetPieceType($"{file}{rank}")
                        .Select(type => CreatePiece(
                            type,
                            $"{file}{rank}",
                            rank < 5 ? WhitePiece : BlackPiece).Select(
                                piece =>
                                {
                                    pieces.Add(piece);
                                    return Unit();
                                }));
                }
            }

            return new List<PieceBase>(pieces);
        }
    }
}
