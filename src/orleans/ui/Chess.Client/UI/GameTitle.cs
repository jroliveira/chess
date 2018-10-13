namespace Chess.Client.UI
{
    using static Chess.Client.Infra.UI.Writer;

    internal static class GameTitle
    {
        private const string AsciiArt = @"
                      _._   +
                  ,   ( )  ( )   ,
        [UU] T\  (^)  / \  / \  (^)  /T [UU]
     ()  ||  |\) / \  | |  | |  / \ (/|  ||  ()
     {}  {}  {}  { }  { }  { }  { }  {}  {}  {}
    {__}{__}{__}{___}{___}{___}{___}{__}{__}{__}
";

        internal static void ShowGameTitle()
        {
            ClearScreen();
            WriteText(AsciiArt);
        }
    }
}
