using Library_API.Models;

namespace Library_API.Services
{
    public interface IValidateBook
    {
        public BookWithId Validate(BookWithId book,Book bookUpdated);
    }
}
