using FileDataExtracterApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileDataExtracterApi.Interfaces
{
    public interface IDBService<T>
    {
        Task<bool> AddBulkDataAsync(List<T> data, FileDataExtracterDBContext fileDataExtracterDBContext);
    }
}
