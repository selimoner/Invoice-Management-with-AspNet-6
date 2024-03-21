using ApiProject.Models;

namespace ApiProject.Services
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post GetPostById(string id);
        Task<Post> InsertPostAsync(Post post);
        void UpdatePost(string id, Post post);
        void DeletePostById(string id);
    }
}
