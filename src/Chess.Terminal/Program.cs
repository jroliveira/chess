namespace Chess.Terminal
{
    using Chess.Terminal.IoC;
    using Chess.Terminal.Scenarios;

    using LightInject;

    public class Program
    {
        public static void Main()
        {
            var container = Container.GetContainer();
            using (container.BeginScope())
            {
                var main = container.GetInstance<Main>();
                main.Start();
            }
        }
    }
}