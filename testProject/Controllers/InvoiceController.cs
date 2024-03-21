using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using testProject.Models;
using testProject.Repositories;
using testProject.Repositories.CustomerRepository;
using testProject.Repositories.InvoiceRepository;
using testProject.Repositories.ItemRepository;
using testProject.ViewModels.InvoiceModels;
namespace testProject.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private IInvoiceCollection database = new InvoiceCollection();
        private ICustomerCollection customerCollection = new CustomerCollection();
        private IItemCollection itemCollection = new ItemCollection();
        private MongoDBRepository _repository = new MongoDBRepository();


        // GET: InvoiceController
        public ActionResult Index()
        {
            var invoices = database.GetAll();
            var viewModelList = new List<IndexInvoiceViewModel>();

            foreach (var invoice in invoices)
            {
                var customer = customerCollection.GetCustomerById(invoice.CustomerId);

                var items = new List<Item>();

                foreach (var invoiceItem in invoice.InvoiceItems)
                {
                    items.Add(itemCollection.GetItemById(invoiceItem.ItemId.ToString()));
                }

                var viewModel = new IndexInvoiceViewModel
                {
                    Invoice = invoice,
                    Customer = customer,
                    Items = items.ToList()
                };

                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(string id)
        {
            var invoice = database.GetInvoiceById(id);
            List<Item> items = new List<Item>();
            foreach (var invoiceItem in invoice.InvoiceItems)
            {
                items.Add(itemCollection.GetItemById(invoiceItem.ItemId));
            }
            var viewModel = new DetailsInvoiceViewModel { Invoice = invoice, Customers=customerCollection.GetCustomerById(invoice.CustomerId), Items=items };
            return View(viewModel);
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            var customers = customerCollection.GetAll();
            var items = itemCollection.GetAll();
            var invoices = database.GetAll();
            var highestInvoiceNo = invoices.Any() ? invoices.Max(i => i.InvoiceNo) : 0;
            var highestDate = invoices.Any() ? invoices.Max(i => i.InvoiceDate) : DateTime.Now;


            var model = new CreateInvoiceViewModel
            {
                Invoice = new Invoice()
                {
                    InvoiceNo = highestInvoiceNo + 1,
                },
                Customers = customers,
                Items = items,
                DateHolder = highestDate
            };
            return View(model);
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInvoiceViewModel model)
        {
            var invoiceList = database.GetAll();
            var invoiceNo = model.Invoice.InvoiceNo;

            var existingInvoice = invoiceList.Find(x=>x.InvoiceNo == invoiceNo);


            try
            {
                var invoice = new Invoice()
                {
                    InvoiceNo=model.Invoice.InvoiceNo,
                    InvoiceDate=model.DateHolder.AddDays(1),
                    CustomerId=model.Invoice.CustomerId.ToString(),
                    InvoiceItems=model.Invoice.InvoiceItems,
                    IsInvoicePaid=model.Invoice.IsInvoicePaid,
                    RemainingBalance=model.Invoice.TotalPrice-model.Invoice.PaidAmount,
                    PaidAmount=model.Invoice.PaidAmount,
                    TotalPrice=model.Invoice.TotalPrice,
                    TotalVat=model.Invoice.TotalVat,
                };
                if(existingInvoice == null ) {
                    database.InsertInvoice(invoice);
                    TempData["SuccessMessage"] = "Invoice created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["SuccessMessage"] = "Invoice is already existing in the database!";
                    return RedirectToAction(nameof(Create));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(string id)
        {
            var invoice = database.GetInvoiceById(id);
            var customers = customerCollection.GetAll();
            var items = itemCollection.GetAll();
            var invoiceDate = invoice.InvoiceDate;
            var model = new EditInvoiceViewModel
            {
                Invoice = invoice,
                Customers = customers,
                Items = items,
                InvoiceDate = invoiceDate,
            };
            return View(model);
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, EditInvoiceViewModel model)
        {

            try
            {
                var invoice = new Invoice()
                {
                    Id= new MongoDB.Bson.ObjectId(id),
                    InvoiceNo = model.Invoice.InvoiceNo,
                    InvoiceDate = model.InvoiceDate.AddDays(1),
                    CustomerId = model.Invoice.CustomerId.ToString(),
                    InvoiceItems = model.Invoice.InvoiceItems,
                    IsInvoicePaid = model.Invoice.IsInvoicePaid,
                    RemainingBalance = model.Invoice.RemainingBalance,
                    PaidAmount = model.Invoice.PaidAmount,
                    TotalPrice = model.Invoice.TotalPrice,
                    TotalVat = model.Invoice.TotalVat,
                };
                database.UpdateInvoice(invoice);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(string id)
        {
            var invoice = database.GetInvoiceById(id);
            List<Item> items = new List<Item>();
            foreach (var invoiceItem in invoice.InvoiceItems)
            {
                items.Add(itemCollection.GetItemById(invoiceItem.ItemId));
            }
            var viewModel = new DetailsInvoiceViewModel { Invoice = invoice, Customers = customerCollection.GetCustomerById(invoice.CustomerId), Items = items };
            return View(viewModel);
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, DeleteInvoiceViewModel model)
        {
            try
            {
                database.DeleteInvoice(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
