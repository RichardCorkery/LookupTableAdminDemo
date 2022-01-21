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
        private readonly IConfiguration _configuration;
        private readonly CloudTable _table;

        public LookupNameValuePairsController(ILogger<LookupNameValuePairsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            var storageAccount = CloudStorageAccount.Parse(_configuration["Data:AzureStorageDemos:ConnectionString"]);

            var tableClient = storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference("LookupNameValuePair");
        }

        // ToDo: Add api
        // GET: api/vpb-delegates
        
        //ToDo: Deploy to Azure
        //ToDo: Delete 
        //ToDo: Create
        //ToDo: Update/Replace

        //ToDo: Confirm Delete
        //ToDo: Split up Controller to: Controller and Repo
        //ToDo: Set up DI

        // GET: api/lookupnamevaluepairs
        [HttpGet()]
        public IEnumerable<LookupNameValuePairEntity> Get()
        {


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

            var operation = TableOperation.Retrieve<LookupNameValuePairEntity>(partitionKey, rowKey);

            var result = _table.Execute(operation);

            //return result.Result as TodoEntity;
        }

        // POST api/lookupnamevaluepairs
        [HttpPost]
        public void Post(LookupNameValuePairEntity model)
        {
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