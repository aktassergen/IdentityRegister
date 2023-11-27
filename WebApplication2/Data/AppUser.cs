using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Data
{
    public class AppUser:IdentityUser<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public List<UserFavBook> FavBooks { get; set; }
    }
}
