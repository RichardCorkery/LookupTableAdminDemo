using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;

namespace LookupTableAdminDemo.Api.Controllers
{
    //ToDo: Constants
    [Route("api/[controller]")]
    [ApiController]
    public class LookupNameValuePairsController : ControllerBase
    {
        private readonly ILogger<LookupNameValuePairsController> _logger;

        public LookupNameValuePairsController(ILogger<LookupNameValuePairsController> logger)
        {
            _logger = logger;
        }

        // ToDo: Add api
        // GET: api/vpb-delegates

        //ToDo: Get cnnstring from config file
        //ToDo: Deploy to Azure
        //ToDo: Delete 
        //ToDo: Create
        //ToDo: Update/Replace

        //ToDo: Confirm Delete
        //ToDo: Split up Controller to: Controller and Repo

        // GET: api/lookupnamevaluepairs
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

        // GET: api/lookupnamevaluepairs/00000
        [HttpGet("partitionKey, rowKey")]
        public void Get(string partitionKey, string rowKey)
        {
            var storageAccount = CloudStorageAccount.Parse("");

            var tableClient = storageAccount.CreateCloudTableClient();

            var _table = tableClient.GetTableReference("LookupNameValuePair");

            var operation = TableOperation.Retrieve<LookupNameValuePairEntity>(partitionKey, rowKey);

            var result = _table.Execute(operation);

            //return result.Result as TodoEntity;
        }

        // POST api/lookupnamevaluepairs
        [HttpPost]
        public void Post(LookupNameValuePairEntity model)
        {
            var storageAccount = CloudStorageAccount.Parse("");

            var tableClient = storageAccount.CreateCloudTableClient();

            var _table = tableClient.GetTableReference("LookupNameValuePair");

            var entity = model;

            var operation = TableOperation.InsertOrReplace(entity);

            _table.Execute(operation);
        }

    }

    public class LookupNameValuePairEntity : TableEntity
    {
        public string LookupKey { get; set; }

        public string Value { get; set; }

    }

}