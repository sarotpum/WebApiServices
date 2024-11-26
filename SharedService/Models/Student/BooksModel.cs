namespace SharedService.Models.Student
{
    public class BooksModel
    {
        //BookNumber, BookName, BookAuther, Price

        public int BookNumber { get; set; }

        public string? BookName { get; set; }

        public string? BookAuther { get; set; }

        public decimal Price { get; set; }
    }
}
