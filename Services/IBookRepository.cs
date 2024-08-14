using Library_API.Data;
using Library_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_API.Services
{
    public interface IBookRepository
    {
        Task<List<BookWithId>> GetAllBooks();
        Task<BookWithId> GetBookById(int id);
        Task<BookWithId> AddBook(Book book);
        Task<BookWithId> UpdateBook(int id, Book book);
        Task<List<BookWithId>> DeleteBook(int id);

    }

}
