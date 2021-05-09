using FifaFinderAPI.Library.Binding;
using FifaFinderAPI.Library.Data;
using FifaFinderAPI.Library.Interfaces;
using FifaFinderAPI.Library.Models;
using FifaFinderAPI.Library.Models.Binding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifaFinderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private ILogger<PostController> _logger;
        private IRepositoryWrapper repository;
        public PostController(ILogger<PostController> logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            repository = repositoryWrapper;
        }

        // GET Request which returns the entire Post table
        [HttpGet]
        public JsonResult GetPosts()
        {
            var allPosts = repository.Posts.FindAll();
            if (allPosts != null)
            {
                _logger.LogInformation("Posts returned");
                return new JsonResult(allPosts);
            }
            _logger.LogInformation("Posts not returned, null value");
            return new JsonResult("Table not found");

        }

        // POST Request which adds a record to the Post table on success, returning feedback of success 
        [HttpPost("{userID}")]
        public JsonResult AddPost(AddPost addPostModel, int userID)
        {
            var user = repository.Users.FindByCondition(u => u.ID == userID).FirstOrDefault();
            var postToInsert = repository.Posts.Create(new Post { User = null, Type = addPostModel.Type, Platform = addPostModel.Platform, Position = addPostModel.Position, PlayerRating = addPostModel.PlayerRating, Description = addPostModel.Description, CreatedAt = DateTime.Now});
            repository.Save();

            _logger.LogInformation("Post created");
            return new JsonResult("Post Created!");
        }

        // DELETE Request which deletes rthe record specified from the id parameter
        [HttpDelete("{id}")]
        public JsonResult DeletePost(int id)
        {
            var postToDelete = repository.Posts.FindByCondition(p => p.ID == id).FirstOrDefault();
            if (postToDelete == null)
            {
                _logger.LogInformation("Post ID not found");
                return new JsonResult("No user to delete");
            }
            repository.Posts.Delete(postToDelete);
            repository.Save();
            _logger.LogInformation("Deletion complete");
            return new JsonResult("Post Deleted!");
        }

        // PUT Request which edits an existing record with the passed in new details, returns feedback of success
        [HttpPut]
        public JsonResult UpdatePost(EditPost post)
        {
            var postToUpdate = repository.Posts.FindByCondition(p => p.ID == post.ID).FirstOrDefault();
            if (postToUpdate == null)
            {
                _logger.LogInformation("Post ID not found");
                return new JsonResult("No user to edit");
            }
            postToUpdate.Type = post.Type;
            postToUpdate.Platform = post.Platform;
            postToUpdate.Position = post.Position;
            postToUpdate.PlayerRating = post.PlayerRating;
            postToUpdate.Description = post.Description;
            repository.Posts.Update(postToUpdate);
            _logger.LogInformation($"About to save {postToUpdate.ID} with new position {postToUpdate.Position}");
            repository.Save();

            _logger.LogInformation("Post updated!");
            return new JsonResult("Post Updated!");
        }

    }
}
