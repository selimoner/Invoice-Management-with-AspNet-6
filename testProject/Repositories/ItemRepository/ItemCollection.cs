using MongoDB.Bson;
using MongoDB.Driver;
using testProject.Models;

namespace testProject.Repositories.ItemRepository
{
    public class ItemCollection : IItemCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Item> Collection;
        public ItemCollection()
        {
            Collection = _repository.database.GetCollection<Item>("Items");
        }
        public void DeleteItem(string id)
        {
            var filter = Builders<Item>.Filter.Eq(s => s.Id, new ObjectId(id));
            Collection.DeleteOneAsync(filter);
        }

        public List<Item> GetAll()
        {
            var query = Collection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }

        public Item GetItemById(string id)
        {
            var item = Collection.Find(
                new BsonDocument { { "_id", new ObjectId(id) } }
            ).FirstOrDefaultAsync().Result;

            return item;
        }

        public void InsertItem(Item item)
        {
            Collection.InsertOneAsync(item);
        }

        public void UpdateItem(Item item)
        {
            var filter = Builders<Item>.Filter.Eq(s => s.Id, item.Id);
            Collection.ReplaceOneAsync(filter, item);
        }
    }
}
