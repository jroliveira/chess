using System.Collections.Generic;
using Chess.Commands;
using Chess.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Test.Commands
{
    [TestFixture]
    public class MountChessboardTests
    {
        [Test]
        public void Execute_DadoTabuleiroVazio_DeveRetornarTabuleiroMontado()
        {
            var chessboard = new Chessboard();
            var mountChessboard = new MountChessboard(chessboard);

            mountChessboard.Execute();

            var pieces = new List<Piece>
            {
                new Rook(1, new Position('a', '1'), chessboard),
                new Knight(1, new Position('b', '1'), chessboard),
                new Bishop(1, new Position('c', '1'), chessboard),
                new King(1, new Position('d', '1'), chessboard),
                new Queen(1, new Position('e', '1'), chessboard),
                new Bishop(1, new Position('f', '1'), chessboard),
                new Knight(1, new Position('g', '1'), chessboard),
                new Rook(1, new Position('h', '1'), chessboard),
                new Pawn(1, new Position('a', '2'), chessboard),
                new Pawn(1, new Position('b', '2'), chessboard),
                new Pawn(1, new Position('c', '2'), chessboard),
                new Pawn(1, new Position('d', '2'), chessboard),
                new Pawn(1, new Position('e', '2'), chessboard),
                new Pawn(1, new Position('f', '2'), chessboard),
                new Pawn(1, new Position('g', '2'), chessboard),
                new Pawn(1, new Position('h', '2'), chessboard),
                new Pawn(2, new Position('a', '7'), chessboard),
                new Pawn(2, new Position('b', '7'), chessboard),
                new Pawn(2, new Position('c', '7'), chessboard),
                new Pawn(2, new Position('d', '7'), chessboard),
                new Pawn(2, new Position('e', '7'), chessboard),
                new Pawn(2, new Position('f', '7'), chessboard),
                new Pawn(2, new Position('g', '7'), chessboard),
                new Pawn(2, new Position('h', '7'), chessboard),
                new Rook(2, new Position('a', '8'), chessboard),
                new Knight(2, new Position('b', '8'), chessboard),
                new Bishop(2, new Position('c', '8'), chessboard),
                new King(2, new Position('d', '8'), chessboard),
                new Queen(2, new Position('e', '8'), chessboard),
                new Bishop(2, new Position('f', '8'), chessboard),
                new Knight(2, new Position('g', '8'), chessboard),
                new Rook(2, new Position('h', '8'), chessboard)
            };

            chessboard.Pieces.ShouldBeEquivalentTo(pieces);
        }
    }
}
