using System;
using System.Collections.Generic;

namespace Chess.UI.Console.Libs.Match
{
    public class Writer : Libs.Writer
    {
        private readonly Color _color;

        public Writer()
        {
            _color = new Color();
        }

        public void Divider(DividerPosition position)
        {
            var dividers = new Dictionary<DividerPosition, Action>
            {
                { DividerPosition.Top,    () => Divider('┌', '┐', '┬') },
                { DividerPosition.Middle, () => Divider('├', '┤', '┼') },
                { DividerPosition.Bottom, () => Divider('└', '┘', '┴') },

                //{ Position.Top,    () => Divider('╔', '╗', '╦') },
                //{ Position.Middle, () => Divider('╠', '╣', '╬') },
                //{ Position.Bottom, () => Divider('╚', '╝', '╩') }
            };

            dividers[position]();
        }

        private void Divider(char leftCorner, char rightCorner, char separator)
        {
            System.Console.Write("     {0}", leftCorner);

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Dash();
                }

                if (!i.Equals(7))
                {
                    System.Console.Write(separator);
                }
            }

            System.Console.Write(rightCorner);

            NewLine();
        }

        public void Dash()
        {
            System.Console.Write("─");
        }

        public void Pipe()
        {
            var current = System.Console.BackgroundColor;

            _color.Restore();

            System.Console.Write("│");

            if (current == ConsoleColor.White)
            {
                _color.White();
            }
        }
    }
}