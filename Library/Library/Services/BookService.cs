using Library.Data;
using Library.Data.Entities;
using Library.Models.Entities.Book;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _dbContext;

    public BookService(LibraryDbContext dbContext)
    {
        this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<IEnumerable<BookViewModel>> All()
    {
        var books = await this._dbContext.Books
            .Include(b => b.Category)
            .Select(x => new BookViewModel()
            {
                Id = x.Id,
                Author = x.Author,
                Category = x.Category.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Title = x.Title
            })
            .ToListAsync();
        
        return books;
    }

    public async Task AddBookToUserCollection(string userId, int bookId)
    {
        var book = await this._dbContext.Books
            .Include(b => b.UsersBooks)
            .FirstOrDefaultAsync(b => b.Id == bookId) ?? throw new ArgumentNullException(nameof(bookId), "Invalid book");

        if (book.UsersBooks.All(u => u.BookId != bookId))
        {
            book.UsersBooks.Add(new ApplicationUserBook(){CollectorId = userId, BookId = bookId});
        }

        await this._dbContext.SaveChangesAsync();
    }

    public async Task RemoveBookFromUserCollection(string userId, int bookId)
    {
        var book = await this._dbContext.Books
            .Include(b => b.UsersBooks)
            .FirstOrDefaultAsync(b => b.Id == bookId) ?? throw new ArgumentNullException(nameof(bookId));

        var applicationUserBook = book.UsersBooks.FirstOrDefault(ub => ub.BookId == bookId);
        if (applicationUserBook is not null)
        {
            book.UsersBooks.Remove(applicationUserBook);
            await this._dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<BookViewModel>> GetUserBooks(string userId)
    {
        var userBooks = await this._dbContext.Books
            .Include(b => b.UsersBooks)
            .Where(b => b.UsersBooks.Any(x => x.CollectorId == userId))
            .Select(b => new BookViewModel()
            {
                Id = b.Id,
                Author = b.Author,
                Category = b.Category.Name,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                Title = b.Title
            })
            .ToListAsync();

        return userBooks;
    }

    public async Task Add(BookInputModel bookInputModel)
    {
        var book = new Book
        {
            Author = bookInputModel.Author,
            CategoryId = bookInputModel.CategoryId,
            Description = bookInputModel.Description,
            ImageUrl = bookInputModel.ImageUrl,
            Rating = bookInputModel.Rating,
            Title = bookInputModel.Title
        };

        await this._dbContext.Books.AddAsync(book);
        await this._dbContext.SaveChangesAsync();
    }
}