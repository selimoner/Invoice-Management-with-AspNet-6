using testProject.Models;

namespace testProject.ViewModels.InvoiceModels
{
    public class IndexInvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public Customer Customer { get; set; }
        public List<Item> Items { get; set; }
    }
}
