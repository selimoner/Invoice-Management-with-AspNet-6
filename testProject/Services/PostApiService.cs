using ApiProject.Models;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Text;

namespace testProject.Services
{
    public static partial class JsonExtensions
    {
        public static IEnumerable<T> FromDelimitedJson<T>(TextReader reader, JsonSerializerSettings settings = null)
        {
            using (var jsonReader = new JsonTextReader(reader) { CloseInput = false, SupportMultipleContent = true })
            {
                var serializer = JsonSerializer.CreateDefault(settings);

                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.Comment)
                        continue;
                    yield return serializer.Deserialize<T>(jsonReader);
                }
            }
        }
    }
    public class PostApiService : IPostApiService
    {
        private readonly HttpClient _httpClient;

        public PostApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Set the base URL of your API
            _httpClient.BaseAddress = new Uri("https://localhost:7150/");
        }

        public async Task<Post> GetPostByIdAsync(string postId)
        {
            var response = await _httpClient.GetAsync($"api/Post/{postId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Check if the content is a JSON array or a single object
                if (content.StartsWith("["))
                {
                    var posts = JsonConvert.DeserializeObject<List<Post>>(content);

                    // Find the post with the matching Id
                    var foundPost = posts.FirstOrDefault(post => post.Id == postId);

                    return foundPost;
                }
                else
                {
                    // Content is a single object, deserialize it directly
                    return JsonConvert.DeserializeObject<Post>(content);
                }
            }

            // Handle error or return null
            return null;
        }


        public async Task<List<Post>> GetAllPostsAsync()
        {
            var response = await _httpClient.GetAsync("api/Post");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Post>>(content);
            }

            // Handle error or return an empty list
            return new List<Post>();
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            var postJson = JsonConvert.SerializeObject(post);
            var content = new StringContent(postJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Post", content);

            if (response.IsSuccessStatusCode)
            {
                var createdPostJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Post>(createdPostJson);
            }

            // Handle error or return null
            return null;
        }

        public async Task<bool> UpdatePostAsync(string postId, Post updatedPost)
        {
            var postJson = JsonConvert.SerializeObject(updatedPost);
            var content = new StringContent(postJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Post/{postId}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePostAsync(string postId)
        {
            var response = await _httpClient.DeleteAsync($"api/Post/{postId}");

            return response.IsSuccessStatusCode;
        }
    }
}
