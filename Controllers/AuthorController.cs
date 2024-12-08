using DapperLib.Model;
using DapperLib.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperLib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepo repo;

        public AuthorController(IAuthorRepo repo)
        {
            this.repo = repo;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var _list = await repo.GetAll();

            if (_list == null || _list.Count == 0)
            {
                return NotFound("No authors found.");
            }

            return Ok(_list);
        }

        [HttpGet("GetbyCode/{code}")]
        public async Task<IActionResult> GetbyCode (int code)
        {
            var _list = await repo.GetbyCode(code);

            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Create")]

        public async Task<IActionResult> Create([FromBody] Authors authors, int code)
        {
            var _result = await repo.Create(authors);

            return Ok(_result);
        }

        [HttpDelete("Remove")]

        public async Task<IActionResult> Remove(int code)
        {
            var _result = await repo.Remove(code);

            return Ok(_result);
        }

    }
}
