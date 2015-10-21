namespace SignalRChat.Web.Bootstrapper
{
    using Autofac;
    using Autofac.Integration.SignalR;
    using MassTransit;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Microsoft.AspNet.SignalR.Infrastructure;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Call our IoC static helper method to start the typical Autofac SignalR setup
            var container = IocConfig.RegisterDependencies();

            // Get your HubConfiguration. In OWIN, we create one rather than using GlobalHost
            var hubConfig = new HubConfiguration();

            // Sets the dependency resolver to be autofac.
            hubConfig.Resolver = new AutofacDependencyResolver(container);

            // OWIN SIGNALR SETUP:

            // Register the Autofac middleware FIRST, then the standard SignalR middleware.
            app.UseAutofacMiddleware(container);
            app.MapSignalR("/signalr", hubConfig);

            // There's not a lot of documentation or discussion for owin getting the hubcontext
            // Got this from here: https://stackoverflow.com/questions/29783898/owin-signalr-autofac
            var builder = new ContainerBuilder();
            var connManager = hubConfig.Resolver.Resolve<IConnectionManager>();
            builder.RegisterInstance(connManager)
                .As<IConnectionManager>()
                .SingleInstance();
            builder.Update(container);

            container.Resolve<IBusControl>().Start();
        }
    }
}
