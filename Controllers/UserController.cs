using Microsoft.AspNetCore.Mvc;
using DapperLib.Repo;
using DapperLib.Model;

namespace DapperLib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(User user)
        {
            var result = await _userRepo.Create(user);
            if (result == "pass")
                return Ok("User created successfully");
            else
                return BadRequest("Failed to create user");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAll();
            return Ok(users);
        }

        [HttpGet("GetById/{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var user = await _userRepo.GetById(userId);
            if (user != null)
                return Ok(user);
            else
                return NotFound("User not found");
        }

        [HttpDelete("Remove/{userId}")]
        public async Task<IActionResult> Remove(int userId)
        {
            var result = await _userRepo.Remove(userId);
            if (result == "pass")
                return Ok("User removed successfully");
            else
                return BadRequest("Failed to remove user");
        }

        [HttpPut("Update/{userId}")]
        public async Task<IActionResult> Update(User user, int userId)
        {
            var result = await _userRepo.Update(user, userId);
            if (result == "pass")
                return Ok("User updated successfully");
            else
                return BadRequest("Failed to update user");
        }
    }
}
