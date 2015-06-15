using Chess.UI.Console.IoC;
using Chess.UI.Console.Scenarios;

namespace Chess.UI.Console
{
    class Program
    {
        static void Main()
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