using System.Diagnostics.Metrics;
using System.Text.Json;

namespace BookStoreProg.BL
{
    public class Book
    {
        int id;
        string title;
        List<string> authors;
        DateTime publishDate;
        List<string> categories;
        double rating;
        string imageLink;
        string language;
        string country;
        float price;
        string description;
        bool isebook;
        string infoLink;

        public Book(int id,string title, List<string> authors, DateTime publishDate, List<string> categories, double rating, string imageLink, string language, string country, float price, string description, bool isebook, string infoLink )
        {
            this.Id = id;
            this.Title = title;
            this.Authors = authors;
            this.PublishDate = publishDate;
            this.Categories = categories;
            this.Rating = rating;
            this.ImageLink = imageLink;
            this.Language = language;
            this.Country = country;
            this.Price = price;
            this.Description = description;
            this.Isebook = isebook;
            this.InfoLink = infoLink;
            
        }

        public string Title { get => title; set => title = value; }
        public List<string> Authors { get => authors; set => authors = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
        public List<string> Categories { get => categories; set => categories = value; }
        public double Rating { get => rating; set => rating = value; }
        public string ImageLink { get => imageLink; set => imageLink = value; }
        public string Language { get => language; set => language = value; }
        public string Country { get => country; set => country = value; }
        public float Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }
        public bool Isebook { get => isebook; set => isebook = value; }
        public string InfoLink { get => infoLink; set => infoLink = value; }
        public int Id { get => id; set => id = value; }

        public static Book getBook(int id)
        {
            DBservices dBservices = new DBservices();
            try
            {
                Book book = dBservices.GetBook(id);
                return book;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }

        public static List<Book> Read()
        {
            DBservices dBservices = new DBservices();
            try
            {
                List<Book> books = dBservices.GetAllBooks();
                return books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }
       
        public static List<object> GetAllBooksAndInfo()
        {
            DBservices dBservices = new DBservices();
            try
            {
                List<object> books = dBservices.GetAllBooksAndInfo();
                return books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }


        public static List<object> GetTradedBooksForDataTable()
        {
            DBservices dBservices = new DBservices();
            try
            {
                List<object> books = dBservices.GetTradedBooksForDataTable();
                return books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }
        public static List<Book> searchBooks(string searchText)
        {
            DBservices dBservices = new DBservices();
            try
            {
                List<Book> books = dBservices.searchBooks(searchText);
                return books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }


        public static bool Insert(Book book)
        {
            DBservices dBservices = new DBservices();
            try
            {
                int result = dBservices.AddNewBookToAllBooksList(book);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }



        }

        public static List<object> GetTop20Books()
        {
            DBservices dBservices = new DBservices();
            try
            {
                List<object> books = dBservices.GetTop20Books();
                return books;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }

        public static bool EditBook(JsonElement book)
        {
            DBservices dBservices = new DBservices();
            try
            {
                int result = dBservices.EditBookInAllBooksList(book);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }

        public static bool DeleteFromMainBooksTable(int BookId)
        {
            try
            {
                DBservices dBservices = new DBservices();
                int result = dBservices.DeleteFromMainBooksTable(BookId);
                if (result == 0) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                // write to log
                return false;
            }
        }









    }



}
