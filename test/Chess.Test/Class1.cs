using System.Diagnostics;
using NUnit.Framework;

namespace Chess.Test
{
    [TestFixture]
    public class Console
    {
        [Test]
        public void Start()
        {
            var chessboard = new Chessboard();
            chessboard.Start();

            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("CHESSBOARD");
            Debug.WriteLine("");
            Debug.WriteLine("-------------------------------------------------------------------------");
            foreach (var file in chessboard.Files)
            {
                Debug.Write(string.Format("   {0}     ", file));
            }

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
                Debug.WriteLine("-------------------------------------------------------------------------");
            }

        }
    }
}
