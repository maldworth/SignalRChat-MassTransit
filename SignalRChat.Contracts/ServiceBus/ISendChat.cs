namespace SignalRChat.Contracts.ServiceBus
{
    public interface ISendChat
    {
        string Name { get; set; }
        string Message { get; set; }
    }
}
