namespace Chess.Client
{
    using LightInject;

    using static Chess.Client.Infra.IoC.Container;

    public class Program
    {
        public static void Main(string[] args) => GetContainer()
            .GetInstance<Main>()
            .Start()
            .GetAwaiter()
            .GetResult();
    }
}
