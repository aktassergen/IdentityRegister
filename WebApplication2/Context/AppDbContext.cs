using WebApplication2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace WebApplication2.Context
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, string>
    {

       public DbSet<Book> books { get; set; }
        public DbSet<UserFavBook> userFavBooks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserFavBook>()
           .HasKey(ufb => new { ufb.UserId, ufb.BookId });

            builder.Entity<UserFavBook>()
                .HasOne(ufb => ufb.Book)
                .WithMany(b => b.FavBooks)
                .HasForeignKey(ufb => ufb.BookId);

            builder.Entity<UserFavBook>()
                .HasOne(ufb => ufb.User)
                .WithMany(u => u.FavBooks)
                .HasForeignKey(ufb => ufb.UserId);

            builder.Entity<AppRole>().HasData(
         new AppRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" },
         new AppRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" }
     );
        }

    }
}
