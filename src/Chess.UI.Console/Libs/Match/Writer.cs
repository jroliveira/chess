namespace Chess.UI.Console.Libs.Match
{
    using System;
    using System.Collections.Generic;

    public class Writer : Libs.Writer
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
                { DividerPosition.Top,    () => this.Divider('┌', '┐', '┬') },
                { DividerPosition.Middle, () => this.Divider('├', '┤', '┼') },
                { DividerPosition.Bottom, () => this.Divider('└', '┘', '┴') },
            };

            dividers[position]();
        }

        public void Dash()
        {
            Console.Write("─");
        }

        public void Pipe()
        {
            var current = Console.BackgroundColor;

            this.color.Restore();

            Console.Write("│");

            if (current == ConsoleColor.White)
            {
                this.color.White();
            }
        }

        private void Divider(char leftCorner, char rightCorner, char separator)
        {
            const int Space = 3;
            const int Block = 8;

            Console.Write("     {0}", leftCorner);

            for (var i = 0; i < Block; i++)
            {
                for (var j = 0; j < Space; j++)
                {
                    this.Dash();
                }

                if (!i.Equals(7))
                {
                    Console.Write(separator);
                }
            }

            Console.Write(rightCorner);

            this.NewLine();
        }
    }
}