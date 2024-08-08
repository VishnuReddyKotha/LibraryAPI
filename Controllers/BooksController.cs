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
        private readonly IBookRepository _bookRepository;
        

        public BooksController(DataContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Booknew book)
        {
            var BookList = new List<Book>();

            BookList = await _bookRepository.AddBook(book);

            return Ok(BookList);

        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
           var BookList = new List<Book>();

           BookList = await _bookRepository.GetAll();

           return Ok(BookList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return book ;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> PutBook(int id, Booknew bookupdated)
        {
            var book = await _bookRepository.UpdateBook(id, bookupdated);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int id)
        {
            var book =  new List<Book>();

            book = await _bookRepository.DeleteBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

    }
}
