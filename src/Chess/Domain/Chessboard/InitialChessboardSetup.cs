namespace Chess.Domain.Chessboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Domain.Pieces.Bishop;
    using Chess.Domain.Pieces.King;
    using Chess.Domain.Pieces.Knight;
    using Chess.Domain.Pieces.Pawn;
    using Chess.Domain.Pieces.Queen;
    using Chess.Domain.Pieces.Rook;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    using static Chess.Infra.Monad.Utils.Util;

    internal static class InitialChessboardSetup
    {
        private static readonly IDictionary<IReadOnlyCollection<Position>, Type> InitialPositions = new Dictionary<IReadOnlyCollection<Position>, Type>
        {
            { new Position[] { "a1", "a8", "h1", "h8" }, typeof(Rook) },
            { new Position[] { "b1", "b8", "g1", "g8" }, typeof(Knight) },
            { new Position[] { "c1", "c8", "f1", "f8" }, typeof(Bishop) },
            { new Position[] { "d1", "d8" }, typeof(Queen) },
            { new Position[] { "e1", "e8" }, typeof(King) },
            { new Position[] { "a2", "a7", "b2", "b7", "c2", "c7", "d2", "d7", "e2", "e7", "f2", "f7", "g2", "g7", "h2", "h7" }, typeof(Pawn) },
        };

        internal static Option<Type> GetPieceType(Position position)
        {
            var key = InitialPositions.Keys.FirstOrDefault(positions => positions.Contains(position));

            return (key != null && InitialPositions.ContainsKey(key))
                ? Some(InitialPositions[key])
                : None();
        }
    }
}
