namespace SignalRChat.Web.Bootstrapper.Modules
{
    using Autofac;
    using Autofac.Integration.SignalR;

    public class HubModule : Module
    {
        private readonly System.Reflection.Assembly[] _assembliesToScan;

        public HubModule(params System.Reflection.Assembly[] assembliesToScan)
        {
            _assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Register your SignalR hubs.
            builder.RegisterHubs(_assembliesToScan);
        }
    }
}