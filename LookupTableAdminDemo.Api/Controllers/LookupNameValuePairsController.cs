using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;

namespace LookupTableAdminDemo.Api.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class LookupNameValuePairsController : ControllerBase
    {
        private readonly ILogger<LookupNameValuePairsController> _logger;

        public LookupNameValuePairsController(ILogger<LookupNameValuePairsController> logger)
        {
            _logger = logger;
        }

        //ToDo: Get cnnstring from config file
        //ToDo: Deploy to Azure
        [HttpGet()]
        public IEnumerable<LookupNameValuePairEntity> Get()
        {
            var storageAccount = CloudStorageAccount.Parse("");

            var tableClient = storageAccount.CreateCloudTableClient();

            var _table = tableClient.GetTableReference("LookupNameValuePair");

            var query = new TableQuery<LookupNameValuePairEntity>();

            var entities = _table.ExecuteQuery(query);

            foreach (var entity in entities)
            {
                var x = entity.LookupKey;
            }

            return entities.ToArray();
        }
    }

    public class LookupNameValuePairEntity : TableEntity
    {
        public string LookupKey { get; set; }

        public string Value { get; set; }

    }

}