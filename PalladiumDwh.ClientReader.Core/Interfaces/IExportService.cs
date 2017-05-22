using System.Threading.Tasks;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IExportService
    {
        Task<string> ExportToJSonAsync(string exportDir = "");
        string Base64Encode(string plainText);
        string Base64Decode(string base64EncodedData);
    }
}