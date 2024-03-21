using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace testProject.Models
{
    public class Customer
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTinNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerContactNumber1 { get; set; }
        public string? CustomerContactNumber2 { get; set; }
    }
}
