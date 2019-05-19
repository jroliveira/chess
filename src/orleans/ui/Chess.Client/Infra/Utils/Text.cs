namespace Chess.Client.Infra.Utils
{
    using System.Text;

    using Chess.Client.Infra.Extensions;

    using static System.Convert;
    using static System.Math;

    internal static partial class Util
    {
        internal static (string Left, string Text, string Right) CenterValue(string text, int areaLength)
        {
            var firstHalfLength = ToInt32(Ceiling(new decimal(areaLength / 2) - (text.Length / 2)));
            var secondHalfLength = areaLength - (firstHalfLength + text.Length);

            return (
                ' '.Repeat(firstHalfLength),
                text,
                ' '.Repeat(secondHalfLength));
        }

        internal static string RepeatValue(object value, int times)
        {
            var @return = new StringBuilder();

            for (var time = 0; time < times; time++)
            {
                @return.Append(value);
            }

            return @return.ToString();
        }
    }
}
