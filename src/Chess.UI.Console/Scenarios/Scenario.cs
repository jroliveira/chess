namespace Chess.UI.Console.Scenarios
{
    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;

    public class Scenario
    {
        protected readonly IGameMultiplayer Game;
        protected readonly IWriter Writer;
        protected readonly IReader Reader;
        protected readonly IScreen Screen;

        protected Scenario()
        {
        }

        protected Scenario(IGameMultiplayer game, IWriter writer, IReader reader, IScreen screen)
        {
            this.Game = game;
            this.Writer = writer;
            this.Reader = reader;
            this.Screen = screen;
        }

        protected void Setup()
        {
            this.Screen.Clean();
        }
    }
}