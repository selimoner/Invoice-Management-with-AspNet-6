using MongoDB.Bson;
using MongoDB.Driver;
using testProject.Models;

namespace testProject.Repositories.UserRepository
{
    public class MongoUserRepository:IUserCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<User> Collection;
        public MongoUserRepository()
        {
            Collection = _repository.database.GetCollection<User>("Users");
        }
        public void DeleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq(s => s.Id, new ObjectId(id).ToString());
            Collection.DeleteOneAsync(filter);
        }

        public async Task<List<User>> GetAll()
        {
            //Id = new MongoDB.Bson.ObjectId(id).ToString(),
            var query = await Collection.Find(new BsonDocument()).ToListAsync();
            return query;
        }

        public User GetUserById(string id)
        {
            var user = Collection.Find(
                new BsonDocument { { "_id", new ObjectId(id).ToString() } }
            ).FirstOrDefaultAsync().Result;

            return user;
        }

        public void InsertUser(User user)
        {
            Collection.InsertOneAsync(user);
        }

        public void UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
            Collection.ReplaceOneAsync(filter, user);
        }

        public User GetUserByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            var user = Collection.Find(filter).FirstOrDefaultAsync().Result;

            return user;
        }
    }
}
