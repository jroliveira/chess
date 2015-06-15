namespace Chess.UI.Console.Libs
{
    public class Reader : IReader
    {
        public char ReadKey()
        {
            return System.Console.ReadKey().KeyChar;
        }

        public string ReadValue()
        {
            return System.Console.ReadLine();
        }
    }
}