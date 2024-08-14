using Library_API.Data;
using Library_API.Models;
using Library_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Library_API.Controllers
{

    [ApiController]
    public class LibraryAPIController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IBookRepository _bookRepository;


        public LibraryAPIController(DataContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;

        }


        /// <summary>
        /// Get all books
        /// </summary>
        /// <remarks>Retrieve a list of all books in the library..</remarks>
        [HttpGet("books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BookWithId>>> GetBooks()
        {
            var BookList = new List<BookWithId>();

            BookList = await _bookRepository.GetAllBooks();

            return Ok(BookList);
        }


        /// <summary>
        /// Add a new book.
        /// </summary>
        /// <remarks>Add a new book to the library.</remarks>
        [HttpPost("book")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<BookWithId>> AddBook([BindRequired] Book book)
        {
            var Book = new BookWithId();

            Book = await _bookRepository.AddBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = Book.id }, Book);

        }


        /// <summary>
        /// Get a specific book
        /// </summary>
        /// <remarks>Retrieve a specific book by its ID.</remarks>
        [HttpGet("book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookWithId>> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookById(id);

            if (book == null)
            {
                var result = JsonSerializer.Serialize(new { message = "The Book id provided is not found/valid." });
                return NotFound(result);
            }

            return Ok(book) ;
        }


        /// <summary>
        /// Update a book
        /// </summary>
        /// <remarks>Update an existing book by its ID..</remarks>
        [HttpPut("book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookWithId>> PutBook(int id, [BindRequired] Book bookModified)
        {
            var book = await _bookRepository.UpdateBook(id, bookModified);

            if (book == null)
            {
                var result = JsonSerializer.Serialize(new { message = "The Book id provided is not found/valid." });
                return NotFound(result);
            }

            return Ok(book);
        }


        /// <summary>
        /// Delete a book
        /// </summary>
        /// <remarks>Delete an existing book by its ID..</remarks>
        [HttpDelete("book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BookWithId>>> DeleteBook(int id)
        {
            var books =  new List<BookWithId>();

            books = await _bookRepository.DeleteBook(id);

            if (books == null)
            {
                var result = JsonSerializer.Serialize(new { message = "The Book id provided is not found/valid." });
                return NotFound(result);
            }

            return Ok(books);
        }

    }
}
