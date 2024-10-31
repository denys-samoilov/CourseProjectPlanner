using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CourseProjectPlanner.Services;

namespace CourseProjectPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersControllerAPI : ControllerBase
    {
        private readonly IUser _userService;

        public UsersControllerAPI(IUser userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userService.GetUsers;
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _userService.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _userService.Edit(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.Remove(id);
            return NoContent();
        }
    }
}
