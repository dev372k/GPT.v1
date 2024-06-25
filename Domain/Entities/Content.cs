using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Entities
{
    public class Content
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string response_id { get; set; }
        public string custom_id { get; set; }
        public string request_id { get; set; }
        public string content { get; set; }
    }
}
