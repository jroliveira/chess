namespace Chess.Client
{
    using System;
    using System.Linq;

    using Chess.Client.Infra.Extensions;
    using Chess.Interfaces;
    using Chess.Lib.Extensions;
    using Chess.Lib.Monad;
    using Chess.Models;

    using static System.Threading.Thread;
    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Lib.Monad.Utils.Util;

    internal class GameClient : IGameClient
    {
        private Option<string> playerName;

        public void GameChanged(Try<Chessboard> chessboard, IGameServer gameServer)
        {
            chessboard.Match(
                exception => WriteError(exception.Message),
                game => game.Draw());

            this.NextMove(gameServer);
        }

        public void SetPlayer(Option<string> name) => this.playerName = name;

        private static (char, char) RequestOption(string text)
        {
            text.ForEach(letter =>
            {
                Sleep(60);
                WriteValue(letter);
            });

            var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            var ranks = new[] { '1', '2', '3', '4', '5', '6', '7', '8' };

            var file = ReadOption(files.Contains, "Insert between a and h")();
            var rank = ReadOption(ranks.Contains, "Insert between 8 and 1")();

            return (file, rank);

            Func<char> ReadOption(Func<char, bool> condition, string invalidMessage) => () =>
            {
                bool valid;
                char option;

                do
                {
                    option = ReadChar();
                    valid = condition(option);

                    if (!valid)
                    {
                        WriteError(invalidMessage);
                    }
                }
                while (!valid);

                return option;
            };
        }

        private void NextMove(IGameServer gameServer)
        {
            ClearOption();

            var (file, rank) = RequestOption("   NEXT MOVE -> piece ");
            var piecePosition = new string(new[] { file, rank });

            var (newFile, newRank) = RequestOption(" move for ");
            var newPosition = new string(new[] { newFile, newRank });

            gameServer.MovePiece(piecePosition, newPosition, this.playerName);
            ClearOption();

            void ClearOption()
            {
                SetCursor(top: 22);

                for (var i = 0; i < 40; i++)
                {
                    WriteValue(' ');
                }

                SetCursor(top: 22);
            }
        }
    }
}
