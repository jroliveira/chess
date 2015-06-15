using Chess.Commands;
using Chess.Exceptions;
using Chess.Extensions;
using Chess.Models;

namespace Chess
{
    public class Game : IGame
    {
        private readonly Chessboard _chessboard;
        private readonly MountChessboardCommand _mountChessboard;

        public char[] Files { get { return _chessboard.Files; } }
        public char[] Ranks { get { return _chessboard.Ranks; } }

        internal Game(Chessboard chessboard, MountChessboardCommand mountChessboard)
        {
            _chessboard = chessboard;
            _mountChessboard = mountChessboard;
        }

        public Game()
        {
            _chessboard = new Chessboard();
            _mountChessboard = new MountChessboardCommand(_chessboard);
        }

        public void Start()
        {
            _mountChessboard.Execute();
        }

        public virtual void Move(string piecePosition, string newPosition)
        {
            var position = newPosition.ToPosition();
            var piece = _chessboard.GetPiece(piecePosition.ToPosition());

            if (piece == null)
            {
                throw new ChessException("Peça não existe.");
            }

            _chessboard.MovePiece(piece, position);
        }

        public Piece GetPiece(char file, char rank)
        {
            var position = new Position(file, rank);
            var piece = _chessboard.GetPiece(position);

            return piece == null
                ? null
                : new Piece(piece.Name, piece.Player);
        }
    }
}