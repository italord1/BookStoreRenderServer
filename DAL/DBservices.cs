using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using BookStoreProg.BL;
using System.Text.Json;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;
using System.Data.Common;
using System.Net;
using System.ComponentModel.Design;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // This method insert a user to the Users table 
    //--------------------------------------------------------------------------------------------------
    public int AddNewBookToAllBooksList(Book book)
    {


        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureAddNewBook(con, "spInsertBook", book);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method Approve Request 
    //--------------------------------------------------------------------------------------------------
    public int ApproveRequest(int bookId, int userId, int userRequestingTheBookId)
    {


        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureRequest(con, "spApproveRequest", bookId, userId, userRequestingTheBookId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method Not Approve Request 
    //--------------------------------------------------------------------------------------------------
    public int NotApproveRequest(int bookId, int userId, int userRequestingTheBookId)
    {


        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureRequest(con, "spNotApproveRequest", bookId, userId, userRequestingTheBookId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method change User Image in Users table
    //--------------------------------------------------------------------------------------------------
    public int ChangeUserImage(int userId, string imageLink)
    {


        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureChangeUserImage(con, "spChangeUserImage", userId, imageLink);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method insert a Request Book to RequestBook table 
    //--------------------------------------------------------------------------------------------------
    public int AddNewRequestBook(int userId, int bookId)
    {


        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureAddNewRequestBook(con, "spAddNewRequestBook", userId, bookId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    // This method get a book from books table 
    //--------------------------------------------------------------------------------------------------
    public Book GetBook(int id)
    {


        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureGetBook(con, "spGetBook", id);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                dataReader.Read();
                Book book = new Book
                        (
                            id: Convert.ToInt32(dataReader["id"]),
                            title: dataReader["title"].ToString(),
                            authors: dataReader["authors"].ToString().Split(',').ToList(),
                            publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                            categories: dataReader["categories"].ToString().Split(',').ToList(),
                            rating: Convert.ToDouble(dataReader["rating"]),
                            imageLink: dataReader["imageLink"].ToString(),
                            language: dataReader["language"].ToString(),
                            country: dataReader["country"].ToString(),
                            price: Convert.ToSingle(dataReader["price"]),
                            description: dataReader["description"].ToString(),
                            isebook: Convert.ToBoolean(dataReader["isEbook"]),
                            infoLink: dataReader["infoLink"].ToString()
                        );

                return book;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method Get My Requests Books from RequestBook table 
    //--------------------------------------------------------------------------------------------------
    public List<Request> GetMyRequestsBooks(int userId)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // Create the connection
        }
        catch (Exception ex)
        {
            // Log the exception (consider using a logging framework)
            throw ex;
        }

        cmd = CreateCommandWithStoredProcedureGetRequests(con, "spGetMyRequestsBooks", userId); // Create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // Execute the command
            if (dataReader.HasRows)
            {
                List<Request> requests = new List<Request>();

                while (dataReader.Read())
                {
                    Book book = new Book
                    (
                        id: Convert.ToInt32(dataReader["id"]),
                        title: dataReader["title"].ToString(),
                        authors: dataReader["authors"].ToString().Split(',').ToList(),
                        publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                        categories: dataReader["categories"].ToString().Split(',').ToList(),
                        rating: Convert.ToDouble(dataReader["rating"]),
                        imageLink: dataReader["imageLink"].ToString(),
                        language: dataReader["language"].ToString(),
                        country: dataReader["country"].ToString(),
                        price: Convert.ToSingle(dataReader["price"]),
                        description: dataReader["description"].ToString(),
                        isebook: Convert.ToBoolean(dataReader["isEbook"]),
                        infoLink: dataReader["infoLink"].ToString()
                    );

                    Request request = new Request
                    (
                        book: book,
                        userRequestingTheBookId: Convert.ToInt32(dataReader["UserRequestingTheBook"]),
                        userRequestingTheBookName: dataReader["UserRequestingTheBookName"].ToString(),
                        userWhoHasTheBookId: Convert.ToInt32(dataReader["UserWhoHasTheBook"]),
                        userWhoHasTheBookName: dataReader["UserWhoHasTheBookName"].ToString(),
                        requestDate: Convert.ToDateTime(dataReader["requestDate"]),
                        status: dataReader["status"].ToString()
                    );

                    requests.Add(request);
                }

                return requests;
            }
            else
            {
                return new List<Request>();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
    //--------------------------------------------------------------------------------------------------
    // This method Get My Requests Asked Of Me Books from RequestBook table 
    //--------------------------------------------------------------------------------------------------
    public List<Request> GetMyRequestsAskedOfMeBooks(int userId)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // Create the connection
        }
        catch (Exception ex)
        {
            // Log the exception (consider using a logging framework)
            throw ex;
        }

        cmd = CreateCommandWithStoredProcedureGetRequests(con, "spGetMyRequestsAskedOfMeBooks", userId); // Create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // Execute the command
            if (dataReader.HasRows)
            {
                List<Request> requests = new List<Request>();

                while (dataReader.Read())
                {
                    Book book = new Book
                    (
                        id: Convert.ToInt32(dataReader["id"]),
                        title: dataReader["title"].ToString(),
                        authors: dataReader["authors"].ToString().Split(',').ToList(),
                        publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                        categories: dataReader["categories"].ToString().Split(',').ToList(),
                        rating: Convert.ToDouble(dataReader["rating"]),
                        imageLink: dataReader["imageLink"].ToString(),
                        language: dataReader["language"].ToString(),
                        country: dataReader["country"].ToString(),
                        price: Convert.ToSingle(dataReader["price"]),
                        description: dataReader["description"].ToString(),
                        isebook: Convert.ToBoolean(dataReader["isEbook"]),
                        infoLink: dataReader["infoLink"].ToString()
                    );

                    Request request = new Request
                    (
                        book: book,
                        userRequestingTheBookId: Convert.ToInt32(dataReader["UserRequestingTheBook"]),
                        userRequestingTheBookName: dataReader["UserRequestingTheBookName"].ToString(),
                        userWhoHasTheBookId: Convert.ToInt32(dataReader["UserWhoHasTheBook"]),
                        userWhoHasTheBookName: dataReader["UserWhoHasTheBookName"].ToString(),
                        requestDate: Convert.ToDateTime(dataReader["requestDate"]),
                        status: dataReader["status"].ToString()
                    );

                    requests.Add(request);
                }

                return requests;
            }
            else
            {
                return new List<Request>();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
    //--------------------------------------------------------------------------------------------------
    // This method get  Users from Users table 
    //--------------------------------------------------------------------------------------------------
    public List<object> GetAllUsers()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = buildReadStoredProcedureCommand(con, "spGetAllUsers");             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<object> users = new List<object>();

                while (dataReader.Read())
                {
                    object user = new
                    {
                        id = Convert.ToInt32(dataReader["id"]),
                        name = dataReader["name"].ToString(),
                        email = dataReader["email"].ToString()

                    };
                    users.Add(user);
                }

                return users;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method get Books Of Author from books table 
    //--------------------------------------------------------------------------------------------------
    public List<Book> GetBooksOfAuthor(string authorName)
    {


        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureGetBooksOfAuthor(con, "spGetBooksOfAuthor", authorName);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<Book> books = new List<Book>();
                while (dataReader.Read())
                {

                    Book book = new Book
                        (
                            id: Convert.ToInt32(dataReader["id"]),
                            title: dataReader["title"].ToString(),
                            authors: dataReader["authors"].ToString().Split(',').ToList(),
                            publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                            categories: dataReader["categories"].ToString().Split(',').ToList(),
                            rating: Convert.ToDouble(dataReader["rating"]),
                            imageLink: dataReader["imageLink"].ToString(),
                            language: dataReader["language"].ToString(),
                            country: dataReader["country"].ToString(),
                            price: Convert.ToSingle(dataReader["price"]),
                            description: dataReader["description"].ToString(),
                            isebook: Convert.ToBoolean(dataReader["isEbook"]),
                            infoLink: dataReader["infoLink"].ToString()
                        );
                    books.Add(book);

                }

                return books;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method Get All Books And Info from books table and BooksOfUser 
    //--------------------------------------------------------------------------------------------------
    public List<object> GetAllBooksAndInfo()
    {


        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = buildReadStoredProcedureCommand(con, "spGetAllBooksAndInfo");             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<object> books = new List<object>();
                while (dataReader.Read())
                {

                    object bookAndInfo = new
                    {

                        id = Convert.ToInt32(dataReader["id"]),
                        title = dataReader["title"].ToString(),
                        authors = dataReader["authors"].ToString().Split(',').ToList(),
                        publishDate = Convert.ToDateTime(dataReader["publishDate"]),
                        categories = dataReader["categories"].ToString().Split(',').ToList(),
                        rating = Convert.ToDouble(dataReader["rating"]),
                        imageLink = dataReader["imageLink"].ToString(),
                        language = dataReader["language"].ToString(),
                        country = dataReader["country"].ToString(),
                        price = Convert.ToSingle(dataReader["price"]),
                        description = dataReader["description"].ToString(),
                        isEbook = Convert.ToBoolean(dataReader["isEbook"]),
                        infoLink = dataReader["infoLink"].ToString(),
                        countUsers = Convert.ToInt32(dataReader["countUsers"])


                    };

                    books.Add(bookAndInfo);

                }

                return books;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method get a the Author Id from Authors table 
    //--------------------------------------------------------------------------------------------------
    public int GetAuthorId(string authorName)
    {
        SqlConnection con = null;
        SqlCommand cmd = null;

        try
        {
            con = connect("myProjDB"); // create the connection

            // Create the command with the stored procedure
            cmd = CreateCommandWithStoredProcedureGetauthorId(con, "spGetAuthorId", authorName);

            // Define and add the output parameter
            SqlParameter outputParam = new SqlParameter("@AuthorId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);

            // Execute the command
            cmd.ExecuteNonQuery();

            // Retrieve the value of the output parameter
            int authorId = (int)cmd.Parameters["@AuthorId"].Value;

            return authorId;
        }
        catch (Exception ex)
        {
            // Log the exception
            throw;
        }
        finally
        {
            if (con != null)
            {
                // Close the database connection
                con.Close();
            }
        }
    }





    //--------------------------------------------------------------------------------------------------
    // This method Get All Books And Info from books table and RequestBooks table
    //--------------------------------------------------------------------------------------------------
    public List<object> GetTradedBooksForDataTable()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }

        cmd = buildReadStoredProcedureCommand(con, "GetTradedBooksAndRequests"); // Replace with your stored procedure name

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            if (dataReader.HasRows)
            {
                List<object> tradedBooks = new List<object>();
                while (dataReader.Read())
                {
                    object bookAndRequest = new
                    {
                        id = Convert.ToInt32(dataReader["BookId"]),
                        userRequestingBook = Convert.ToInt32(dataReader["UserRequestingTheBook"]),
                        userWhoHasBook = Convert.ToInt32(dataReader["UserWhoHasTheBook"]),
                        tradeDate = Convert.ToDateTime(dataReader["TradeDate"]),
                        bookPassedSuccessfully = Convert.ToBoolean(dataReader["BookPassedSuccessfully"])
                    };

                    tradedBooks.Add(bookAndRequest);
                }

                return tradedBooks;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------
    // This method Get All Authors And Info from authors table and AuthorsOfBook 
    //--------------------------------------------------------------------------------------------------
    public List<object> GetAllAuthorsAndInfo()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = buildReadStoredProcedureCommand(con, "spGetAllAuthorsAndInfo");             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<object> authors = new List<object>();
                while (dataReader.Read())
                {

                    object authorAndInfo = new
                    {

                        id = Convert.ToInt32(dataReader["id"]),
                        name = dataReader["name"].ToString(),
                        countAuthorInUsersLibrary = Convert.ToInt32(dataReader["countAuthorInUsersLibrary"]),

                    };

                    authors.Add(authorAndInfo);

                }

                return authors;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    private SqlCommand CreateCommandWithStoredProcedureAddNewBook(SqlConnection con, string spName, Book book)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@title", book.Title);
        cmd.Parameters.AddWithValue("@authors", string.Join(",", book.Authors));
        cmd.Parameters.AddWithValue("@publishDate", book.PublishDate);
        cmd.Parameters.AddWithValue("@categories", string.Join(",", book.Categories));  // Join categories into a single string
        cmd.Parameters.AddWithValue("@rating", book.Rating);
        cmd.Parameters.AddWithValue("@imageLink", book.ImageLink);
        cmd.Parameters.AddWithValue("@country", book.Country);
        cmd.Parameters.AddWithValue("@language", book.Language);
        cmd.Parameters.AddWithValue("@price", book.Price);
        cmd.Parameters.AddWithValue("@description", book.Description);
        cmd.Parameters.AddWithValue("@isebook", book.Isebook);
        cmd.Parameters.AddWithValue("@infoLink", book.InfoLink);


        return cmd;
    }

    public List<Book> GetAllBooks()
    {
        List<Book> books = new List<Book>();

        // Use 'using' to ensure the connection is closed and disposed correctly
        using (SqlConnection con = connect("myProjDB"))
        {
            SqlCommand cmd = buildReadStoredProcedureCommand(con, "GetAllBooks"); // Stored procedure name

            try
            {
                // Open the connection if not open
                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {


                        Book book = new Book
                        (
                            id: Convert.ToInt32(dataReader["id"]),
                            title: dataReader["title"].ToString(),
                            authors: dataReader["authors"].ToString().Split(',').ToList(),
                            publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                            categories: dataReader["categories"].ToString().Split(',').ToList(),
                            rating: Convert.ToDouble(dataReader["rating"]),
                            imageLink: dataReader["imageLink"].ToString(),
                            language: dataReader["language"].ToString(),
                            country: dataReader["country"].ToString(),
                            price: Convert.ToSingle(dataReader["price"]),
                            description: dataReader["description"].ToString(),
                            isebook: Convert.ToBoolean(dataReader["isEbook"]),
                            infoLink: dataReader["infoLink"].ToString()
                        );

                        books.Add(book);
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle or log the exception appropriately
                throw new ApplicationException("Error fetching books.", ex);
            }
        }

        return books;
    }
    //--------------------------------------------------------------------------------------------------
    // This method get Get All Authors from authors table 
    //--------------------------------------------------------------------------------------------------
    public List<Author> GetAllAuthors()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = buildReadStoredProcedureCommand(con, "GetAllAuthors");             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<Author> authors = new List<Author>();
                while (dataReader.Read())
                {
                    Author author = new Author
                    (
                        id: Convert.ToInt32(dataReader["id"]),
                        name: dataReader["name"].ToString()

                    );

                    authors.Add(author);
                }

                return authors;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method get Get All Books from books table 
    //--------------------------------------------------------------------------------------------------

    private SqlCommand buildReadStoredProcedureCommand(SqlConnection con, String spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text




        return cmd;
    }

    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedure("spInsertUser", con, user);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                dataReader.Read();
                return Convert.ToInt32(dataReader["id"]);

            }
            else
            {
                return -1;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@Email", user.Email);

        cmd.Parameters.AddWithValue("@Name", user.Name);

        cmd.Parameters.AddWithValue("@Password", user.Password);

        cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

        cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
        cmd.Parameters.AddWithValue("@UserImageLink", user.UserImageLink);


        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method verify User Account in Users table 
    //--------------------------------------------------------------------------------------------------
    public object verifyUserAccount(string userEmail, string password)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedure("spVerifyUserAccount", con, userEmail, password);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                dataReader.Read();
                object result = new
                {
                    userId = Convert.ToInt32(dataReader["id"]),
                    userName = dataReader["name"].ToString(),
                    isAdmin = dataReader["isAdmin"].ToString(),
                    userImageLink = dataReader["userImageLink"].ToString()

                };
                return result;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for insert New Authors Of Book
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetBook(SqlConnection con, string spName, int bookID)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@id", bookID);

        return cmd;
    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for get author Id
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetauthorId(SqlConnection con, string spName, string authorname)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@name", authorname);

        return cmd;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for insert New Authors Of Book
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetBooksOfAuthor(SqlConnection con, string spName, string authorName)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@authorName", authorName);

        return cmd;
    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for Search Books by text
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureSearchBooks(SqlConnection con, string spName, string searchText)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@searchText", searchText);

        return cmd;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for insert New Authors Of Book
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureAddNewAuthorsOfBook(SqlConnection con, string spName, int authorID, int bookID)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@authorID", authorID);
        cmd.Parameters.AddWithValue("@bookID", bookID);

        return cmd;
    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for insert New Categories Of Book
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureAddNewCategoriesOfBook(SqlConnection con, string spName, string category, int bookID)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@category", category);
        cmd.Parameters.AddWithValue("@bookID", bookID);

        return cmd;
    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for insert Category
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureAddNewCategory(SqlConnection con, string spName, string category)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@category", category);
        return cmd;
    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for insert Category
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureAddNewAuthor(SqlConnection con, string spName, string author)
    {
        SqlCommand cmd = new SqlCommand();  // Create the command object

        cmd.Connection = con;               // Assign the connection to the command object
        cmd.CommandText = spName;           // Stored procedure name
        cmd.CommandTimeout = 10;            // Time to wait for the execution
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Command is a stored procedure

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@name", author);


        return cmd;
    }


    //------------------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for verify User Account in Users table 
    //------------------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, string userEmail, string password)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserEmail", userEmail);

        cmd.Parameters.AddWithValue("@Password", password);

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method get User Books List  in BooksOfUser table 
    //--------------------------------------------------------------------------------------------------
    public List<Book> GetUserBooksList(int userId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedure("spGetUserBooks", con, userId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<Book> books = new List<Book>();

                while (dataReader.Read())
                {


                    Book book = new Book
                    (
                        id: Convert.ToInt32(dataReader["id"]),
                        title: dataReader["title"].ToString(),
                        authors: dataReader["authors"].ToString().Split(',').ToList(),
                        publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                        categories: dataReader["categories"].ToString().Split(',').ToList(),
                        rating: Convert.ToDouble(dataReader["rating"]),
                        imageLink: dataReader["imageLink"].ToString(),
                        language: dataReader["language"].ToString(),
                        country: dataReader["country"].ToString(),
                        price: Convert.ToSingle(dataReader["price"]),
                        description: dataReader["description"].ToString(),
                        isebook: Convert.ToBoolean(dataReader["isEbook"]),
                        infoLink: dataReader["infoLink"].ToString()
                    );

                    books.Add(book);
                }


                return books;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    // This method get User Read List  from ReadListOfUser table 
    //--------------------------------------------------------------------------------------------------
    public List<Book> GetMyReadList(int userId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedure("spGetMyReadList", con, userId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<Book> books = new List<Book>();

                while (dataReader.Read())
                {


                    Book book = new Book
                    (
                        id: Convert.ToInt32(dataReader["id"]),
                        title: dataReader["title"].ToString(),
                        authors: dataReader["authors"].ToString().Split(',').ToList(),
                        publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                        categories: dataReader["categories"].ToString().Split(',').ToList(),
                        rating: Convert.ToDouble(dataReader["rating"]),
                        imageLink: dataReader["imageLink"].ToString(),
                        language: dataReader["language"].ToString(),
                        country: dataReader["country"].ToString(),
                        price: Convert.ToSingle(dataReader["price"]),
                        description: dataReader["description"].ToString(),
                        isebook: Convert.ToBoolean(dataReader["isEbook"]),
                        infoLink: dataReader["infoLink"].ToString()
                    );

                    books.Add(book);
                }


                return books;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for  get User Books List 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@userId", userId);

        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // This method get User Books List  in CartOfUser table 
    //--------------------------------------------------------------------------------------------------
    public List<Book> GetUserCartBooksList(int userId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedure("spGetUserCartBooks", con, userId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<Book> books = new List<Book>();

                while (dataReader.Read())
                {


                    Book book = new Book
                    (
                        id: Convert.ToInt32(dataReader["id"]),
                        title: dataReader["title"].ToString(),
                        authors: dataReader["authors"].ToString().Split(',').ToList(),
                        publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                        categories: dataReader["categories"].ToString().Split(',').ToList(),
                        rating: Convert.ToDouble(dataReader["rating"]),
                        imageLink: dataReader["imageLink"].ToString(),
                        language: dataReader["language"].ToString(),
                        country: dataReader["country"].ToString(),
                        price: Convert.ToSingle(dataReader["price"]),
                        description: dataReader["description"].ToString(),
                        isebook: Convert.ToBoolean(dataReader["isEbook"]),
                        infoLink: dataReader["infoLink"].ToString()
                    );

                    books.Add(book);
                }


                return books;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }



        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method get Get All Books from books table 
    //--------------------------------------------------------------------------------------------------
    public List<Book> searchBooks(string searchText)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureSearchBooks(con, "spSearchBooks", searchText);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                List<Book> books = new List<Book>();
                while (dataReader.Read())
                {
                    Book book = new Book
                        (
                            id: Convert.ToInt32(dataReader["id"]),
                            title: dataReader["title"].ToString(),
                            authors: dataReader["authors"].ToString().Split(',').ToList(),
                            publishDate: Convert.ToDateTime(dataReader["publishDate"]),
                            categories: dataReader["categories"].ToString().Split(',').ToList(),
                            rating: Convert.ToDouble(dataReader["rating"]),
                            imageLink: dataReader["imageLink"].ToString(),
                            language: dataReader["language"].ToString(),
                            country: dataReader["country"].ToString(),
                            price: Convert.ToSingle(dataReader["price"]),
                            description: dataReader["description"].ToString(),
                            isebook: Convert.ToBoolean(dataReader["isEbook"]),
                            infoLink: dataReader["infoLink"].ToString()
                        );

                    books.Add(book);
                }
                return books;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method Add Book To User buy to BooksOfUser  table 
    //--------------------------------------------------------------------------------------------------
    public int AddBookToUserbuyBook(int userId, int bookId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureAddBookToBuyBookOfUser("spAddBookToBooksOfUser", con, userId, bookId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);// execute the command
            if (dataReader.HasRows)
            {
                dataReader.Read();
                return Convert.ToInt32(dataReader["result"]);

            }
            else
            {
                return 3;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    // This method Add Book To User buy to BooksOfUser  table 
    //--------------------------------------------------------------------------------------------------
    public int AddBookToToMyreadList(int userId, int bookId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureAddBookToToMyreadList("spAddBookToToMyreadList", con, userId, bookId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public int AddBookToUserBookCart(int userId, int bookId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureAddBookToBookCartOfUser("spAddBookToCartOfUser", con, userId, bookId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    private SqlCommand CreateCommandWithStoredProcedureAddBookToBuyBookOfUser(String spName, SqlConnection con, int userId, int BookId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        cmd.Parameters.AddWithValue("@BookId", BookId);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method delete Book from User Book list in cartOfUsers table 
    //--------------------------------------------------------------------------------------------------
    public int DeleteBookFromUserBooksList(int userId, int BookId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureDeleteBookFromUserBooksList("spDeleteBookFromUserCartBooksList", con, userId, BookId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }



    //--------------------------------------------------------------------------------------------------
    // This method delete Book from User Book list in BooksOfUsers table 
    //--------------------------------------------------------------------------------------------------
    public int Deleteownedbook(int userId, int BookId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureDeleteBookFromUserBooksList("spDeleteBookFromUserBooksList", con, userId, BookId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }




    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for delete course from User books list 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureDeleteBookFromUserBooksList(String spName, SqlConnection con, int userId, int BookId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        cmd.Parameters.AddWithValue("@BookId", BookId);

        return cmd;
    }

    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for delete course from User courses list 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureCheckIfTaken(SqlConnection con, String spName, int BookId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@BookId", BookId);

        return cmd;
    }
    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for add Book to User Book cart 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureAddBookToBookCartOfUser(String spName, SqlConnection con, int userId, int BookId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        cmd.Parameters.AddWithValue("@BookId", BookId);

        return cmd;
    }

    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for add review to Usercomments 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureAddCommentToUsercomments(String spName, SqlConnection con, int userId, int BookId, string comment)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        cmd.Parameters.AddWithValue("@BookId", BookId);

        cmd.Parameters.AddWithValue("@Comment", comment);

        return cmd;
    }





    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for add Book to User Book cart 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureAddBookToToMyreadList(String spName, SqlConnection con, int userId, int BookId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        cmd.Parameters.AddWithValue("@BookId", BookId);

        return cmd;
    }
    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for add Book to User Book cart 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureAddNewRequestBook(SqlConnection con, String spName, int userId, int BookId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        cmd.Parameters.AddWithValue("@BookId", BookId);

        return cmd;
    }
    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for add Book to User Book cart 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGetRequests(SqlConnection con, String spName, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        return cmd;
    }
    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for add Book to User Book cart 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureRequest(SqlConnection con, string spName, int bookId, int userId, int userRequestingTheBookId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@BookId", bookId);
        cmd.Parameters.AddWithValue("@UserWhoHasTheBookId", userId);
        cmd.Parameters.AddWithValue("@UserRequestingTheBookId", userRequestingTheBookId);


        return cmd;
    }
    //--------------------------------------------------------------------------------------------------
    // This method retrieves top 20 books from the Books table
    //--------------------------------------------------------------------------------------------------
    public List<Object> GetTop20Books()
    {
        List<Object> books = new List<Object>();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        SqlCommand cmd = buildReadStoredProcedureCommand(con, "spGetTop20Books");

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);



            while (dataReader.Read())
            {
                var o = new
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Title = dataReader["title"].ToString(),
                    Rating = Convert.ToSingle(dataReader["rating"]),
                    Authors = dataReader["authors"].ToString().Split(',').ToList(),
                    PublishDate = Convert.ToDateTime(dataReader["publishDate"]),
                    Categories = dataReader["categories"].ToString().Split(',').ToList(),
                    ImageLink = dataReader["imageLink"].ToString(),
                    Language = dataReader["language"].ToString(),
                    Country = dataReader["country"].ToString(),
                    Price = Convert.ToSingle(dataReader["price"]),
                    Description = dataReader["description"].ToString(),
                    IsEbook = Convert.ToBoolean(dataReader["isEbook"]),
                    InfoLink = dataReader["infoLink"].ToString()
                };
                books.Add(o);
            }

            return books;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }


    public int AddNewReview(int userId, int bookId, string comment)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureAddCommentToUsercomments("spAddReviewToUserComments", con, userId, bookId, comment);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method get User comment List  in user comments table 
    //--------------------------------------------------------------------------------------------------
    public List<object> GetReviewsByBookId(int bookId)
    {
        List<object> comments = new List<object>();
        SqlConnection con = null;
        SqlCommand cmd = null;

        try
        {
            con = connect("myProjDB"); // Create the connection
            cmd = buildReadStoredProcedureCommand(con, "spGetBooksComments", bookId); // Create the command

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // Execute the command

            while (dataReader.Read())
            {
                var comment = new
                {
                    UserName = dataReader["UserName"].ToString(),
                    userImageLink = dataReader["userImageLink"].ToString(),
                    CommentText = dataReader["Comment"].ToString(),
                    CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"])
                  

                };
                comments.Add(comment);
            }

            return comments;
        }
        catch (Exception ex)
        {
            // Log the exception (implement logging as needed)
            throw; // Re-throw the exception
        }
        finally
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close(); // Ensure the connection is closed
            }
        }
    }

    private SqlCommand buildReadStoredProcedureCommand(SqlConnection con, string spName, int bookId)
    {
        SqlCommand cmd = new SqlCommand(); // Create the command object

        cmd.Connection = con;              // Assign the connection to the command object
        cmd.CommandText = spName;          // Set the command text to the stored procedure name
        cmd.CommandTimeout = 10;           // Set the command timeout
        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Specify the command type as Stored Procedure

        // Add the parameter for the stored procedure
        cmd.Parameters.AddWithValue("@BookId", bookId);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method Edit Book In All Books List in Books table 
    //--------------------------------------------------------------------------------------------------
    public int EditBookInAllBooksList(JsonElement book)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureEditDataTableBook("spEditDataTableBook", con, book);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //----------------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for Add New Course To All Courses List
    //---------------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureEditDataTableBook(String spName, SqlConnection con, JsonElement book)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(book.GetProperty("id").GetInt32()));
        cmd.Parameters.AddWithValue("@price", Convert.ToSingle(book.GetProperty("price").GetSingle()));
        cmd.Parameters.AddWithValue("@rating", Convert.ToSingle(book.GetProperty("rating").GetSingle()));

        return cmd;
    }

    //----------------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for Add New Course To All Courses List
    //---------------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureChangeUserImage(SqlConnection con, String spName, int userId, string imageLink)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@ImageLink", imageLink);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method delete Book from main Books table 
    //--------------------------------------------------------------------------------------------------
    public int DeleteFromMainBooksTable( int BookId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureDeleteBookFromMainBooksList("spDeleteBookFromMainBooksList", con ,BookId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //-----------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure for delete book from main books list 
    //-----------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureDeleteBookFromMainBooksList(String spName, SqlConnection con, int BookId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        cmd.Parameters.AddWithValue("@id", BookId);

        return cmd;
    }


}





