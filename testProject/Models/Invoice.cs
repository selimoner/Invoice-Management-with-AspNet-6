using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace testProject.Models
{
    public class Invoice
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalVat { get; set; }
        public int InvoiceNo { get; set; }
        public bool? IsInvoicePaid { get; set; }
        public decimal? RemainingBalance { get; set; }
        public decimal? PaidAmount { get; set; }
        public string CustomerId { get; set; }
        public List<InvoiceItemModel> InvoiceItems { get; set; }
        public Invoice()
        {
            IsInvoicePaid = false;
        }
    }
}
