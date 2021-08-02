using FileDataExtracterApi.Common;
using FileDataExtracterApi.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileDataExtracterApi.Services
{
    public class FileUploadService : IFileUploadService
    {
        ResponseContext _responseData;
        public async Task<ResponseContext> UploadChunk(string id, string fileName, string tempFolder, int chunkSize, HttpRequest Request)
        {
            _responseData = new();
            try
            {
                var chunkNumber = id;                
                string newpath = Path.Combine(tempFolder + @"\Temp", fileName + chunkNumber);
                using (FileStream fs = System.IO.File.Create(newpath))
                {
                    byte[] bytes = new byte[chunkSize];
                    int bytesRead = 0;
                    while ((bytesRead = await Request.Body.ReadAsync(bytes, 0, bytes.Length)) > 0)
                    {
                        fs.Write(bytes, 0, bytesRead);
                    }
                }
                return _responseData;
            }
            catch (Exception ex)
            {
                _responseData.ErrorMessage = ex.Message;
                _responseData.IsSuccess = false;
                return _responseData;
            }           
        }              

        public ResponseContext UploadComplete(string fileName, string tempFolder)
        {
            _responseData = new();
            try
            {
                string tempPath = tempFolder + @"\Temp";
                string newPath = Path.Combine(tempPath, fileName);
                if (File.Exists(newPath))
                    File.Move(newPath, tempFolder + @"\Backup");
                string[] filePaths = Directory.GetFiles(tempPath).Where(p => p.Contains(fileName)).OrderBy(p => Int32.Parse(p.Replace(fileName, "$").Split('$')[1])).ToArray();

                foreach (string filePath in filePaths)
                {
                    MergeChunks(newPath, filePath);
                }
                File.Move(Path.Combine(tempPath, fileName), Path.Combine(tempFolder, fileName));
                return _responseData;
            }
            catch (Exception ex)
            {
                _responseData.ErrorMessage = ex.Message;
                _responseData.IsSuccess = false;
                return _responseData;
            }
        }

        public void MergeChunks(string chunk1, string chunk2)
        {
            FileStream fs1 = null;
            FileStream fs2 = null;
            try
            {
                fs1 = File.Open(chunk1, FileMode.Append);
                fs2 = File.Open(chunk2, FileMode.Open);
                byte[] fs2Content = new byte[fs2.Length];
                fs2.Read(fs2Content, 0, (int)fs2.Length);
                fs1.Write(fs2Content, 0, (int)fs2.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
            }
            finally
            {
                if (fs1 != null) fs1.Close();
                if (fs2 != null) fs2.Close();
                File.Delete(chunk2);
            }
        }
    }
}
