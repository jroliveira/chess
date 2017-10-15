namespace Chess.Terminal.Lib.Match
{
    using System;
    using System.Collections.Generic;

    using static System.Console;
    using static System.ConsoleColor;

    using static Chess.Terminal.Lib.Constants.Symbols;

    public class Writer : Screen
    {
        private readonly Color color;

        public Writer()
        {
            this.color = new Color();
        }

        public void Divider(DividerPosition position)
        {
            var dividers = new Dictionary<DividerPosition, Action>
            {
                { DividerPosition.Top,    () => this.Divider(Board.Upper.Left, Board.Upper.Right, Board.Upper.Center) },
                { DividerPosition.Middle, () => this.Divider(Board.Middle.Left, Board.Middle.Right, Board.Middle.Center) },
                { DividerPosition.Bottom, () => this.Divider(Board.Bottom.Left, Board.Bottom.Right, Board.Bottom.Center) },
            };

            dividers[position]();
        }

        public void Pipe()
        {
            var current = BackgroundColor;

            this.color.Restore();

            Write(Board.Pipe);

            if (current == White)
            {
                this.color.White();
            }
        }

        private void Divider(char leftCorner, char rightCorner, char separator)
        {
            const int Space = 3;
            const int Block = 8;

            Write("     {0}", leftCorner);

            for (var i = 0; i < Block; i++)
            {
                for (var j = 0; j < Space; j++)
                {
                    Write(Board.Dash);
                }

                if (!i.Equals(7))
                {
                    Write(separator);
                }
            }

            Write(rightCorner);

            this.WriteNewLine();
        }
    }
}