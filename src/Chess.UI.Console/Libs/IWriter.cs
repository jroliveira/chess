namespace Chess.UI.Console.Libs
{
    public interface IWriter
    {
        void NewLine();
        void Erase();
        void Write(string text);
        void WriteError(string text);
        void WriteOption(string option, string text);
        void WriteWithSleep(string format, params object[] args);
        void WriteWithSleep(string text);
        void WriteInsideTheBox(string text);
    }
}