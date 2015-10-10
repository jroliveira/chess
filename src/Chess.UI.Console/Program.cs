namespace Chess.UI.Console
{
    using System.Text;

    using Chess.UI.Console.IoC;
    using Chess.UI.Console.Scenarios;

    using LightInject;

    public class Program
    {
        public static void Main()
        {
            System.Console.OutputEncoding = Encoding.GetEncoding(65001);

            var container = Container.GetContainer();
            using (container.BeginScope())
            {
                var main = container.GetInstance<Main>();
                main.Start();
            }
        }
    }
}