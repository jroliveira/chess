namespace Chess.Client.Infra.UI
{
    internal static class Symbols
    {
        internal static class Board
        {
            internal static char Pipe => '│';

            internal static char Dash => '─';

            internal static class Upper
            {
                internal static char Left => '┌';

                internal static char Center => '┬';

                internal static char Right => '┐';
            }

            internal static class Middle
            {
                internal static char Left => '├';

                internal static char Center => '┼';

                internal static char Right => '┤';
            }

            internal static class Bottom
            {
                internal static char Left => '└';

                internal static char Center => '┴';

                internal static char Right => '┘';
            }
        }
    }
}
