using Chess.Multiplayer;
using Chess.UI.Console.Libs;
using Chess.UI.Console.Libs.Match;

namespace Chess.UI.Console.Scenarios.Matches
{
    public class Online : Match
    {
        protected Online() { }

        public Online(IGameMultiplayer game, Chessboard chessboard, IWriter writer, IReader reader, IScreen screen)
            : base(game, chessboard, writer, reader, screen)
        {
            game.Played += OnPlayed;
        }

        public void Start(bool isServer)
        {
            Setup();

            Game.Start();
            Chessboard.Print(Game);

            if (isServer)
            {
                NextMove();
            }
            else
            {
                Game.WaitingTheMove();
            }
        }

        private void OnPlayed(string piecePosition, string newPosition)
        {
            Chessboard.Print(Game);
            NextMove();
        }
    }
}