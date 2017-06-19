namespace Chess
{
    using Chess.Commands;
    using Chess.Exceptions;
    using Chess.Extensions;
    using Chess.Models;

    public class Game : IGame
    {
        private readonly Chessboard chessboard;
        private readonly MountChessboardCommand mountChessboard;

        public Game()
        {
            this.chessboard = new Chessboard();
            this.mountChessboard = new MountChessboardCommand(this.chessboard);
        }

        internal Game(Chessboard chessboard, MountChessboardCommand mountChessboard)
        {
            this.chessboard = chessboard;
            this.mountChessboard = mountChessboard;
        }

        public char[] Files => this.chessboard.Files;

        public char[] Ranks => this.chessboard.Ranks;

        public void Start()
        {
            this.mountChessboard.Execute();
        }

        public virtual void Move(string piecePosition, string newPosition)
        {
            var position = newPosition.ToPosition();
            var piece = this.chessboard.GetPiece(piecePosition.ToPosition());

            if (piece == null)
            {
                throw new ChessException("Peça não existe.");
            }

            this.chessboard.MovePiece(piece, position);
        }

        public Piece GetPiece(char file, char rank)
        {
            var position = new Position(file, rank);
            var piece = this.chessboard.GetPiece(position);

            return piece == null
                ? null
                : new Piece(piece.Name, piece.Player);
        }
    }
}