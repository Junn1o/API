using API.Data;
using API.Models.DTO;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(AppDbContext appDbContext, IAuthorRepository authorRepository)
        {
            _appDbContext = appDbContext;
            _authorRepository = authorRepository;
        }
        [HttpGet("get-all-author")]
        public IActionResult GellAllAuthors()
        {
            var authorlist = _authorRepository.GellAllAuthors();
            return Ok(authorlist);
        }
        [HttpGet("get-author-with-id")]
        public IActionResult GetAuthorwithId(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author != null)
            {
                return Ok(author);
            }
            else
                return NotFound("Data Empty");
        }
        [HttpPost("add - author")]
        public IActionResult AddAuthor([FromBody] AddAuthorRequestDTO addAuthor)
        {
            var authorAdd = _authorRepository.AddAuthor(addAuthor);
            return Ok(authorAdd);
        }
        [HttpPut("update-author-with-id")]
        public IActionResult UpdateAuthor(int id, [FromBody] AddAuthorRequestDTO updateAuthor)
        {
            var authorUpdate = _authorRepository.UpdateAuthorById(id, updateAuthor);
            return Ok(authorUpdate);
        }
        [HttpDelete("delete-author-with-id")]
        public IActionResult DeleteAuthorwithId(int id)
        {
            var authorDelete = _authorRepository.DeleteAuthorById(id);
            if (authorDelete == null)
            {
                return StatusCode(500);
            }
            else
            {
                return Ok("author deleted");
            }
        }
    }
}
