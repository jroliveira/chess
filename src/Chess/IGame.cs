namespace Chess
{
    using System;
    using System.Collections.Generic;

    using Chess.Models;

    public interface IGame : IObservable<IGame>
    {
        IReadOnlyCollection<char> Files { get; }

        IReadOnlyCollection<char> Ranks { get; }

        void Start();

        void Move(string piecePosition, string newPosition);

        Piece GetPiece(char file, char rank);
    }
}