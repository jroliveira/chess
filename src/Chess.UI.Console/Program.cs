using System.Threading;

namespace Chess.UI.Console
{
    class Program
    {
        private const char ArrowRight = (char)26;

        static void Main()
        {
            var game = new Game.Game();
            game.Start();

            while (true)
            {
                string piece;
                var position = NextMove(out piece);

                game.Move(piece, position);
            }
        }

        static string NextMove(out string piece)
        {
            System.Console.WriteLine("");
            Text("NEXT MOVE {0}", (char)1);
            System.Console.WriteLine("");
            System.Console.WriteLine("");

            Text(" {0} Piece ", ArrowRight);
            var file = System.Console.ReadKey().KeyChar;
            var rawn = System.Console.ReadKey().KeyChar;

            piece = new string(new[] { file, rawn });

            Text(" move for ");

            file = System.Console.ReadKey().KeyChar;
            rawn = System.Console.ReadKey().KeyChar;

            return new string(new[] { file, rawn });
        }

        static void Text(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Text(text);
        }

        static void Text(string text)
        {
            foreach (var t in text)
            {
                Thread.Sleep(80);
                System.Console.Write(t);
            }
        }
    }
}
