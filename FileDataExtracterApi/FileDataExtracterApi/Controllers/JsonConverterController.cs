using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FileDataExtracterApi.Model;
using System.IO;
using Microsoft.Extensions.Configuration;
using FileDataExtracterApi.Interfaces;
using FileDataExtracterApi.Data;


namespace FileDataExtracterApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class JsonConverterController : ControllerBase
    {       
        private string jsonFilePath;             
        private IConfiguration configuration;      
        private string csvFolder;
        private readonly IJsonConverterService<Artikel> _jsonConverterService;
        private readonly ICSVReaderService<Artikel> _csvReaderService;
        private readonly IDBService<Artikel> _dbService;
        private readonly FileDataExtracterDBContext _appDbContext;
        public JsonConverterController(IConfiguration configuration, IJsonConverterService<Artikel> jsonConverterService, ICSVReaderService<Artikel> csvReaderService,IDBService<Artikel> dbService, FileDataExtracterDBContext appDbContext)
        {
            this.configuration = configuration;
            jsonFilePath = configuration["JsonFilePath"];
            csvFolder = configuration["TargetFolder"];
            _jsonConverterService = jsonConverterService;
            _csvReaderService = csvReaderService;
            _dbService = dbService;
            _appDbContext = appDbContext;
        }
        /// <summary>
        /// This method reads the data from csv file and save it in json file in the configured directory
        /// </summary>
        /// <returns>returns the message as api response</returns>
        [HttpGet]
        public async Task<IActionResult> SaveJson()
        {                  
            var artikels = await _csvReaderService.ReadDataFromCSV(csvFolder+@"\"+getLatestFileNamefromDirectory());  

            bool isSaved = await _jsonConverterService.WriteDataToJson(artikels,jsonFilePath);
            if (isSaved)
                return Ok("json file saved successfully");
            else
                return Ok("unable to save json file");
        }        
        /// <summary>
        /// This method reads the data from csv file and saves to the database
        /// </summary>
        /// <returns>returns the message as api response</returns>
        public async Task<IActionResult> SaveData()
        {

            var artikels = await _csvReaderService.ReadDataFromCSV(csvFolder + @"\" + getLatestFileNamefromDirectory());

            bool isSaved = await _dbService.AddBulkDataAsync(artikels, _appDbContext);

            if (isSaved)
                return Ok("Saved Successfully");
            else
                return BadRequest("Unable to save data"); //this is very generic, need to show the error according to generated exception.
        }

        /// <summary>
        /// gets the latest file name from the directory
        /// </summary>
        /// <returns>latest csv file name</returns>
        private string getLatestFileNamefromDirectory()
        {
            string pattern = "*.csv";            
            var dirInfo = new DirectoryInfo(csvFolder);
            var file = (from f in dirInfo.GetFiles(pattern) orderby f.LastWriteTime descending select f).FirstOrDefault();
            return file.Name;

        }
    }
}
