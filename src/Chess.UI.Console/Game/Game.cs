namespace Chess.UI.Console.Game
{
    public class Game
    {
        private readonly Chessboard _chessboard;

        public Game()
        {
            _chessboard = new Chessboard();
        }

        public void Start()
        {
            _chessboard.Start();
            Print(_chessboard);
        }

        public void Move(string piecePosition, string newPosition)
        {
            var piece = _chessboard.GetPiece(piecePosition);
            _chessboard.MovePiece(piece, newPosition);
            Print(_chessboard);
        }

        private static void Print(Chessboard chessboard)
        {
            System.Console.Clear();

            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("CHESSBOARD");
            System.Console.WriteLine("");
            System.Console.Write("   ");
            foreach (var file in chessboard.Files)
            {
                System.Console.Write("|   {0}    ", file);
            }

            System.Console.WriteLine("----------------------------------------------------------------------------");

            foreach (var rank in chessboard.Ranks)
            {
                System.Console.Write("{0}  ", rank);
                foreach (var file in chessboard.Files)
                {
                    var position = new Position(file, rank);
                    var piece = chessboard.GetPiece(position.ToString());

                    if (piece == null)
                    {
                        System.Console.Write("|        ");
                    }
                    else
                    {
                        var name = piece.GetType().Name.Substring(0, 4);
                        System.Console.Write("|  {0}  ", name);
                    }
                }
                System.Console.WriteLine("|");
                System.Console.WriteLine("----------------------------------------------------------------------------");
            }
        }
    }
}