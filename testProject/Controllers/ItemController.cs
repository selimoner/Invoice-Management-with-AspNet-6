using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testProject.Models;
using testProject.Repositories.ItemRepository;
using testProject.ViewModels.ItemModels;

namespace testProject.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private IItemCollection database = new ItemCollection();

        // GET: ItemController
        public ActionResult Index()
        {
            var items = database.GetAll();
            var viewModelList = items.Select(c => new IndexItemViewModel { Item = c }).ToList();
            return View(viewModelList);
        }

        // GET: ItemController/Details/5
        public ActionResult Details(string id)
        {
            var item = database.GetItemById(id);
            var viewModel = new DetailsItemViewModel { Item = item };
            return View(viewModel);
        }

        // GET: ItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateItemViewModel model)
        {
            var itemList = database.GetAll();
            var itemCode = model.Item.ItemCode;
            var existingItem = itemList.Find(x=>x.ItemCode == itemCode);
            try
            {
                var item = new Item()
                {
                    ItemCode=model.Item.ItemCode,
                    ItemDescription=model.Item.ItemDescription
                };
                if (existingItem == null)
                {
                    database.InsertItem(item);
                    TempData["SuccessMessage"] = "Item created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["SuccessMessage"] = "Item is already existing in the database!";
                    return RedirectToAction(nameof(Create));
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Edit/5
        public ActionResult Edit(string id)
        {
            var item = database.GetItemById(id);
            var viewModel=new EditItemViewModel { Item = item };
            return View(viewModel);
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, EditItemViewModel model)
        {
            try
            {
                var item = new Item()
                {
                    Id=new MongoDB.Bson.ObjectId(id),
                    ItemCode=model.Item.ItemCode,
                    ItemDescription=model.Item.ItemDescription
                };
                database.UpdateItem(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Delete/5
        public ActionResult Delete(string id)
        {
            var item = database.GetItemById(id);
            var viewModel = new DeleteItemViewModel { Item = item };
            return View(viewModel);
        }

        // POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, DeleteItemViewModel model)
        {
            try
            {
                database.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
