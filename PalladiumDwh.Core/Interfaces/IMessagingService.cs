namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingService
    {
        string QueueName { get; }
        object Queue { get; }

        void Initialize();
        string Send(object message);
    }
}