using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace testProject.Models
{
    public class Item
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
    }
}
