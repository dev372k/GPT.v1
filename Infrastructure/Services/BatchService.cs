using Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Services
{
    public class BatchService
    {
        private readonly IMongoCollection<Batch> _batch;
        public BatchService(IConfiguration _configuration)
        {
            var client = new MongoClient(_configuration.GetSection("MongoDB:ConnectionString").Value);
            var database = client.GetDatabase(_configuration.GetSection("MongoDB:DatabaseName").Value);
            _batch = database.GetCollection<Batch>("batch");
        }

        public List<Batch> Get()
        {
            List<Batch> batches;
            batches = _batch.Find(_ => !_.IsProcessed).ToList();
            return batches;
        }

        public Batch Get(string id) =>
            _batch.Find<Batch>(emp => emp.Id == id).FirstOrDefault();

        public Batch Create(Batch batch)
        {
            _batch.InsertOne(batch);
            return batch;
        }

        public void Update(string id, Batch batchIn)
        {
            _batch.ReplaceOne(batch => batch.Id == id, batchIn);
        }
    }
}
