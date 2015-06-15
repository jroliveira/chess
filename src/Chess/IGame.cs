using Chess.Models;

namespace Chess
{
    public interface IGame
    {
        char[] Files { get; }
        char[] Ranks { get; }

        void Start();
        void Move(string piecePosition, string newPosition);
        Piece GetPiece(char file, char rank);
    }
}