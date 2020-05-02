namespace Chess.Orleans.ConsoleApp.Scenarios.Match
{
    using System.Linq;

    using Chess;
    using Chess.Infra.Monad;

    using static System.String;

    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    internal static class MatchExtension
    {
        internal static Unit Draw(this Option<Match> @this) => Draw(@this.Get());

        internal static Unit Draw(this Match @this)
        {
            SetCursorPosition(top: 6);

            WriteBoxLine($" {Join(" vs ", @this.Players.Select(player => player))} ");

            if (@this.Spectators.Any())
            {
                WriteBoxLine($"[ {Join(',', @this.Spectators.Select(spectator => spectator))} ]");
            }

            SetCursorPosition(top: 9);

            return @this.Pieces.Draw();
        }
    }
}
