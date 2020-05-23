namespace Chess
{
    using System;
    using System.Collections.Generic;

    public static class Constants
    {
        public static class Chessboard
        {
            public static IReadOnlyCollection<char> Files => new[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
            };

            public static IReadOnlyCollection<byte> Ranks => new byte[]
            {
                1, 2, 3, 4, 5, 6, 7, 8,
            };
        }

        public static class ErrorMessages
        {
            public static ChessException CannotBeNullOrEmpty(string property) => new ChessException($"{property} can't be null or empty");

            public static class PieceError
            {
                public static ChessException CannotMove(string piece) => new ChessException($"Can't move the piece '{piece}'");

                public static ChessException DoesNotExist(Type type) => new ChessException($"Piece type '{type.Name}' doesn't exist");

                public static ChessException DoesNotExist(PieceColor color) => new ChessException($"Piece color '{color}' doesn't exist");

                public static ChessException DoesNotExist(string position) => new ChessException($"Piece '{position}' don't exist");
            }

            public static class UserError
            {
                public static ChessException IsAlreadyInUse(string user) => new ChessException($"User name '{user}' is already in use");

                public static ChessException IsAlreadyInUse(PieceColor color) => new ChessException($"Piece color '{color}' is already in use");

                public static ChessException IsNotPlaying(string user) => new ChessException($"Player '{user}' isn't playing");

                public static ChessException DoesNotBelongToYou(string piece) => new ChessException($"Piece '{piece}' doesn't belong to you");
            }
        }
    }
}
