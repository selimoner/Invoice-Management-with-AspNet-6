using MongoDB.Bson;
using MongoDB.Driver;
using testProject.Models;

namespace testProject.Repositories.InvoiceRepository
{
    public class InvoiceCollection : IInvoiceCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Invoice> Collection;
        private IMongoCollection<Customer> CustomersCollection;
        public InvoiceCollection()
        {
            Collection = _repository.database.GetCollection<Invoice>("Invoices");
            CustomersCollection = _repository.database.GetCollection<Customer>("Customers");
        }

        public void DeleteInvoice(string id)
        {
            var filter = Builders<Invoice>.Filter.Eq(s => s.Id, new ObjectId(id));
            Collection.DeleteOneAsync(filter);
        }

        public List<Invoice> GetAll()
        {
            var query = Collection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }

        public Invoice GetInvoiceById(string id)
        {
            var invoice = Collection.Find(
                new BsonDocument { { "_id", new ObjectId(id) } }
            ).FirstAsync().Result;

            return invoice;
        }

        public void InsertInvoice(Invoice invoice)
        {
            Collection.InsertOneAsync(invoice);

        }

        public void UpdateInvoice(Invoice invoice)
        {
            var filter = Builders<Invoice>.Filter.Eq(s => s.Id, invoice.Id);
            Collection.ReplaceOneAsync(filter, invoice);
        }
        public List<Invoice> GetCustomerAccount(string customerId)
        {
            var query = Collection.Find(x=>x.CustomerId==customerId).ToList();
            return query;
        }

    }
}
