using Chess.UI.Console.Libs;

namespace Chess.UI.Console.Scenarios
{
    public class Scenario
    {
        protected const char ArrowRight = (char)26;

        protected readonly ChessGame Game;
        protected readonly IWriter Writer;
        protected readonly IReader Reader;
        protected readonly IScreen Screen;

        protected string Title
        {
            get
            {
                return @"
           _                        
          | |                       
     ___  | |__     ___   ___   ___ 
    / __| | '_ \   / _ \ / __| / __|
   | (__  | | | | |  __/ \__ \ \__ \
    \___| |_| |_|  \___| |___/ |___/
  

════════════════════════════════════════════════════════════════════════════════════";
            }
        }

        protected Scenario() { }

        protected Scenario(ChessGame game, IWriter writer, IReader reader, IScreen screen)
        {
            Game = game;
            Writer = writer;
            Reader = reader;
            Screen = screen;
        }

        protected void Setup()
        {
            Screen.Clean();
            Writer.Write(Title);
            Writer.NewLine();
            Writer.NewLine();
            Writer.NewLine();
        }
    }
}