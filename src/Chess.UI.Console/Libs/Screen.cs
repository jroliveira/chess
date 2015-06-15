namespace Chess.UI.Console.Libs
{
    public class Screen : IScreen
    {
        public string Title
        {
            get { return System.Console.Title; }
            set { System.Console.Title = value; }
        }

        public Size Size
        {
            get { return new Size { Height = System.Console.WindowHeight, Width = System.Console.WindowWidth }; }
            set { System.Console.SetWindowSize(value.Width, value.Height); }
        }

        public void SetCursorPosition(int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
        }

        public void Clean()
        {
            System.Console.Clear();
        }
    }
}
