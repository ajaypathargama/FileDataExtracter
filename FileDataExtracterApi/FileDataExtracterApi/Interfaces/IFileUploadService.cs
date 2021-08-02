using FileDataExtracterApi.Common;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileDataExtracterApi.Interfaces
{
    public interface IFileUploadService
    {
        Task<ResponseContext> UploadChunk(string id, string fileName, string tempFolder, int chunkSize, HttpRequest Request);

        ResponseContext UploadComplete(string fileName, string tempFolder);

        void MergeChunks(string chunk1, string chunk2);
    }
}
