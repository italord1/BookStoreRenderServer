using BookStoreProg.BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreProg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return BL.User.Read();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}/OwnedBooks")]
        public IEnumerable<Book> GetOwnedBooks(int id)
        {
            try
            {
                return BookStoreProg.BL.User.GetUserBooksList(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }
        // GET api/<UsersController>/5
        [HttpGet("{id}/MyReadList")]
        public IEnumerable<Book> GetMyReadList(int id)
        {
            try
            {
                return BookStoreProg.BL.User.GetMyReadList(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}/MyCart")]
        public IEnumerable<Book> GetMyCartBooks(int id)
        {
            try
            {
                return BookStoreProg.BL.User.GetUserCartBooksList(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Book>();
            }
        }
        // GET api/<UsersController>/5
        [HttpGet("{userId}/RequestsIAsked")]
        public IEnumerable<Request> GetMyRequestsBooks(int userId)
        {

     
            try
            {
                return BookStoreProg.BL.User.GetMyRequestsBooks(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Request>();
            }
        }
        // GET api/<UsersController>/5
        [HttpGet("{userId}/RequestsAskedOfMe")]
        public IEnumerable<Request> GetMyRequestsAskedOfMeBooks(int userId)
        {
            try
            {
                IEnumerable < Request > Requests=BookStoreProg.BL.User.GetMyRequestsAskedOfMeBooks(userId);
                return Requests;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Request>();
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("GetReviews/{bookId}")]
        public IEnumerable<object> GetMyreviews(int bookId)
        {
            return BL.User.GetMyreviews(bookId);
        }





        // POST: api/<UserController>
        [HttpPost("verifyUserAccount")]
        public IActionResult verifyUserAccount([FromBody] User user)
        {
            try
            {
                object userIdAndName =BL.User.verifyUserAccount(user.Email, user.Password);
                if (userIdAndName != null)
                {
                    return Ok(new { user = userIdAndName });
                }
                else
                {
                    return Conflict(new { message = "Incorrect Username or Password." });
                }
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }

        }

        // POST api/<UserController>
        [HttpPost("addBookToCart/{userId}")]
        public IActionResult postAddToBookcart([FromBody] JsonElement data, int userId)
        {

            int BookId = Convert.ToInt32(data.GetProperty("Id").GetInt32());
            bool result = BL.User.InsertBookToCart(userId, BookId);

            if (result)
            {
                return Ok(new { message = "The book has been added to your book cart." });
            }
            else
            {
                return Conflict(new { message = "You already have this book in your book cart." });
            }


        }
        // POST api/<UserController>
        [HttpPost("sendRequestBook/{userId}")]
        public IActionResult sendRequestBook([FromBody] JsonElement data, int userId)
        {

            int bookId = Convert.ToInt32(data.GetProperty("Id").GetInt32());
            bool result = BL.User.AddNewRequestBook(userId, bookId);
            
                if (result)
                {
                    return Ok(new { message = "The request has been sent." });
                }
                else
                {
                    return Conflict(new { message = "You have already sent a request before." });
                }
            
           
            


        }

        // POST api/<UserController>
        [HttpPost("AddToMyreadList/{userId}")]
        public IActionResult postAddToMyreadList([FromBody] JsonElement data, int userId)
        {

            int BookId = Convert.ToInt32(data.GetProperty("Id").GetInt32());
            bool result = BL.User.InsertBookToToMyreadList(userId, BookId);

            if (result)
            {
                return Ok(new { message = "The book has been added to your read List." });
            }
            else
            {
                return Conflict(new { message = "You already have this book in your  read List." });
            }


        }
        // POST api/<UserController>
        [HttpPost("BuyBook/{userId}")]
        public IActionResult postAddToBookList([FromBody] JsonElement data, int userId)
        {

            int BookId = Convert.ToInt32(data.GetProperty("Id").GetInt32());
            int result = BL.User.InsertBookToUsersBuyBooks(userId, BookId);

            if (result== 1)
            {
                return Ok(new { message = "The book has been added to your book cart." });
            }
            if (result == 0)
            {
                return Conflict(new { message = "You already have this book in your book cart."});
            }
            if (result == 2)
            {
                return Conflict(new { 
                    message = "The book is already taken by another user.", 
                    takenByAnotherUser = true, 
                    BookId= BookId,
                    userId= userId
                });
            }
           else
            {
                return Conflict(new { message = "Something went wrong."});
            }

        }






        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            int idUserResult = user.Insert();

            if (idUserResult != -1)
            {
                return Ok(new { id = idUserResult });
            }
            else
            {
                return Conflict(new { message = "The username is already taken, try another one." });
            }
        }

        // POST api/<UserController>
        [HttpPost("AddReview/{userId}/{bookId}")]
        public IActionResult AddReview([FromBody] JsonElement data, int userId, int bookId)
        {
            // Extract the comment from the JSON data
            string comment = data.GetProperty("comment").GetString();

            // Example: Call your business logic (BL) layer to handle the review addition
            bool result = BL.User.AddNewReview(userId, bookId, comment);

            if (result)
            {
                return Ok(new { message = "Review added successfully." });
            }
            else
            {
                return Conflict(new { message = "You have already submitted a review for this book." });
            }
        }





        // PUT api/<UsersController>/5
        [HttpPut("{userId}/ApproveRequest")]
        public IActionResult ApproveRequest([FromBody] JsonElement data,int userId)
        {
            int bookId = Convert.ToInt32(data.GetProperty("bookId").GetInt32());
            int userRequestingTheBookId = Convert.ToInt32(data.GetProperty("userRequestingTheBookId").GetInt32());
            bool result = BL.User.ApproveRequest(bookId, userId, userRequestingTheBookId);

            if (result)
            {
                return Ok(new { message = "The Request Approve." });
            }
            else
            {
                return Conflict(new { message = "Something went wrong." });
            }
        }
        // PUT api/<UsersController>/5
        [HttpPut("{userId}/notApproveRequest")]
        public IActionResult NotApproveRequest([FromBody] JsonElement data, int userId)
        {
            int bookId = Convert.ToInt32(data.GetProperty("bookId").GetInt32());
            int userRequestingTheBookId = Convert.ToInt32(data.GetProperty("userRequestingTheBookId").GetInt32());
            bool result = BL.User.NotApproveRequest(bookId, userId, userRequestingTheBookId);

            if (result)
            {
                return Ok(new { message = "The Request not Approve." });
            }
            else
            {
                return Conflict(new { message = "Something went wrong." });
            }
        }
        // PUT api/<UsersController>/5
        [HttpPut("changeImage/{userId}")]
        public IActionResult changeUserImage([FromBody] string imageLink, int userId)
        {
          
            bool result = BL.User.changeUserImage( userId, imageLink);

            if (result)
            {
                return Ok(new { message = "The Request not Approve."  , ImageLink = imageLink });
            }
            else
            {
                return Conflict(new { message = "Something went wrong." });
            }
        }
        // DELETE api/<UserController>/5
        [HttpDelete("DeleteMyCartbook/{userId}")]
        public IActionResult DeleteBook([FromBody] Book book, int userId)
        {
            bool result = BL.User.DeleteBook(userId,book.Id);
            if (result)
            {
                return Ok(new {message="book deleted"});
            }
            else
            {
                return Conflict();
            }
        }


        // DELETE api/<UserController>/5
        [HttpDelete("Deleteownedbook/{userId}")]
        public IActionResult Deleteownedbook([FromBody] Book book, int userId)
        {
            bool result = BL.User.Deleteownedbook(userId, book.Id);
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

