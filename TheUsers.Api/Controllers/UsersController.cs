using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheUsers.Api.Validators;
using TheUsers.Domain.Models;
using TheUsers.Domain.Services;

namespace TheUsers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            return Ok(existingUser);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var validator = new UserValidator();
            var validationResult = validator.ValidateAsync(user);
            if (!validationResult.Result.IsValid)
                return BadRequest(validationResult.Result.Errors);

            _userService.AddUser(user);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            _userService.UpdateUser(user);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
