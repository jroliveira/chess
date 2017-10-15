namespace Chess.Terminal.Scenarios
{
    using System;
    using System.Reactive.Subjects;
    using System.Threading;

    using Chess.Lib.Extensions;
    using Chess.Terminal.Lib;

    public abstract class Scenario<TGame>
        where TGame : IGame
    {
        protected readonly TGame Game;
        protected readonly IScreen Screen;

        private readonly ISubject<string> title;
        private readonly ISubject<string> requestOption;

        protected Scenario(TGame game, IScreen screen)
        {
            this.Game = game;

            this.Screen = screen;
            this.Screen.Title = "Chess";

            this.title = new Subject<string>();
            this.title.Subscribe(this.Screen.WriteTitle);

            this.requestOption = new Subject<string>();
            this.requestOption.Subscribe(text => text.Subscribe(letter =>
            {
                Thread.Sleep(60);
                this.Screen.WriteText(letter);
            }));
        }

        protected Scenario()
        {
        }

        public virtual void Start()
        {
            this.Screen.ClearScreen();
            this.Initialize();
        }

        protected abstract void Initialize();

        protected TReturn RequestOption<TReturn>(string text, Func<TReturn> getOption)
        {
            this.requestOption.OnNext(text);
            return getOption();
        }

        protected void SetTitle(string text)
        {
            this.title.OnNext(text);
        }

        protected Func<char> ReadOption(Func<char, bool> condition, string invalidMessage)
        {
            return () =>
            {
                bool valid;
                char option;

                do
                {
                    option = this.Screen.GetChar();
                    valid = condition(option);

                    if (!valid)
                    {
                        this.Screen.WriteError(invalidMessage);
                    }
                }
                while (!valid);

                return option;
            };
        }
    }
}