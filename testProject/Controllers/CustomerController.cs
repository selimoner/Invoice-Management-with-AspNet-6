using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using testProject.Models;
using testProject.Repositories.CustomerRepository;
using testProject.ViewModels.CustomerModels;

namespace testProject.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ICustomerCollection database = new CustomerCollection();

        // GET: CustomerController
        public ActionResult Index()
        {
            var customers = database.GetAll();
            var viewModelList = customers.Select(c => new IndexCustomerViewModel { Customer = c }).ToList();
            return View(viewModelList);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(string id)
        {
            var customer = database.GetCustomerById(id);
            var viewModel = new DetailsCustomerViewModel { Customer = customer };
            return View(viewModel);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCustomerViewModel model)
        {
            var customerList = database.GetAll();
            var customerTinNumber = model.Customer.CustomerTinNumber;
            var existingCustomer = customerList.Find(x=>x.CustomerTinNumber == customerTinNumber);
            try
            {
                var customer = new Customer()
                {
                    CustomerName = model.Customer.CustomerName,
                    CustomerTinNumber=model.Customer.CustomerTinNumber,
                    CustomerAddress = model.Customer.CustomerAddress,
                    CustomerContactNumber1= model.Customer.CustomerContactNumber1,
                    CustomerContactNumber2= model.Customer.CustomerContactNumber2,
                    CustomerEmail= model.Customer.CustomerEmail,
                };
                if (existingCustomer == null)
                {
                    database.InsertCustomer(customer);
                    TempData["SuccessMessage"] = "Customer created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                {
                    TempData["SuccessMessage"] = "Customer is already existing in the database!";
                    return RedirectToAction(nameof(Create));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(string id)
        {
            var customer = database.GetCustomerById(id);
            var viewModel = new EditCustomerViewModel { Customer = customer };
            return View(viewModel);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, EditCustomerViewModel model)
        {
            try
            {
                var customer = new Customer()
                {
                    Id=new MongoDB.Bson.ObjectId(id),
                    CustomerName=model.Customer.CustomerName,
                    CustomerTinNumber=model.Customer.CustomerTinNumber,
                    CustomerAddress=model.Customer.CustomerAddress,
                    CustomerContactNumber1=model.Customer.CustomerContactNumber1,
                    CustomerContactNumber2=model.Customer.CustomerContactNumber2,
                    CustomerEmail= model.Customer.CustomerEmail
                };
                database.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(string id)
        {
            var customer=database.GetCustomerById(id);
            var viewModel = new DeleteCustomerViewModel { Customer = customer };
            return View(viewModel);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, DeleteCustomerViewModel model)
        {
            try
            {
                database.DeleteCustomer(id); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
