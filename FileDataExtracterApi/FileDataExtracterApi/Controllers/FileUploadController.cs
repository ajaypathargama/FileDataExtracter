using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileDataExtracterApi.Common;
using FileDataExtracterApi.Interfaces;

namespace FileDataExtracterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {      
        private readonly ILogger<FileUploadController> _logger;
        private ResponseContext _responseData;
        private IFileUploadService _fileUploadService;
        private IConfiguration configuration;
        public int chunkSize;
        private string tempFolder;

        public FileUploadController(IConfiguration configuration,IFileUploadService fileUploadService, ILogger<FileUploadController> logger)
        {
            this.configuration = configuration;
            _logger = logger;
            chunkSize = 1048576 * Convert.ToInt32(configuration["ChunkSize"]);
            tempFolder = configuration["TargetFolder"];
            _responseData = new ResponseContext();           
            _fileUploadService = fileUploadService;
        }
        /// <summary>
        /// It returns the default api response to indicate the API running status
        /// </summary>
        /// <returns>It returns the default api response message </returns>
        [HttpGet]
        public string Get()
        {
            return "FileDataExtracterApi running..it will be consumed in React WebApp to upload the csv flie";
        }
        /// <summary>
        /// This api upload the splitted files in chunc in the configured directory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns>it returns the status of uploaded files</returns>
        [HttpPost("UploadChunks")]
        public async Task<IActionResult> UploadChunks(string id, string fileName)
        {            
            _responseData = await _fileUploadService.UploadChunk(id, fileName, tempFolder, chunkSize, Request);
            return Ok(_responseData);
        }
        /// <summary>
        /// This API merge the chunks of uploaded file and returns the upload status of the file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>It returns the status of complete file upload status</returns>
        [HttpPost("UploadComplete")]
        public IActionResult UploadComplete(string fileName)
        {
            _responseData = _fileUploadService.UploadComplete(fileName, tempFolder);
            return Ok(_responseData);
        }        
    }
}
