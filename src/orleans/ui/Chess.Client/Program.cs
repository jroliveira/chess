namespace Chess.Client
{
    using static Chess.Client.Main;

    public class Program
    {
        public static void Main(string[] args) => StartClient()
            .GetAwaiter()
            .GetResult();
    }
}
