namespace Chess.Test.Domain.Pieces.Pawn
{
    using Chess.Test.Domain.Pieces.Shared;

    using Xunit;

    using static Chess.Domain.Pieces.Pawn.Pawn;
    using static Chess.PieceColor;

    public sealed class PawnTests : PieceBaseTests
    {
        [Theory]
        [InlineData(WhitePiece, '♙')]
        [InlineData(BlackPiece, '♟')]
        public override void ImplicitOperatorChar_GivenPieceColor_ShouldReturn(PieceColor pieceColor, char expected) => this.ImplicitOperatorChar(CreatePawn("a2", pieceColor), expected);

        [Theory]
        [InlineData(WhitePiece, '♙')]
        [InlineData(BlackPiece, '♟')]
        public override void ImplicitOperatorPiece_GivenPieceColor_ShouldReturn(PieceColor pieceColor, char symbol) => this.ImplicitOperatorPiece(CreatePawn("a2", pieceColor), symbol);

        [Theory]
        [InlineData("a2", WhitePiece, '♙')]
        [InlineData("a7", BlackPiece, '♟')]
        public override void ToString_GivenPieceBase_ShouldReturn(string position, PieceColor pieceColor, char symbol) => this.ToString(CreatePawn(position, pieceColor), position, symbol);
    }
}
