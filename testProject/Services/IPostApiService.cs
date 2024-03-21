using Microsoft.Extensions.Hosting;
using ApiProject.Models;
namespace testProject.Services
{
    public interface IPostApiService
    {
        Task<Post> GetPostByIdAsync(string postId);
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(string postId, Post updatedPost);
        Task<bool> DeletePostAsync(string postId);
    }
}
