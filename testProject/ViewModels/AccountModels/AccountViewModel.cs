using testProject.Models;

namespace testProject.ViewModels.AccountModels
{
    public class AccountViewModel
    {
        public string CustomerId { get; set; }  
        public List<Customer> Customers { get; set; }
        public List<Invoice> Invoices { get; set; }
        public Customer Customer { get; set; }
    }
}
