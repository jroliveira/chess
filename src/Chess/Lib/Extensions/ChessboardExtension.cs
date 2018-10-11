namespace Chess.Lib.Extensions
{
    using System.Linq;

    using Chess.Models;

    internal static class ChessboardExtension
    {
        internal static Chessboard ToModel(this Entities.Chessboard @this)
        {
            var pieces = new Piece[@this.Files.Count, @this.Ranks.Count];

            for (var fileIndex = 0; fileIndex < @this.Files.Count; fileIndex++)
            {
                for (var rankIndex = 0; rankIndex < @this.Ranks.Count; rankIndex++)
                {
                    var file = @this.Files.ElementAt(fileIndex);
                    var rank = @this.Ranks.ElementAt(rankIndex);

                    var piece = @this.GetPiece(new Entities.Position(file, rank));
                    if (!piece.IsDefined)
                    {
                        continue;
                    }

                    pieces[fileIndex, rankIndex] = new Piece(piece.Get());
                }
            }

            return new Chessboard(@this.Files, @this.Ranks, pieces);
        }
    }
}
