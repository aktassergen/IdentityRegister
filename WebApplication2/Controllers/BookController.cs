using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication2.Context;
using WebApplication2.Data;
using WebApplication2.Dto;
using WebApplication2.Token;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BookController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetBooks([FromQuery] BooksFilter filter)
        {
            var query = _context.books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                query = query.Where(b => b.CategoryName == filter.CategoryName);
            }


            var result = query.Select(b => new BookDto
            {
                BookName = b.BookName,
                AuthName = b.AuthName,
                CategoryName = b.CategoryName,
                NumberOfPages = b.NumberOfPages,
                Color = b.Color
            }).ToList();

            return Ok(result);
        }

      [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto bookDto)
        {
            if (_context.books.Any(b => b.BookName == bookDto.BookName))
            {
                return BadRequest("Book with the same name already exists.");
            }

            var book = new Book
            {
                BookName = bookDto.BookName,
                AuthName = bookDto.AuthName,
                CategoryName = bookDto.CategoryName,
                NumberOfPages = bookDto.NumberOfPages,
                Color = bookDto.Color
            };
            await _context.books.AddAsync(book);
            await _context.SaveChangesAsync();

            return Ok("Book added successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDto bookDto)
        {
            var book = _context.books.Find(id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            book.BookName = bookDto.BookName;
            book.AuthName = bookDto.AuthName;
            book.CategoryName = bookDto.CategoryName;
            book.NumberOfPages = bookDto.NumberOfPages;
            book.Color = bookDto.Color;

            _context.SaveChanges();

            return Ok("Book updated successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = _context.books.Find(id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            _context.books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("Book deleted successfully.");
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _context.books.Find(id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            var result = new BookDto
            {
                BookName = book.BookName,
                AuthName = book.AuthName,
                CategoryName = book.CategoryName,
                NumberOfPages = book.NumberOfPages,
                Color = book.Color
            };

            return Ok(result);
        }

        [HttpPost("add-favorite")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFavoriteBook(int bookId)
        {
            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = await _context.books.FirstOrDefaultAsync(x => x.Id == bookId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null && book==null)
            {
                return BadRequest("User or Book not found  ");
            }


            var userFavBook = new UserFavBook
            {
                BookId = bookId,
                UserId = userId,
            };

            _context.userFavBooks.Add(userFavBook);
            await _context.SaveChangesAsync();

            return Ok("Book added to favorites successfully.");
        }

        [HttpGet("favorite-books")]
        [Authorize]
        public async Task<IActionResult> GetFavoriteBooks()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var favoriteBooks = _context.userFavBooks
                .Where(ufb => ufb.UserId == user.Id)
                .Select(ufb => new BookDto
                {
                    BookName = ufb.Book.BookName,
                    AuthName = ufb.Book.AuthName,
                    CategoryName = ufb.Book.CategoryName,
                    NumberOfPages = ufb.Book.NumberOfPages,
                    Color = ufb.Book.Color
                })
                .ToList();

            return Ok(favoriteBooks);
        }
    }
}
