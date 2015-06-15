namespace Chess.UI.Console.Libs
{
    public interface IScreen
    {
        string Title { get; set; }
        Size Size { get; set; }
        void SetCursorPosition(int left, int top);
        void Clean();
    }
}