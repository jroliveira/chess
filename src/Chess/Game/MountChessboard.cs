using System;
using Chess.Game.Pieces;

namespace Chess.Game
{
    internal class MountChessboard
    {
        private readonly Chessboard _chessboard;

        public MountChessboard(Chessboard chessboard)
        {
            _chessboard = chessboard;
        }

        public void Mount()
        {
            PutPieces();
        }

        private void PutPieces()
        {
            for (var r = 0; r < 8; r++)
            {
                var rank = _chessboard.Ranks[r];

                if (r.Equals(0) || r.Equals(7))
                {
                    for (var f = 0; f < 8; f++)
                    {
                        var file = _chessboard.Files[f];

                        var position = new Position(file, rank);

                        if (f.Equals(0) || f.Equals(7))
                        {
                            PutPiece<Rook>(position);
                        }
                        else if (f.Equals(1) || f.Equals(6))
                        {
                            PutPiece<Knight>(position);
                        }
                        else if (f.Equals(2) || f.Equals(5))
                        {
                            PutPiece<Bishop>(position);
                        }
                        else if (f.Equals(3))
                        {
                            PutPiece<King>(position);
                        }
                        else if (f.Equals(4))
                        {
                            PutPiece<Queen>(position);
                        }
                    }
                }
                else if (r.Equals(1) || r.Equals(6))
                {
                    for (var f = 0; f < 8; f++)
                    {
                        var file = _chessboard.Files[f];

                        var position = new Position(file, rank);

                        PutPiece<Pawn>(position);
                    }
                }
            }
        }

        private void PutPiece<TPiece>(Position position)
            where TPiece : Piece
        {
            var player = 1;

            if (position.Rank.Equals('7') || position.Rank.Equals('8'))
            {
                player = 2;
            }

            var newPiece = Activator.CreateInstance(typeof(TPiece), player, position, _chessboard) as Piece;

            _chessboard.AddPiece(newPiece);
        }
    }
}
