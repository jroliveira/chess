namespace Chess.Queries
{
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Pieces;

    internal class GetPiecesQuery
    {
        public virtual ICollection<Piece> GetResult(IEnumerable<Piece> pieces)
        {
            return pieces
                .OrderBy(piece => piece.Position.Rank)
                 .ThenBy(piece => piece.Position.File)
                 .ToList();
        }
    }
}
