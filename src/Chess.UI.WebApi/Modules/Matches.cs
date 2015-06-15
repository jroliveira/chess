using Chess.UI.WebApi.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Chess.UI.WebApi.Modules
{
    public class Matches : NancyModule
    {
        private readonly Game _game;

        public Matches(Game game)
            : base("/matches")
        {
            _game = game;

            Post["/"] = _ => Create(this.Bind<Match>());
        }

        private dynamic Create(Match model)
        {
            _game.Start();

            var response = new
            {
                Id = 1
            };

            return Response.AsJson(response, HttpStatusCode.Accepted);
        }
    }
}
