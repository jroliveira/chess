using System.Collections.Generic;
using System.Linq;
using Chess.Pieces;

namespace Chess.Queries
{
    internal class GetPiecesQuery
    {
        public virtual ICollection<Piece> GetResult(IEnumerable<Piece> pieces)
        {
            return pieces.OrderBy(piece => piece.Position.Rank)
                         .ThenBy(piece => piece.Position.File)
                         .ToList();
        }
    }
}
