using MongoDB.Driver;

namespace testProject.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client;
        public IMongoDatabase database;
        public MongoDBRepository()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("TestDB");
        }
    }
}
