namespace BuggyBackend.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int AvailableCopies { get; set; }

        public bool IsAvailable()
        {
            return AvailableCopies > 0;
        }
    }
}
