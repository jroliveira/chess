namespace Chess.Client.Scenarios.Match
{
    using System;

    using Chess.Models;

    internal static partial class MatchScenario
    {
        internal static void ShowMatch(Chessboard chessboard)
        {
            DrawScenario(chessboard);
            PrintSuccessMessage();
        }

        internal static void MovePiece(Action<string, string> done)
        {
            DrawScenario();

            var (piecePosition, newPosition) = GetNextMove();

            done(piecePosition, newPosition);
        }
    }
}
