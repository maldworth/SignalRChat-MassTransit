using MassTransit;
using Microsoft.AspNet.SignalR;
using SignalRChat.Contracts.ServiceBus;

namespace SignalRChat.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IBus _bus;

        public ChatHub(IBus bus)
        {
            _bus = bus;
        }
        public void Send(string name, string message)
        {
            _bus.Publish<ISendChat>(new { Name = name, Message = message });
        }
    }
}
