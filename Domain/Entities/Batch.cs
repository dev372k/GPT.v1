using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Batch
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FileId { get; set; } = string.Empty;
        public string OutputFileId { get; set; } = string.Empty;

        public string BatchId { get; set; } = string.Empty;

        public bool IsProcessed { get; set; } = false;
    }
}
