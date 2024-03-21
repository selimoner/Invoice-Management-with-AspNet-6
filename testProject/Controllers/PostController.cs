using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using testProject.Services;
using ApiProject.Models;
using testProject.ViewModels.PostViewModels;
using MongoDB.Bson;
using testProject.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using testProject.Helper;
using Newtonsoft.Json;


namespace testProject.Controllers
{
    [Authorize]
    [Controller]
    public class PostController : Controller
    {
        private readonly IPostApiService _postApiService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostController(IPostApiService postApiService, IWebHostEnvironment webHostEnvironment)
        {
            _postApiService = postApiService;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: PostController
        public async Task<IActionResult> Index()
        {
            var posts = await _postApiService.GetAllPostsAsync();


            List<PostViewModel> postViewModels = new List<PostViewModel>();

            foreach (var item in posts)
            {
                for (int i = 0; i < 10; i++)
                {
                    postViewModels.Add(new PostViewModel { Id = item.Id, Title = item.Title, Description = item.Description, ImageUrl = item.ImageUrl, PublishDate = item.PublishDate });
                }
            }
            int postCount = postViewModels.Count();
            this.ViewBag.PostCount = postCount;
            return View(postViewModels);
        }

        // GET: PostController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var post = await _postApiService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            try
            {
                var viewModel = new PostViewModel()
                {
                    Id = id.ToString(),
                    Description = post.Description,
                    ImageUrl = post.ImageUrl,
                    PublishDate = post.PublishDate,
                    Title = post.Title,
                };
                return View(viewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            var post = new PostViewModel()
            {
                PublishDate=DateTime.Now,
            };
            return View(post);
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel viewModel)
        {
            string uniqueFileName = UploadedFile(viewModel);
            try
            {
                var post = new Post()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Description = viewModel.Description,
                    PublishDate= DateTime.Now,
                    Title= viewModel.Title,
                    ImageUrl=uniqueFileName
                };
                await _postApiService.CreatePostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: PostController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var post = await _postApiService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            try
            {
                var viewModel = new PostViewModel()
                {
                    Id = id.ToString(),
                    Description = post.Description,
                    ImageUrl = post.ImageUrl,
                    PublishDate = post.PublishDate,
                    Title = post.Title,

                };
                return View(viewModel);
            }
            catch
            {
                return View();
            }
            
        }
        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, PostViewModel viewModel)
        {
            var post2 = await _postApiService.GetPostByIdAsync(id);
            string uniqueFileName;

            if (viewModel.PostImage != null)
            {
                uniqueFileName = UploadedFile(viewModel);
            }
            else
            {
                uniqueFileName = post2.ImageUrl;
            }
            try
            {
                var post = new Post()
                {
                    Id=id,
                    Description= viewModel.Description,
                    ImageUrl=uniqueFileName,
                    PublishDate=viewModel.PublishDate,
                    Title=viewModel.Title,
                };
                await _postApiService.UpdatePostAsync(id, post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }

            
        }
        // GET: PostController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var post = await _postApiService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            try
            {
                var viewModel = new PostViewModel()
                {
                    Id = id.ToString(),
                    Description = post.Description,
                    ImageUrl = post.ImageUrl,
                    PublishDate = post.PublishDate,
                    Title = post.Title,
                };
                return View(viewModel);
            }
            catch
            {
                return View();
            }
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, PostViewModel model)
        {
            await _postApiService.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private string UploadedFile(PostViewModel post)
        {
            string uniqueFileName = null;
            if (post.PostImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + post.PostImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    post.PostImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<ActionResult> GetPagedData(int pageNumber = 1, int pageSize = 5)
        {
            var posts = await _postApiService.GetAllPostsAsync();

            List<PostViewModel> postViewModels = new List<PostViewModel>();
            for (int i = 0; i < 10; i++)
            {
                foreach (var item in posts)
            {
                
                    postViewModels.Add(new PostViewModel { Id = item.Id, Title = item.Title, Description = item.Description, ImageUrl = item.ImageUrl, PublishDate = item.PublishDate });
                
            }
            }
            var pagedData = Pagination.PagedResult(postViewModels, pageNumber, pageSize);
            return Json(pagedData);
        }


        // GET: PostController
        public async Task<IActionResult> IndexMvcPage(int pg = 1)
        {
            var posts = await _postApiService.GetAllPostsAsync();


            List<PostViewModel> postViewModels = new List<PostViewModel>();
            for (int i = 0; i < 10; i++)
            {
                foreach (var item in posts)
            {
                
                    postViewModels.Add(new PostViewModel { Id = item.Id, Title = item.Title, Description = item.Description, ImageUrl = item.ImageUrl, PublishDate = item.PublishDate });
                
            }
            }
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int postCount = postViewModels.Count();
            var pager = new Pager(postCount, pg, pageSize);
            int postSkip = (pg - 1) * pageSize;

            var data = postViewModels.Skip(postSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }
    }
}