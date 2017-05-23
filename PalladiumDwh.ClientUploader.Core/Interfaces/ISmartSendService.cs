namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface ISmartSendService
    {
        void SendBatch(int batchSize = 100);
    }
}