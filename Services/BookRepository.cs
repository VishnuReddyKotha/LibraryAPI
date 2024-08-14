using Library_API.Data;
using Library_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        private readonly IValidateBook _validateBook;

        public BookRepository(DataContext context, IValidateBook validateBook)
        {
            _context = context;
            _validateBook = validateBook;

        }
        public async Task<List<BookWithId>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<BookWithId> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<BookWithId> AddBook(Book book)
        {
            _context.Books.Add(new BookWithId { title = book.title, author = book.author, isbn = book.isbn, publishedDate = book.publishedDate });
            await _context.SaveChangesAsync();

            return _context.Books.OrderByDescending(x => x.id).FirstOrDefault();
        }


        public async Task<BookWithId> UpdateBook(int id, Book bookUpdated)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return book;
            }

            var item = _validateBook.Validate(book, bookUpdated);

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return book;
        }

        public async Task<List<BookWithId>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return await _context.Books.ToListAsync();

        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.id == id);
        }

    }
}
