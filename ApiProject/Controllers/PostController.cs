using ApiProject.Models;
using ApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService=postService;
        }


        // GET: api/<PostController>
        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            return postService.GetAll();
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public ActionResult<Post> Get(string id)
        {
            var post = postService.GetPostById(id);
            if (post == null)
            {
                return NotFound($"Post with ID = {id} not found");
            }
            return post;
        }

        // POST api/<PostController>
        [HttpPost]
        public ActionResult<Post> Post([FromBody] Post post)
        {
            // Insert the post into MongoDB
            postService.InsertPostAsync(post);

            // Return a 201 Created response with the location header pointing to the newly created resource
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Post post)
        {
            var existingPost = postService.GetPostById(id);
            if (existingPost == null)
            {
                return NotFound($"Post with ID = {id} not found");
            }
            postService.UpdatePost(id, post);
            return NoContent();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteConfirm(string id)
        {
            var existingPost = postService.GetPostById(id);
            if (existingPost == null)
            {
                return NotFound($"Post with ID = {id} not found");
            }
            postService.DeletePostById(existingPost.Id.ToString());
            return Ok($"Post with Id = {id} deleted");
        }
    }
}
