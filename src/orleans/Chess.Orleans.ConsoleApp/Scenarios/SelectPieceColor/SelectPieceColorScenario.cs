namespace Chess.Orleans.ConsoleApp.Scenarios.SelectPieceColor
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chess.Infra.Monad;
    using Chess.Orleans.ConsoleApp.Scenarios.Shared;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.GameMenuItem;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;
    using static Chess.PieceColor;

    internal sealed class SelectPieceColorScenario : ScenarioBase
    {
        protected override async Task<GameData> DrawScenario(GameData data)
        {
            Option<PieceColor> playingWithOption = default;

            if (!await data.MatchGrainOption.Get().HasBegun())
            {
                var option = GetFromSelectBox(" Color number: ", new List<(int, string)>
                {
                    (1, "Black"),
                    (2, "White"),
                });

                if (option == 0)
                {
                    return data.SetGameMenuItem(MainMenu);
                }

                playingWithOption = option == 2 ? WhitePiece : BlackPiece;
            }

            var tryMatch = await data.MatchGrainOption.Get().JoinUser(data.UserGrainOption, data.UserNameOption, playingWithOption);

            return tryMatch.Match(
                exception =>
                {
                    ShowError(exception.Message, confirm: true);
                    return data.SetGameMenuItem(MainMenu);
                },
                match => data.SetPieceColor(playingWithOption, Some(match)));
        }
    }
}
