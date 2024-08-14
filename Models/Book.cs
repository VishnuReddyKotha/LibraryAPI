namespace Library_API.Models
{
    public class Book
    {
        public string title { get; set; } = string.Empty;

        public string author { get; set; } = string.Empty;

        public string isbn { get; set; } = string.Empty;

        public DateOnly publishedDate { get; set; } = new DateOnly();
    }
}
