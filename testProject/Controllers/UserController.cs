using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using testProject.Models;
using testProject.Repositories.UserRepository;
using testProject.ViewModels.AuthenticationViewModels;
using testProject.ViewModels.ItemModels;

namespace testProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserCollection _userCollection;

        public UserController(IUserCollection userCollection)
        {
            _userCollection = userCollection;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var users =await _userCollection.GetAll();
            var viewModelList = users.Select(c => new LoginViewModel { User = c }).ToList();
            return View(viewModelList);
        }

        // GET: UserController/Details/5
        public ActionResult Details(string id)
        {
            var user = _userCollection.GetUserById(id);
            var viewModel = new LoginViewModel { User = user };
            return View(viewModel);
        }

        // GET: UserController/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create(LoginViewModel model)
        {
            var objectId = ObjectId.GenerateNewId().ToString();
            try
            {
                var user = new User()
                {
                    Id =objectId,
                    Username =model.User.Username,
                    Surname=model.User.Surname,
                    EmailAddress=model.User.EmailAddress,
                    Name=model.User.Name,
                    Password=model.User.Password,
                };
                _userCollection.InsertUser(user);
                HttpContext.SignOutAsync();
                TempData["SuccessMessage"] = "User created successfully! Please log in.";
                return RedirectToAction("Login", "Access");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(string id)
        {
            var user = _userCollection.GetUserById(id);
            var viewModel = new LoginViewModel { User=user };
            return View(viewModel);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, LoginViewModel model)
        {
            try
            {
                var user = new User()
                {
                    Id = new MongoDB.Bson.ObjectId(id).ToString(),
                    Username = model.User.Username,
                    Surname = model.User.Surname,
                    EmailAddress = model.User.EmailAddress,
                    Name = model.User.Name,
                    Password = model.User.Password,
                };
                _userCollection.UpdateUser(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(string id)
        {
            var user = _userCollection.GetUserById(id);
            var viewModel = new LoginViewModel { User = user };
            return View(viewModel);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, LoginViewModel model)
        {
            try
            {
                _userCollection.DeleteUser(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
