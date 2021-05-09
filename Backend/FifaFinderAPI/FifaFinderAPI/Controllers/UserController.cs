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
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private IRepositoryWrapper repository;

        public UserController(ILogger<UserController> logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            repository = repositoryWrapper;
        }

        // GET Request which returns the entire User table
        [HttpGet]
        public JsonResult GetUsers()
        {
            var allUsers = repository.Users.FindAll();
            if (allUsers != null) {
                _logger.LogInformation($"Users returned");
                return new JsonResult(allUsers);
            }
            _logger.LogInformation("Users not returned, null value");
            return new JsonResult("User table has no records");
        }

        // POST Request which verifies credentials are acceptable and adds them to the User table on success, returning the newly created record details
        [HttpPost]
        [Route("register")]
        public JsonResult RegisterUser(RegisterUser user)
        {
            var queryEmailRegistered = repository.Users.FindByCondition(u => u.Email == user.Email).FirstOrDefault();
            var queryUsernameRegistered = repository.Users.FindByCondition(u => u.Username == user.Username).FirstOrDefault();
            // verifiying if the user credentials already exist within the User table
            if (queryEmailRegistered != null && queryUsernameRegistered != null)
            {
                _logger.LogInformation("Both Username and Email exist with the User table");
                return new JsonResult("Username and Email already registered");
            } else if (queryUsernameRegistered != null)
            {
                _logger.LogInformation("Username exists in the User tables");
                return new JsonResult("Username already registered");
            } else if (queryEmailRegistered != null)
            {
                _logger.LogInformation("Email exists in the User tables");
                return new JsonResult("Email already registered");
            }

            // Inserting the new user
            var userToInsert = repository.Users.Create( new User { Username = user.Username, Password = user.Password, Email = user.Email });
            repository.Save();

            // Returning the user that has been created
            _logger.LogInformation("Successful registration and returned user");
            return new JsonResult(repository.Users.FindByCondition(u => u.Username == user.Username).FirstOrDefault());

        }

        // POST Request which attempts to log the user in returns the user record
        [HttpPost]
        [Route("login")]
        public JsonResult LoginUser(LoginUser user)
        {
            // checking for a matching username and password
            var queryLoginAttempt = repository.Users.FindByCondition(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            // the query will be null if the match hasnt been found. if conditional handles this accordingly
            if (queryLoginAttempt != null)
            {
                _logger.LogInformation("Successful login");
                return new JsonResult(repository.Users.FindByCondition(u => u.Username == user.Username).FirstOrDefault());
            }
            else {
                _logger.LogInformation("Incorrect credentials");
                return new JsonResult("Incorrect username or password");
            } 
        }

        // DELETE Request which deletes the record specified from the id parameter
        [HttpDelete("{id}")]
        public JsonResult DeleteUser(int id)
        {
            var userToDelete = repository.Users.FindByCondition(u => u.ID == id).FirstOrDefault();
            if (userToDelete == null)
            {
                _logger.LogInformation("No user to delete");
                return new JsonResult("No user to delete");
            }
            repository.Users.Delete(userToDelete);
            repository.Save();
            _logger.LogInformation("Deletion complete");
            return new JsonResult("User Deleted!");
        }

    }
}
