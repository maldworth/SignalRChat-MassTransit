namespace SignalRChat.Web.Bootstrapper
{
    using Autofac;
    using Autofac.Integration.SignalR;
    using SignalRChat.Web.Bootstrapper.Modules;
    using System.Reflection;

    public class IocConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new HubModule(Assembly.Load("SignalRChat.Web")));

            builder.RegisterModule(new BusModule(Assembly.Load("SignalRChat.Web")));

            return builder.Build();
        }
    }
}
