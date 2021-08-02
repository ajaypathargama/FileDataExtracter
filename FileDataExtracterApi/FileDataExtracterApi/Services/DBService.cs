using FileDataExtracterApi.Interfaces;
using FileDataExtracterApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using FileDataExtracterApi.Data;

namespace FileDataExtracterApi.Services
{
    public class DBService : IDBService<Artikel>
    {
        public async Task<bool> AddBulkDataAsync(List<Artikel> data, FileDataExtracterDBContext fileDataExtracterDBContext)
        {
            try
            {
                await fileDataExtracterDBContext.BulkInsertAsync(data);
                return true;
            }
            catch(Exception ex)
            {
                //log exception details
                return false;
            }            
        }
    }
}
