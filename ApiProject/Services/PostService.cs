using ApiProject.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiProject.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoCollection<Post> _posts;

        public PostService(IPostSystemDatabaseSettings settings, IMongoClient mongoClient) {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _posts = database.GetCollection<Post>(settings.PostCollectionName);
        }
        public void DeletePostById(string id)
        {
            _posts.DeleteOneAsync(post=>post.Id.ToString()==id);
        }

        public List<Post> GetAll()
        {
            return _posts.Find(post=>true).ToList();
        }

        public Post GetPostById(string id)
        {
            return _posts.Find(post => post.Id==id).FirstOrDefault();
        }

        public async Task<Post> InsertPostAsync(Post post)
        {
            post.Id= ObjectId.GenerateNewId().ToString();
            await _posts.InsertOneAsync(post);
            return post;
        }

        public void UpdatePost(string id, Post post)
        {
            _posts.ReplaceOneAsync(post => post.Id.ToString() == id, post);
        }
    }
}
