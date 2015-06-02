using System.Diagnostics;
using NUnit.Framework;

namespace Chess.Test
{
    [TestFixture]
    public class Console
    {
        private Chessboard _chessboard;

        [SetUp]
        public void Setup()
        {
            _chessboard = new Chessboard();
            _chessboard.Start();

            Print(_chessboard);
        }

        [Test]
        public void Start() { }

        [Test]
        public void Move()
        {
            var chessboard = new Chessboard();
            chessboard.Start();

            var piece = chessboard.GetPiece("a7");

            chessboard.MovePiece(piece, "a5");

            Print(chessboard);
        }

        private static void Print(Chessboard chessboard)
        {
            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("CHESSBOARD");
            Debug.WriteLine("");
            Debug.Write("   ");
            foreach (var file in chessboard.Files)
            {
                Debug.Write(string.Format("|   {0}    ", file));
            }

            Debug.WriteLine("----------------------------------------------------------------------------");

            foreach (var rank in chessboard.Ranks)
            {
                Debug.Write(string.Format("{0}  ", rank));
                foreach (var file in chessboard.Files)
                {
                    var position = new Position(file, rank);
                    var piece = chessboard.GetPiece(position.ToString());

                    if (piece == null)
                    {
                        Debug.Write("|        ");
                    }
                    else
                    {
                        var name = piece.GetType().Name.Substring(0, 4);
                        Debug.Write(string.Format("|  {0}  ", name));
                    }
                }
                Debug.WriteLine("|");
                Debug.WriteLine("----------------------------------------------------------------------------");
            }
        }
    }
}
