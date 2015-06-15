using System;
using System.Linq;
using Chess.Exceptions;
using Chess.UI.Console.Libs;
using Chess.UI.Console.Libs.Match;

namespace Chess.UI.Console.Scenarios.Matches
{
    public class Match : Scenario
    {
        protected readonly Chessboard Chessboard;

        protected Match() { }

        public Match(ChessGame game, Chessboard chessboard, IWriter writer, IReader reader, IScreen screen)
            : base(game, writer, reader, screen)
        {
            Chessboard = chessboard;
        }

        protected void NextMove()
        {
            ClearNextMove();

            Screen.SetCursorPosition(0, 49);
            Writer.NewLine();
            Writer.WriteInsideTheBox("next move");
            Writer.NewLine();
            Writer.NewLine();

            Writer.WriteWithSleep("   {0} piece ", ArrowRight);
            var file = GetFile();
            var rank = GetRank();
            var piecePosition = new string(new[] { file, rank });

            Writer.WriteWithSleep(" move for ");
            file = GetFile();
            rank = GetRank();
            var newPosition = new string(new[] { file, rank });

            try
            {
                Game.Move(piecePosition, newPosition);
                Chessboard.Print(Game);
                ClearNextMove();
            }
            catch (ChessException exception)
            {
                Writer.WriteError(exception.Message);
            }
        }

        private void ClearNextMove()
        {
            Screen.SetCursorPosition(0, 49);
            for (var i = 0; i < 7; i++)
            {
                Writer.Erase();
                Writer.NewLine();
            }
        }

        private char GetFile()
        {
            var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

            return ReadKey(files.Contains, "( invalid file! please insert between a and h )");
        }

        private char GetRank()
        {
            var ranks = new[] { '8', '7', '6', '5', '4', '3', '2', '1' };

            return ReadKey(ranks.Contains, "( invalid rank! please insert between 8 and 1 )");
        }

        private char ReadKey(Func<char, bool> condition, string invalidMessage)
        {
            bool keyValid;
            char key;

            do
            {
                key = Reader.ReadKey();
                keyValid = condition(key);

                if (!keyValid)
                {
                    Writer.WriteError(invalidMessage);
                }
            } while (!keyValid);

            return key;
        }
    }
}
