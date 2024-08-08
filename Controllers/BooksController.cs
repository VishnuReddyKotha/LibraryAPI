using Library_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly DataContext _context;
        

        public BooksController(DataContext context)
        {
            _context = context;
            
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Booknew book)
        {
            _context.Books.Add(new Book {Title = book.Title , Author = book.Author, Isbn = book.Isbn,  publishedDate = book.publishedDate });
            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());

        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book ;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> PutProduct(int id, Booknew product)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var item = Validate(book, product);

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return book;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());
        }

        private bool ProductExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private Book Validate(Book book, Booknew product)
        {
            
            if (product.Title.Length != 0) { book.Title = product.Title; }
            if (product.Author.Length != 0) { book.Author = product.Author; }
            if (product.Isbn.Length != 0) { book.Isbn = product.Isbn; }
            if (product.publishedDate != null) { book.publishedDate = product.publishedDate; }
            return book;
        }
    }
}
