namespace Library_API
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public DateOnly publishedDate { get; set; } = new DateOnly();
    }
}
