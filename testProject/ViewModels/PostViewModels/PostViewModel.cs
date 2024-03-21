using ApiProject.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace testProject.ViewModels.PostViewModels
{
    public class PostViewModel
    {

        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
        public IFormFile PostImage { get; set; }

    }
}
