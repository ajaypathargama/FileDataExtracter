using FileDataExtracterApi.Interfaces;
using FileDataExtracterApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileDataExtracterApi.Services
{
    public class JsonConverterService : IJsonConverterService<Artikel>
    {
        public async Task<bool> WriteDataToJson(List<Artikel> artikels,string jsonFilePath)
        {       
            try
            {
                string JsonResult = JsonConvert.SerializeObject(artikels);

                using (var streamWriter = new StreamWriter(jsonFilePath, true))
                {
                    await streamWriter.WriteLineAsync(JsonResult.ToString());
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }          
    }
}
