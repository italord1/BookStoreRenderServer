namespace BookStoreProg.BL
{
    public class Author
    {
        int id;
        string name;
        DateTime birthDate;
        string description;
        public Author(int id, string name)
        {
            this.Id = id;
            this.Name = name;
         
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

    
        public static List<Author> Read()
        {
            DBservices dBservices = new DBservices();
            try
            {
                List<Author> authors = dBservices.GetAllAuthors();
                return authors;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }
        public static List<Book> GetBooksOfAuthor(string authorName)
        {
            DBservices dBservices = new DBservices();
            try
            {
               return dBservices.GetBooksOfAuthor( authorName);
                 
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }

        public static int GetAuthorId(string authorName)
        {
            DBservices dBservices = new DBservices();
            try
            {
                return dBservices.GetAuthorId(authorName);

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }

        public static List<object> GetAllAuthorsAndInfo()
        {
            DBservices dBservices = new DBservices();
            try
            {
                return dBservices.GetAllAuthorsAndInfo();
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
        }
    }
}
