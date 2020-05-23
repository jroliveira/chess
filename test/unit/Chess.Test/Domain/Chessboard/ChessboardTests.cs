namespace Chess.Test.Domain.Chessboard
{
    using System.Collections.Generic;

    using Chess;
    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;

    using FluentAssertions;

    using Xunit;

    using static Chess.Domain.Pieces.Pawn.Pawn;
    using static Chess.Domain.Shared.Position;
    using static Chess.Domain.User.Player;
    using static Chess.PieceColor;

    public sealed class ChessboardTests
    {
        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │   │   │   │   │   │   │   │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │   │   │   │   │   │   │   │   │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │   │   │   │   │   │   │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │   │   │   │   │   │   │   │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │   │   │   │   │   │   │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │   │ ○ │   │   │   │   │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │ p │   │   │   │   │   │   │   │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │   │   │   │   │   │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Fact]
        public void MovePiece_GivenInvalidNewPosition_ShouldReturnException()
        {
            var expected = new ChessException("Can't move the piece '♙a2'");
            Chessboard chessboard = new List<PieceBase> { CreatePawn("a2", WhitePiece) };

            var actual = chessboard
                .MovePiece(CreatePlayer("jr", WhitePiece), CreatePosition("a2"), CreatePosition("b3"))
                .Match(_ => _, _ => default);

            actual.ShouldBeEquivalentTo(expected);
        }

        /*     a   b   c   d   e   f   g   h
         *   ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * 8 │   │   │   │   │   │   │   │   │ 8
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 7 │   │   │   │   │   │   │   │   │ 7
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 6 │   │   │   │   │   │   │   │   │ 6
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 5 │   │   │   │   │   │   │   │   │ 5
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 4 │ ● │   │   │   │   │   │   │   │ 4
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 3 │   │   │   │   │   │   │   │   │ 3
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 2 │ p │   │   │   │   │   │   │   │ 2
         *   ├───┼───┼───┼───┼───┼───┼───┼───┤
         * 1 │   │   │   │   │   │   │   │   │ 1
         *   └───┴───┴───┴───┴───┴───┴───┴───┘
         *     a   b   c   d   e   f   g   h   */

        [Fact]
        public void MovePiece_GivenValidNewPosition_ShouldReturnNewChessboard()
        {
            Piece[,] expected = (Pieces)(Chessboard)new List<PieceBase> { CreatePawn("a4", WhitePiece) };
            Chessboard chessboard = new List<PieceBase> { CreatePawn("a2", WhitePiece) };

            Piece[,] actual = chessboard
                .MovePiece(CreatePlayer("jr", WhitePiece), CreatePosition("a2"), CreatePosition("a4"))
                .Match(_ => default, _ => _)
                .ToPieces();

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
