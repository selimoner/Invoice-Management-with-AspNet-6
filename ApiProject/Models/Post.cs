using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiProject.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("PublishDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime PublishDate { get; set; }
    }
}
