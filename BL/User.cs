using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStoreProg.BL
{
    public class User
    {
        int id;
        string name;
        string email;
        string password;
        bool isAdmin;
        bool isActive;
        string userImageLink;

        public User(int id, string name, string email, string password, bool isAdmin, bool isActive, string userImageLink)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.IsAdmin = isAdmin;
            this.IsActive = isActive;
            this.UserImageLink = userImageLink;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string UserImageLink { get => userImageLink; set => userImageLink = value; }

        public int Insert()
        {
            DBservices dBservices = new DBservices();
            try
            {
                this.isActive = true;
                int idUserResult = dBservices.InsertUser(this);
                return idUserResult;
            }
            catch { return -1; }


        }
        public static List<object> Read()
        {
            DBservices dBservices = new DBservices();
            try
            {
                return dBservices.GetAllUsers();

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }
        public static List<Request> GetMyRequestsAskedOfMeBooks(int userId)
        {
            DBservices dBservices = new DBservices();
            try
            {
                return dBservices.GetMyRequestsAskedOfMeBooks(userId);

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }
        public static List<Request> GetMyRequestsBooks(int userId)
        {
            DBservices dBservices = new DBservices();
            try
            {
                return dBservices.GetMyRequestsBooks(userId);

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }
        public static object verifyUserAccount(string userEmail, string password)
        {
            try
            {
                DBservices dBservices = new DBservices();
                object result = dBservices.verifyUserAccount(userEmail, password);
                return result;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


        }

        public static List<Book> GetUserBooksList(int userId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                List<Book> Books = dBservices.GetUserBooksList(userId);
                return Books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

        }
        public static List<Book> GetUserCartBooksList(int userId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                List<Book> Books = dBservices.GetUserCartBooksList(userId);
                return Books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

        }
        public static List<Book> GetMyReadList(int userId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                List<Book> Books = dBservices.GetMyReadList(userId);
                return Books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

        }




        public static bool InsertBookToCart(int userId, int courseId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int userExist = dBservices.AddBookToUserBookCart(userId, courseId);
                if (userExist == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                return false;
            }

        }
        public static bool ApproveRequest(int bookId, int userId,int userRequestingTheBookId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int result = dBservices.ApproveRequest(bookId, userId, userRequestingTheBookId);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                return false;
            }

        }
        public static bool changeUserImage( int userId, string imageLink)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int result = dBservices.ChangeUserImage( userId, imageLink);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                return false;
            }

        }
        public static bool NotApproveRequest(int bookId, int userId, int userRequestingTheBookId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int result = dBservices.NotApproveRequest(bookId, userId, userRequestingTheBookId);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                return false;
            }

        }
        public static bool InsertBookToToMyreadList(int userId, int courseId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int userExist = dBservices.AddBookToToMyreadList(userId, courseId);
                if (userExist == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                return false;
            }

        }

        public static int InsertBookToUsersBuyBooks(int userId, int courseId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                return dBservices.AddBookToUserbuyBook(userId, courseId);

            }
            catch (Exception ex)
            {
                // write to log
                return 3;
            }

        }


        public static bool DeleteBook(int userId, int BookId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int result = dBservices.DeleteBookFromUserBooksList(userId, BookId);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                return false;
            }
        }



        public static bool Deleteownedbook(int userId, int BookId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int result = dBservices.Deleteownedbook(userId, BookId);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                return false;
            }
        }


        public static bool AddNewRequestBook(int userId, int bookId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int result = dBservices.AddNewRequestBook(userId, bookId);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static bool AddNewReview(int userId, int bookId,string comment)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int result = dBservices.AddNewReview(userId, bookId,comment);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


     

        public static List<object> GetMyreviews(int bookId)
        {
            DBservices dBservices = new DBservices();
            try
            {
                List<object> reviews = dBservices.GetReviewsByBookId(bookId);
                return reviews;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }





    }




}




