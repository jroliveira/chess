namespace Chess.Terminal.Lib.Constants
{
    public static class Symbols
    {
        public static class Box
        {
            public static char Pipe => '║';

            public static char Dash => '═';

            public static class Upper
            {
                public static char Left => '╔';

                public static char Right => '╗';
            }

            public static class Bottom
            {
                public static char Left => '╚';

                public static char Right => '╝';
            }
        }

        public static class Board
        {
            public static char Pipe => '│';

            public static char Dash => '─';

            public static class Upper
            {
                public static char Left => '┌';

                public static char Center => '┬';

                public static char Right => '┐';
            }

            public static class Middle
            {
                public static char Left => '├';

                public static char Center => '┼';

                public static char Right => '┤';
            }

            public static class Bottom
            {
                public static char Left => '└';

                public static char Center => '┴';

                public static char Right => '┘';
            }
        }
    }
}
