using Autofac;
using MassTransit;
using System;

namespace SignalRChat.Web.Bootstrapper.Modules
{
    public class MassTransitModule : Module
    {
        private readonly System.Reflection.Assembly[] _assembliesToScan;

        public MassTransitModule(params System.Reflection.Assembly[] assembliesToScan)
        {
            _assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Registers all consumers with our container
            builder.RegisterAssemblyTypes(_assembliesToScan)
                .Where(t =>
                {
                    var a = typeof(IConsumer).IsAssignableFrom(t);
                    return a;
                })
                .AsSelf();

            // Creates our bus from the factory and registers it as a singleton against two interfaces
            builder.Register(c => Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/signalrchat"), h =>
                {
                    h.Username("signalrchat");
                    h.Password("signal");
                });

                sbc.ReceiveEndpoint(host, "sendchat_queue", ep => ep.LoadFrom(c.Resolve<ILifetimeScope>()));
            }))
                .As<IBusControl>()
                .As<IBus>()
                .SingleInstance();

        }
    }
}
