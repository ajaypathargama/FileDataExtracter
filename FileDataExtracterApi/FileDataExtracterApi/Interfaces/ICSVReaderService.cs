using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileDataExtracterApi.Interfaces
{
    public interface ICSVReaderService<T> where T : class
    {
        Task<List<T>> ReadDataFromCSV(string filePath);
    }
}
