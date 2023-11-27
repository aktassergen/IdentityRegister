namespace WebApplication2.Dto
{
    public class BooksFilter
    {
        public int? PageSizeMax { get; set; }
        public int? PageSizeMin { get; set; }
        public string? CategoryName { get; set; }
        public string? Color { get; set; }
        public string? AuthorName { get; set; }
    }
}
