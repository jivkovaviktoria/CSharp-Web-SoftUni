using Library.Data.Common;
using Library.Data.Entities;
using Library.Models.Entities.Book;

namespace Library.Services.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookViewModel>> All();
    Task AddBookToUserCollection(string userId, int bookid);
    Task RemoveBookFromUserCollection(string userId, int bookId);
    Task<IEnumerable<BookViewModel>> GetUserBooks(string userId);

    Task Add(BookInputModel bookInputModel);
}