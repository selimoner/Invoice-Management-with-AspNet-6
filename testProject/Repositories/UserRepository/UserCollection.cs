using MongoDB.Bson;
using MongoDB.Driver;
using testProject.Models;

namespace testProject.Repositories.UserRepository
{
    //public class UserCollection : IUserCollection
    //{
    //    internal AppDbContext _repository = new AppDbContext();
    //    private IMongoCollection<User> Collection;
    //    public UserCollection() { 
    //        Collection= _repository.database.GetCollection<User>("Users");
    //    }
    //    public void DeleteUser(string id)
    //    {
    //        var filter = Builders<User>.Filter.Eq(s => s.Id, new ObjectId(id));
    //        Collection.DeleteOneAsync(filter);
    //    }

    //    public List<User> GetAll()
    //    {
    //        var query = Collection.Find(new BsonDocument()).ToListAsync();
    //        return query.Result;
    //    }

    //    public User GetUserById(string id)
    //    {
    //        var user = Collection.Find(
    //            new BsonDocument { { "_id", new ObjectId(id) } }
    //        ).FirstOrDefaultAsync().Result;

    //        return user;
    //    }

    //    public void InsertUser(User user)
    //    {
    //        Collection.InsertOneAsync(user);
    //    }

    //    public void UpdateUser(User user)
    //    {
    //        var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
    //        Collection.ReplaceOneAsync(filter, user);
    //    }

    //    public User GetUserByUsername(string username)
    //    {
    //        var filter = Builders<User>.Filter.Eq(u => u.Username, username);
    //        var user = Collection.Find(filter).FirstOrDefaultAsync().Result;

    //        return user;
    //    }
    //}
}
