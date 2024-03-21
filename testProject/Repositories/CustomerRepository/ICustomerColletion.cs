using testProject.Models;

namespace testProject.Repositories.CustomerRepository
{
    public interface ICustomerCollection
    {
        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(string id);
        List<Customer> GetAll();
        Customer GetCustomerById(string id);
    }
}
