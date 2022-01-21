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
        
        // GET: api/vpb-delegates

        //ToDo: Deploy to Azure

        //ToDo: Review PS ToDo App
        //ToDo: Confirm Delete
        //ToDo: Split up Controller to: Controller and Repo
        //ToDo: Set up DI
        //ToDo: Review Controller of the function app I did

        // GET: api/lookupnamevaluepairs
        [HttpGet()]
        public IEnumerable<LookupNameValuePairEntity> Get()
        {
            var query = new TableQuery<LookupNameValuePairEntity>();

            var entities = _table.ExecuteQuery(query);

            return entities.ToArray();
        }

        // GET: api/lookupnamevaluepairs/00000
        [HttpGet("partitionKey, rowKey")]
        public LookupNameValuePairEntity Get(string partitionKey, string rowKey)
        {

            var operation = TableOperation.Retrieve<LookupNameValuePairEntity>(partitionKey, rowKey);

            var result = _table.Execute(operation);

            return result.Result as LookupNameValuePairEntity;
        }

        // POST api/lookupnamevaluepairs
        [HttpPost]
        public void Post(LookupNameValuePairEntity model)
        {
            var entity = model;
            

            var operation = TableOperation.Insert(entity);

            //$$$RAC: Note post and put could be combined
            //var operation = TableOperation.InsertOrReplace(entity);

            _table.Execute(operation);
        }

        // PUT api/lookupnamevaluepairs/???
        [HttpPut]
        public void Put(LookupNameValuePairEntity model)
        {

            var operation = TableOperation.Retrieve<LookupNameValuePairEntity>(model.PartitionKey, model.RowKey);

            var result = _table.Execute(operation);

            var entity = result.Result as LookupNameValuePairEntity;
            
            entity.LookupKey = model.LookupKey;
            entity.Value = model.Value;
            
            var operation2 = TableOperation.Replace(entity);

            _table.Execute(operation2);
        }

        // PUT api/lookupnamevaluepairs/???
        [HttpDelete]
        public void Delete(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<LookupNameValuePairEntity>(partitionKey, rowKey);

            var result = _table.Execute(operation);

            var entity = result.Result as LookupNameValuePairEntity;

            var operation2 = TableOperation.Delete(entity);

            _table.Execute(operation2);
        }

    }

    public class LookupNameValuePairEntity : TableEntity
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string LookupKey { get; set; }

        public string Value { get; set; }

    }

}