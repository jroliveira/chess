using System;
using System.Linq;
using Chess.Exceptions;
using Chess.UI.Console.Libs.Match;

namespace Chess.UI.Console.Scenarios
{
    public class Match : Scenario
    {
        private readonly Chessboard _chessboard;

        public Match(ChessGame game)
            : base(game)
        {
            _chessboard = new Chessboard();

            game.Played += OnPlayed;
        }

        private void OnPlayed(string piece, string newPosition)
        {
            _chessboard.Print(Game);
        }

        protected override void Show()
        {
            Game.Start();
            _chessboard.Print(Game);

            while (true)
            {
                string piece;
                var position = NextMove(out piece);

                try
                {
                    Game.Move(piece, position);
                    _chessboard.Print(Game);
                }
                catch (ChessException exception)
                {
                    Text.Error(exception.Message);
                }
            }
        }

        private string NextMove(out string piece)
        {
            System.Console.SetCursorPosition(0, 47);
            System.Console.WriteLine("");
            System.Console.WriteLine("           ");
            System.Console.WriteLine("");
            System.Console.WriteLine("                                                     ");
            System.Console.SetCursorPosition(0, 47);

            Text.NewLine();
            Text.WriteInsideTheBox("next move");
            Text.NewLine();
            Text.NewLine();

            Text.WriteWithSleep("   {0} piece ", ArrowRight);
            var file = GetFile();
            var rank = GetRank();

            piece = new string(new[] { file, rank });

            Text.WriteWithSleep(" move for ");

            file = GetFile();
            rank = GetRank();

            return new string(new[] { file, rank });
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
                key = System.Console.ReadKey().KeyChar;
                keyValid = condition(key);

                if (!keyValid)
                {
                    Text.Error(invalidMessage);
                }
            } while (!keyValid);

            return key;
        }
    }
}