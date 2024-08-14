using Library_API.Models;

namespace Library_API.Services
{
    public class ValidateBook : IValidateBook
    {

        public BookWithId Validate(BookWithId book, Book bookUpdated)
        {

            if (bookUpdated.title.Length != 0) { book.title = bookUpdated.title; }
            if (bookUpdated.author.Length != 0) { book.author = bookUpdated.author; }
            if (bookUpdated.isbn.Length != 0) { book.isbn = bookUpdated.isbn; }
            if (bookUpdated.publishedDate != DateOnly.MinValue) { book.publishedDate = bookUpdated.publishedDate; }
            return book;
        }


    }
}
