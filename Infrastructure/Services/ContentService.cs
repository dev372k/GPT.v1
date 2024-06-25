using Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Services
{
    public class ContentService
    {
        private readonly IMongoCollection<Content> _content;
        public ContentService(IConfiguration _configuration)
        {
            var client = new MongoClient(_configuration.GetSection("MongoDB:ConnectionString").Value);
            var database = client.GetDatabase(_configuration.GetSection("MongoDB:DatabaseName").Value);
            _content = database.GetCollection<Content>("content");
        }


        public Content Create(Content content)
        {
            _content.InsertOne(content);
            return content;
        }

    }
}
