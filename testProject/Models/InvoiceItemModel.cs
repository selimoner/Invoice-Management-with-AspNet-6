using MongoDB.Bson;

namespace testProject.Models
{
    public class InvoiceItemModel
    {

        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
