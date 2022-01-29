using LookupTableAdminDemo.Api.Entities;
using LookupTableAdminDemo.Api.Models;
using LookupTableAdminDemo.Api.Repositories;
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

        //ToDo: Create repo
        //ToDo: Split up Controller to: Controller and Repo

        //ToDo: Deploy to Azure

        //ToDo: Review PS ToDo App
        //ToDo: Confirm Delete
        //ToDo: Set up DI
        //ToDo: Review Controller of the function app I did

        // GET: api/lookupnamevaluepairs
        [HttpGet]
        public IEnumerable<LookupNameValuePairModel> Get()
        {
            var cnnStr = _configuration["Data:AzureStorageDemos:ConnectionString"];

            var repository = new LookupNameValuePairRepository(cnnStr);

            var entities = repository.All();

            var models = entities.Select(x => new LookupNameValuePairModel
            {
                RowKey = x.RowKey,
                PartitionKey = x.PartitionKey,
                LookupKey = x.LookupKey,
                Value = x.Value
            });

            return models.ToArray();
        }

        //// GET: api/lookupnamevaluepairs/00000
        //[HttpGet("partitionKey, rowKey")]
        //public LookupNameValuePairModel Get(string partitionKey, string rowKey)
        //{

        //    var operation = TableOperation.Retrieve<LookupNameValuePairModel>(partitionKey, rowKey);

        //    var result = _table.Execute(operation);

        //    return result.Result as LookupNameValuePairModel;
        //}

        //// POST api/lookupnamevaluepairs
        //[HttpPost]
        //public void Post(LookupNameValuePairModel model)
        //{
        //    var entity = model;
            

        //    var operation = TableOperation.Insert(entity);

        //    //$$$RAC: Note post and put could be combined
        //    //var operation = TableOperation.InsertOrReplace(entity);

        //    _table.Execute(operation);
        //}

        //// PUT api/lookupnamevaluepairs/???
        //[HttpPut]
        //public void Put(LookupNameValuePairModel model)
        //{

        //    var operation = TableOperation.Retrieve<LookupNameValuePairModel>(model.PartitionKey, model.RowKey);

        //    var result = _table.Execute(operation);

        //    var entity = result.Result as LookupNameValuePairModel;
            
        //    entity.LookupKey = model.LookupKey;
        //    entity.Value = model.Value;
            
        //    var operation2 = TableOperation.Replace(entity);

        //    _table.Execute(operation2);
        //}

        //// PUT api/lookupnamevaluepairs/???
        //[HttpDelete]
        //public void Delete(string partitionKey, string rowKey)
        //{
        //    var operation = TableOperation.Retrieve<LookupNameValuePairModel>(partitionKey, rowKey);

        //    var result = _table.Execute(operation);

        //    var entity = result.Result as LookupNameValuePairModel;

        //    var operation2 = TableOperation.Delete(entity);

        //    _table.Execute(operation2);
        //}
    }
}