using MongoDB.Bson;
using MongoDB.Driver;
using testProject.Models;

namespace testProject.Repositories.CustomerRepository
{
    public class CustomerCollection : ICustomerCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Customer> Collection;
        public CustomerCollection()
        {
            Collection = _repository.database.GetCollection<Customer>("Customers");
        }

        public void DeleteCustomer(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(s => s.Id, new ObjectId(id));
            Collection.DeleteOneAsync(filter);
        }

        public List<Customer> GetAll()
        {
            var query = Collection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }



        public Customer GetCustomerById(string id)
        {
            var customer = Collection.Find(
                new BsonDocument { { "_id", new ObjectId(id) } }
            ).FirstOrDefaultAsync().Result;

            return customer;
        }

        public void InsertCustomer(Customer customer)
        {
            Collection.InsertOneAsync(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(s => s.Id, customer.Id);
            Collection.ReplaceOneAsync(filter, customer);
        }
    }
}
