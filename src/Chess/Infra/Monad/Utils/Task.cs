namespace Chess.Infra.Monad.Utils
{
    using System.Threading.Tasks;

    using static System.Threading.Tasks.Task;

    public static partial class Util
    {
        public static Task<TReturn> Task<TReturn>(TReturn @return) => FromResult(@return);
    }
}
