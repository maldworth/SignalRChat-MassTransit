using MassTransit;
using Microsoft.AspNet.SignalR.Infrastructure;
using SignalRChat.Contracts.ServiceBus;
using SignalRChat.Web.Hubs;
using System.Threading.Tasks;

namespace SignalRChat.Web.Consumers
{
    public class SendChatConsumer : IConsumer<ISendChat>
    {
        private readonly IConnectionManager _connectionManager;

        public SendChatConsumer(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        public Task Consume(ConsumeContext<ISendChat> context)
        {
            _connectionManager.GetHubContext<ChatHub>().Clients.All.addNewMessageToPage(context.Message.Name, context.Message.Message);
            return Task.FromResult(0);
        }
    }
}