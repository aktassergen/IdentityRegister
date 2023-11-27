namespace WebApplication2.Data
{
    public class UserFavBook
    {
        public AppUser User { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
    }
}
