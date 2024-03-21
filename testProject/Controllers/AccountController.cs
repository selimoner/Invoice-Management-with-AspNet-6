using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testProject.Repositories.CustomerRepository;
using testProject.Repositories.InvoiceRepository;
using testProject.Repositories.ItemRepository;
using testProject.Repositories;
using testProject.ViewModels.AccountModels;
using testProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace testProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IInvoiceCollection database = new InvoiceCollection();
        private ICustomerCollection customerCollection = new CustomerCollection();
        private IItemCollection itemCollection = new ItemCollection();
        private MongoDBRepository _repository = new MongoDBRepository();
        // GET: AccountController
        public ActionResult Index()
        {
            var customers = customerCollection.GetAll();
            var model = new AccountViewModel()
            {
                Customers=customers
            };
            return View(model);
        }

        public ActionResult ListInvoices(AccountViewModel model)
        {
            var customerId = model.CustomerId;
            List<Invoice> invoices = database.GetCustomerAccount(customerId);
            model.Customer = customerCollection.GetCustomerById(customerId);
            if (invoices==null || invoices.Count == 0)
            {
                TempData["SuccessMessage"] = "No invoice of this customer found in database!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                model.Invoices= invoices;
                return View(model);
            }
        }
    }
}
