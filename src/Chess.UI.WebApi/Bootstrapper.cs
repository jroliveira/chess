using System;
using Chess.UI.WebApi.Lib;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Json;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace Chess.UI.WebApi
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            JsonSettings.MaxJsonLength = Int32.MaxValue;

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register(typeof(JsonSerializer), typeof(CustomJsonSerializer));
        }
    }
}
