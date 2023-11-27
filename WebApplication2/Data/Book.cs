namespace WebApplication2.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName{ get; set; }
        public string AuthName { get; set; }
        public string CategoryName { get; set; }
        public int NumberOfPages { get; set; }
        public string Color { get; set; }
        public List<UserFavBook> FavBooks { get; set; }
    }


}
