namespace Chess.Commands
{
    using System;

    using Chess.Pieces;

    internal class MountChessboardCommand
    {
        private readonly Chessboard chessboard;

        public MountChessboardCommand(Chessboard chessboard)
        {
            this.chessboard = chessboard;
        }

        protected MountChessboardCommand()
        {
        }

        public virtual void Execute()
        {
            for (var r = 0; r < 8; r++)
            {
                var rank = this.chessboard.Ranks[r];

                if (r.Equals(0) || r.Equals(7))
                {
                    for (var f = 0; f < 8; f++)
                    {
                        var file = this.chessboard.Files[f];

                        var position = new Position(file, rank);

                        if (f.Equals(0) || f.Equals(7))
                        {
                            this.PutPiece<Rook>(position);
                        }
                        else if (f.Equals(1) || f.Equals(6))
                        {
                            this.PutPiece<Knight>(position);
                        }
                        else if (f.Equals(2) || f.Equals(5))
                        {
                            this.PutPiece<Bishop>(position);
                        }
                        else if (f.Equals(3))
                        {
                            this.PutPiece<King>(position);
                        }
                        else if (f.Equals(4))
                        {
                            this.PutPiece<Queen>(position);
                        }
                    }
                }
                else if (r.Equals(1) || r.Equals(6))
                {
                    for (var f = 0; f < 8; f++)
                    {
                        var file = this.chessboard.Files[f];

                        var position = new Position(file, rank);

                        this.PutPiece<Pawn>(position);
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

            var newPiece = Activator.CreateInstance(typeof(TPiece), player, position, this.chessboard) as Piece;

            this.chessboard.AddPiece(newPiece);
        }
    }
}
