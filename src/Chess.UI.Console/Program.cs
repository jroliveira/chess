namespace Chess.UI.Console
{
    class Program
    {
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
            System.Console.WriteLine("NEXT MOVE");
            System.Console.WriteLine("");

            System.Console.Write("piece: ");
            var file = System.Console.ReadKey().KeyChar;
            var rawn = System.Console.ReadKey().KeyChar;
            System.Console.WriteLine("");

            piece = new string(new[] { file, rawn });

            System.Console.Write("position: ");
            file = System.Console.ReadKey().KeyChar;
            rawn = System.Console.ReadKey().KeyChar;

            return new string(new[] { file, rawn });
        }
    }
}
