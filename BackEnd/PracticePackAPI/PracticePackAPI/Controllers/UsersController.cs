using Microsoft.AspNetCore.Mvc;
using PracticePackAPI.Models;
using PracticePackAPI.Services;

namespace PracticePackAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly iService<User> _userService;

        public UsersController(iService<User> UserService)
        {
            _userService = UserService;
        }

        [HttpPost("Login")]
        public User LoginUser(User security)
        {
            List<User> users = _userService.GetAllWithData().ToList();
            User user = users.FirstOrDefault(u =>
                u.Mail == security.Mail &&
                u.Password == security.Password
            );
            if (user != null && user.BranchId == null)
                user.BranchId = Guid.Empty;
            return user;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userService.GetAllWithData().ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            return await _userService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            user.BranchId = null;
            return await _userService.Create(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            if (user.BranchId == Guid.Empty)
                user.BranchId = null;

            var updatedEntity = await _userService.Update(user);

            if (updatedEntity == null)
                return NotFound();

            return updatedEntity;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if(await _userService.GetById(id) == null)
                return NotFound();

            await _userService.Delete(id);

            return NoContent();
        }
    }
}