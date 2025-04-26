using Microsoft.AspNetCore.Mvc;
using BookStoreProg.BL;
using System.Text.Json;
using System.Security.AccessControl;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreProg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class booksController : ControllerBase
    {
        // GET: api/<booksController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return Book.Read();
        }
        // GET: api/<booksController>
        [HttpGet("info")]
        public IEnumerable<object> GetAllBooksAndInfo()
        {
            return Book.GetAllBooksAndInfo();
        }
        // GET: api/<BooksController>
        [HttpGet("Top20Books")]
        public IEnumerable<object> GetTop20Books()
        {
            return Book.GetTop20Books();
        }


       
        // GET api/<booksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
          
            try
            {
                Book book = Book.getBook(id);
                if (book == null)
                {
                    return Conflict(new { message = "error" });
                }
                return Ok(new { book = book });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = "error" });
            }
        }

        // GET api/<booksController>/5
        [HttpGet("searchBooks/{searchText}")]
        public IEnumerable<Book> Get(string searchText)
        {
            try
            {
                List<Book> books = Book.searchBooks(searchText);
                return books;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            try
            {
                bool response = Book.Insert(book);
                if (response == false)
                {
                    return Conflict(new { message = "You already have this book" });
                }
                return Ok(new { message = "The book save Successfully." });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = "You already have this course or instructors Id doesnt exist" });
            }


        }

        // PUT api/<BooksController>/5
        [HttpPut]
        public IActionResult Put([FromBody] JsonElement bookToEdit)
        {
            try
            {
                bool response = Book.EditBook(bookToEdit);
                if (response == false)
                {
                    return Conflict(new { message = "Update failed due to invalid conditions" });
                }
                return Ok(new { message = "The Book updated Successfully." });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = "An error occurred" });
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("DeleteFromMainBooksTable/{BookId}")]
        public IActionResult DeleteFromMainBooksTable(int BookId)
        {
            bool result = BL.Book.DeleteFromMainBooksTable(BookId);
            if (result)
            {
                return Ok(new { message = "book deleted" });
            }
            else
            {
                return Conflict();
            }
        }
    }
}
