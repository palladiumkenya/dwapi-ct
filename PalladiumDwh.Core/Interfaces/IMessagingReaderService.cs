namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingReaderService
    {
        string QueueName { get; }
        object Queue { get; }

        void Initialize();
        void Read();
    }
}