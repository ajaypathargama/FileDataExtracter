using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileDataExtracterApi.Interfaces
{
    public interface IJsonConverterService<T> where T : class
    {
        Task<bool> WriteDataToJson(List<T> data, string jsonFilePath);
    }
}
