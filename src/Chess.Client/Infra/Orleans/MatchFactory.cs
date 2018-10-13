namespace Chess.Client.Infra.Orleans
{
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;
    using Chess.Lib.Monad.Extensions;

    using global::Orleans;

    internal static class MatchFactory
    {
        public static async Task<IMatch> CreateMatchWith(IGrainFactory grainFactory, Option<string> name)
        {
            var match = grainFactory.GetGrain<IMatch>(name.GetOrElse(name.GetOrElse("default")));
            await match.WakeUp();

            return match;
        }
    }
}
