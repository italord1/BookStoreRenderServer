namespace BookStoreProg.BL
{
    public class Request
    {
        Book book;
        int userRequestingTheBookId;
        string userRequestingTheBookName;
        int userWhoHasTheBookId;
        string userWhoHasTheBookName;
        DateTime requestDate;
        string status;

        public Request(Book book, int userRequestingTheBookId, string userRequestingTheBookName, int userWhoHasTheBookId, string userWhoHasTheBookName, DateTime requestDate, string status)
        {
            Book = book;
            UserRequestingTheBookId = userRequestingTheBookId;
            UserRequestingTheBookName = userRequestingTheBookName;
            UserWhoHasTheBookId = userWhoHasTheBookId;
            UserWhoHasTheBookName = userWhoHasTheBookName;
            RequestDate = requestDate;
            Status = status;
        }

        public Book Book { get => book; set => book = value; }
        public int UserRequestingTheBookId { get => userRequestingTheBookId; set => userRequestingTheBookId = value; }
        public string UserRequestingTheBookName { get => userRequestingTheBookName; set => userRequestingTheBookName = value; }
        public int UserWhoHasTheBookId { get => userWhoHasTheBookId; set => userWhoHasTheBookId = value; }
        public string UserWhoHasTheBookName { get => userWhoHasTheBookName; set => userWhoHasTheBookName = value; }
        public DateTime RequestDate { get => requestDate; set => requestDate = value; }
        public string Status { get => status; set => status = value; }
    }
}
