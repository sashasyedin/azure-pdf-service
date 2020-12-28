using System.IO;
using System.Threading.Tasks;

namespace PdfService.Api.Helpers
{
    public interface IStorageManager
    {
        Task<MemoryStream> DownloadFileAsync(string containerId, string path);
        Task UploadFileAsync(string containerId, string path, Stream file);
    }
}