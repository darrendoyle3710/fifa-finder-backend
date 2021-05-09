using FifaFinderAPI.Controllers;
using FifaFinderAPI.Library.Binding;
using FifaFinderAPI.Library.Interfaces;
using FifaFinderAPI.Library.Models;
using FifaFinderAPI.Library.Models.Binding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FifaFinderTest
{
    public class PostControllerTest
    {
        private Mock<ILogger<PostController>> _logger;
        private Mock<IRepositoryWrapper> mockRepo;
        private PostController postController;
        private Post post;
        private AddPost addPost;
        private Mock<IAddPost> addPostMock;
        private List<Post> posts;
        private Mock<IPost> postMock;
        private List<IPost> postsMock;


        public PostControllerTest()
        {
            //mock setup
            postMock = new Mock<IPost>();
            postsMock = new List<IPost> { postMock.Object };
            post = new Post();
            posts = new List<Post>();

            //sample models


            //controller setup
            //courseControllerMock = new Mock<ICourseController>();
            _logger = new Mock<ILogger<PostController>>();

            mockRepo = new Mock<IRepositoryWrapper>();
            var allPosts = GetPosts();
            postController = new PostController(_logger.Object, mockRepo.Object);
        }

        [Fact]
        public void GetPosts_Test()
        {
            // arrange
            mockRepo.Setup(repo => repo.Posts.FindAll()).Returns(GetPosts());
            // act 
            JsonResult controllerJsonResult = postController.GetPosts();
            // assert
            Assert.NotNull(controllerJsonResult);
            Assert.IsType<JsonResult>(controllerJsonResult);
        }
        [Fact]
        public void GetNullPosts_Test()
        {
            // arrange
            mockRepo.Setup(repo => repo.Posts.FindAll()).Returns(GetEmptyPosts());
            // act 
            JsonResult controllerJsonResult = postController.GetPosts();
            // assert
            Assert.NotNull(controllerJsonResult);
            Assert.IsType<JsonResult>(controllerJsonResult);
        }
        [Fact]
        public void AddPost_Test()
        {
            // arrange
            mockRepo.Setup(repo => repo.Posts.FindByCondition(u => u.ID == It.IsAny<int>())).Returns(GetPosts());
            mockRepo.Setup(repo => repo.Users.FindByCondition(u => u.ID == It.IsAny<int>())).Returns(GetUsers());


            AddPost potentialPost = new AddPost() { Type = "Looking for player", Platform = "PS5", PlayerRating = "87", Position = "RW", Description = "test description"};
            // act 
            var controllerJsonResult = postController.AddPost(potentialPost, 2);
            // assert
            Assert.NotNull(controllerJsonResult);
            Assert.IsType<JsonResult>(controllerJsonResult);
        }
        [Fact]
        public void UpdatePost_Test()
        {
            // arrange
            mockRepo.Setup(repo => repo.Posts.FindByCondition(u => u.ID == It.IsAny<int>())).Returns(GetPosts());
            EditPost potentialPost = new EditPost() { ID = 2, Type = "Looking for player", Platform = "Xbox", PlayerRating = "82", Position = "GK", Description = "test description" };
            // act 
            var controllerJsonResult = postController.UpdatePost(potentialPost);
            // assert
            Assert.NotNull(controllerJsonResult);
            Assert.IsType<JsonResult>(controllerJsonResult);
        }

        [Fact]
        public void DeletePost_Test()
        {
            // arrange
            mockRepo.Setup(repo => repo.Posts.FindByCondition(u => u.ID == It.IsAny<int>())).Returns(GetPosts());
            mockRepo.Setup(repo => repo.Posts.Delete(GetPost()));
            // act 
            var controllerJsonResult = postController.DeletePost(It.IsAny<int>());
            var nullExpected = postController.DeletePost(0);
            // assert
            Assert.NotNull(controllerJsonResult);
            Assert.IsType<JsonResult>(controllerJsonResult);
        }
        [Fact]
        public void DeletePostNull_Test()
        {
            // arrange
            mockRepo.Setup(repo => repo.Posts.FindByCondition(u => u.ID == It.IsAny<int>())).Returns(GetEmptyPosts());
            mockRepo.Setup(repo => repo.Posts.Delete(GetEmptyPost()));
            // act 
            var controllerJsonResult = postController.DeletePost(It.IsAny<int>());
            var nullExpected = postController.DeletePost(0);
            // assert
            Assert.NotNull(controllerJsonResult);
            Assert.IsType<JsonResult>(controllerJsonResult);
        }


        private IEnumerable<Post> GetPosts()
        {
            var posts = new List<Post> {
            new Post(){ID = 1, Type = "Looking for player", Platform = "PS5", PlayerRating = "87", Position = "RW", Description = "test description", User = null , CreatedAt = DateTime.Now},
            new Post(){ID = 2, Type = "Looking for club", Platform = "PS4", PlayerRating = "88", Position = "LM", Description = "something something", User = null , CreatedAt = DateTime.Now},
            };
            return posts;
        }
        private IEnumerable<Post> GetEmptyPosts()
        {
            
            return null;
        }
        private Post GetEmptyPost()
        {
            return null;
        }
        private Post GetPost()
        {
            return GetPosts().ToList()[0];
        }
        private IEnumerable<User> GetUsers()
        {
            var users = new List<User> {
            new User(){ID = 1, Username = "JoeBloggs", Email = "joebloggs@gmail.com", Password="password", CreatedAt = DateTime.Now},
            new User(){ID = 2, Username = "SallyBobert", Email = "sallybobert@gmail.com", Password="password", CreatedAt = DateTime.Now}
            };
            return users;
        }
    }
}
