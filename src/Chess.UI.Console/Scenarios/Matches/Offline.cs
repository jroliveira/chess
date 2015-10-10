using Chess.Multiplayer;
using Chess.UI.Console.Libs;
using Chess.UI.Console.Libs.Match;

namespace Chess.UI.Console.Scenarios.Matches
{
    public class Offline : Match
    {
        protected Offline()
        {
            
        }

        public Offline(IGameMultiplayer game, Chessboard chessboard, IWriter writer, IReader reader, IScreen screen)
            : base(game, chessboard, writer, reader, screen)
        {
            
        }

        public virtual void Start()
        {
            Setup();

            Game.Start();
            Chessboard.Print(Game);

            while (true)
            {
                NextMove();
            }
        }
    }
}