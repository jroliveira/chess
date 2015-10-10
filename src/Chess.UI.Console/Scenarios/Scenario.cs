using Chess.Multiplayer;
using Chess.UI.Console.Libs;

namespace Chess.UI.Console.Scenarios
{
    public class Scenario
    {
        protected readonly IGameMultiplayer Game;
        protected readonly IWriter Writer;
        protected readonly IReader Reader;
        protected readonly IScreen Screen;

        protected Scenario() { }

        protected Scenario(IGameMultiplayer game, IWriter writer, IReader reader, IScreen screen)
        {
            Game = game;
            Writer = writer;
            Reader = reader;
            Screen = screen;
        }

        protected void Setup()
        {
            Screen.Clean();
        }
    }
}