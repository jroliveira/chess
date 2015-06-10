using Chess.UI.Console.Libs;

namespace Chess.UI.Console.Scenarios
{
    public abstract class Scenario
    {
        protected const char ArrowRight = (char)26;

        protected readonly ChessGame Game;
        protected readonly Text Text;

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
  

            ";
            }
        }

        protected Scenario(ChessGame game)
        {
            Game = game;
            Text = new Text();
        }

        public void Start()
        {
            Setup();
            Show();
        }

        protected abstract void Show();

        private void Setup()
        {
            System.Console.Clear();
            Text.Write(Title);
            Text.NewLine();
        }
    }
}