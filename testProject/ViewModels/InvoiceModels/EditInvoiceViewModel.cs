using testProject.Models;

namespace testProject.ViewModels.InvoiceModels
{
    public class EditInvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Item> Items { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
