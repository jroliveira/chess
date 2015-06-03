using Chess.Exceptions;
using Chess.Game;
using Chess.Game.Extensions;

namespace Chess
{
    public class ChessGame
    {
        private readonly Chessboard _chessboard;
        private readonly MountChessboard _mountChessboard;

        public char[] Files { get { return _chessboard.Files; } }
        public char[] Ranks { get { return _chessboard.Ranks; } }

        public ChessGame()
        {
            _chessboard = new Chessboard();
            _mountChessboard = new MountChessboard(_chessboard);
        }

        public void Start()
        {
            _mountChessboard.Mount();
        }

        public void Move(string piecePosition, string newPosition)
        {
            var piece = _chessboard.GetPiece(piecePosition.ToPosition());

            if (piece == null)
            {
                throw new PieceIsNullException(piecePosition);
            }

            _chessboard.MovePiece(piece, newPosition.ToPosition());
        }

        public ChessPiece GetPiece(char file, char rank)
        {
            var position = new Position(file, rank);
            var piece = _chessboard.GetPiece(position);

            return piece == null
                ? null
                : new ChessPiece(piece.Name, piece.Player);
        }
    }
}