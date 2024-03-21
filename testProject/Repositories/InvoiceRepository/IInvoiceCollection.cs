using testProject.Models;

namespace testProject.Repositories.InvoiceRepository
{
    public interface IInvoiceCollection
    {
        void InsertInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
        void DeleteInvoice(string id);
        List<Invoice> GetAll();
        Invoice GetInvoiceById(string id);
        List<Invoice> GetCustomerAccount(string customerId);
    }
}
