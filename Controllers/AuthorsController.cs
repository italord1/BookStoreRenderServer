using BookStoreProg.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreProg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        // GET: api/<AuthorsController>
        [HttpGet]
        public IEnumerable<Author> Get() 
        {
            return Author.Read();
        }
        // GET: api/<AuthorsController>
        [HttpGet("info")]
        public IEnumerable<object> GetAllAuthorsAndInfo()
        {
            return Author.GetAllAuthorsAndInfo();
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{authorName}")]
        public IActionResult Get(string authorName)
        {
            try
            {
                List<Book> booksOfAuthor = Author.GetBooksOfAuthor(authorName);
                if (booksOfAuthor == null)
                {
                    return Conflict(new { message = "error" });
                }
                return Ok(new { books = booksOfAuthor });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = "error" });
            }
        }

        // GET api/<AuthorsController>/5
        [HttpGet("getAuthorId/{authorName}")]
        public IActionResult GetAuthorId(string authorName)
        {

            try
            {
               int authorId = Author.GetAuthorId(authorName);
                if (authorId == -1)
                {
                    return Conflict(new { message = "error" });
                }
                return Ok(new { authorId });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = "error" });
            }
        }


        // POST api/<AuthorsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
