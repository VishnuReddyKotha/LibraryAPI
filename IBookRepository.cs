using Library_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_API
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);

        Task<List<Book>> AddBook(Booknew book);
        Task<Book> UpdateBook(int id, Booknew book);

        Task<List<Book>> DeleteBook(int id);


    }

    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;


        public BookRepository(DataContext context)
        {
            _context = context;

        }
        public  async Task<List<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
           return await _context.Books.FindAsync(id);
        }

        public async Task<List<Book>> AddBook(Booknew book)
        {
            _context.Books.Add(new Book { Title = book.Title, Author = book.Author, Isbn = book.Isbn, publishedDate = book.publishedDate });
            await _context.SaveChangesAsync();

            return await _context.Books.ToListAsync();
        }



        public async Task<Book> UpdateBook(int id, Booknew bookupdated)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return book;
            }

            var item = ValidateBook(book, bookupdated);

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

        public async Task<List<Book>> DeleteBook(int id)
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
            return _context.Books.Any(e => e.Id == id);
        }

        private Book ValidateBook(Book book, Booknew bookupdated)
        {

            if (bookupdated.Title.Length != 0) { book.Title = bookupdated.Title; }
            if (bookupdated.Author.Length != 0) { book.Author = bookupdated.Author; }
            if (bookupdated.Isbn.Length != 0) { book.Isbn = bookupdated.Isbn; }
            if (bookupdated.publishedDate != null && bookupdated.publishedDate!= DateOnly.MinValue) { book.publishedDate = bookupdated.publishedDate; }
            return book;
        }




    }
}
