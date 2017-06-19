namespace Chess.UI.Console.Scenarios.Matches
{
    using System;
    using System.Linq;

    using Chess.Exceptions;
    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;
    using Chess.UI.Console.Libs.Match;

    public class Match : Scenario
    {
        protected readonly Chessboard Chessboard;

        public Match(IGameMultiplayer game, Chessboard chessboard, IWriter writer, IReader reader, IScreen screen)
            : base(game, writer, reader, screen)
        {
            this.Chessboard = chessboard;
        }

        protected Match()
        {
        }

        protected void NextMove()
        {
            this.ClearNextMove();

            this.Screen.SetCursorPosition(0, 22);

            this.Writer.WriteWithSleep("   NEXT MOVE -> piece ");
            var file = this.GetFile();
            var rank = this.GetRank();
            var piecePosition = new string(new[] { file, rank });

            this.Writer.WriteWithSleep(" move for ");
            file = this.GetFile();
            rank = this.GetRank();
            var newPosition = new string(new[] { file, rank });

            try
            {
                this.Game.Move(piecePosition, newPosition);
                this.Chessboard.Print(this.Game);
                this.ClearNextMove();
            }
            catch (ChessException exception)
            {
                this.Writer.WriteError(exception.Message);
            }
        }

        private void ClearNextMove()
        {
            this.Screen.SetCursorPosition(0, 22);
            this.Writer.Erase();
        }

        private char GetFile()
        {
            var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

            return this.ReadKey(files.Contains, "Insert between a and h");
        }

        private char GetRank()
        {
            var ranks = new[] { '8', '7', '6', '5', '4', '3', '2', '1' };

            return this.ReadKey(ranks.Contains, "Insert between 8 and 1");
        }

        private char ReadKey(Func<char, bool> condition, string invalidMessage)
        {
            bool keyValid;
            char key;

            do
            {
                key = this.Reader.ReadKey();
                keyValid = condition(key);

                if (!keyValid)
                {
                    this.Writer.WriteError(invalidMessage);
                }
            }
            while (!keyValid);

            return key;
        }
    }
}
