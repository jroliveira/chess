namespace Chess.Orleans.ConsoleApp.Infra.Orleans
{
    using System.Threading.Tasks;

    using Chess.Infra.Monad;
    using Chess.Orleans.ConsoleApp.Scenarios.Match;
    using Chess.Orleans.Contract;

    using global::Orleans;

    internal static class UserGrainFactory
    {
        internal static Task<IUserGrain> CreateUserGrain(Option<IClusterClient> clusterClientOption, Option<string> userNameOption) => clusterClientOption
            .Get()
            .CreateObjectReference<IUserGrain>(new UserGrain(userNameOption));
    }
}
