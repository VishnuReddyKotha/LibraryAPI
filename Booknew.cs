namespace Library_API
{
    public class Booknew
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public DateOnly publishedDate { get; set; } = new DateOnly();
    }
}
