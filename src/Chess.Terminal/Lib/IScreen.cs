namespace Chess.Terminal.Lib
{
    public interface IScreen
    {
        string Title { get; set; }

        void ClearScreen();

        void ClearOption();

        char GetChar();

        string GetString();

        void WriteError(string text);

        void WriteNewLine();

        void WriteOption(char option, string text);

        void WriteText(string text);

        void WriteText(char letter);

        void WriteTitle(string text);
    }
}