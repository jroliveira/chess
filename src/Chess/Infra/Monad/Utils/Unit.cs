namespace Chess.Infra.Monad.Utils
{
    public static partial class Util
    {
        private static readonly Unit Default = new Unit();

        public static Unit Unit() => Default;
    }
}
