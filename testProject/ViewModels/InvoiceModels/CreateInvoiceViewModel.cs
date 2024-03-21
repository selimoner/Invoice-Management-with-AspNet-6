using MongoDB.Bson;
using testProject.Models;

namespace testProject.ViewModels.InvoiceModels
{
    public class CreateInvoiceViewModel
    {

      
        public Invoice Invoice { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Item> Items { get; set; }
        public DateTime DateHolder { get; set; }
    }
}
