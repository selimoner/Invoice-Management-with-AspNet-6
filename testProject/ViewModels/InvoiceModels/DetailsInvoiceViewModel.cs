using testProject.Models;

namespace testProject.ViewModels.InvoiceModels
{
    public class DetailsInvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public Customer Customers { get; set; }
        public List<Item> Items { get; set; }
    }
}
