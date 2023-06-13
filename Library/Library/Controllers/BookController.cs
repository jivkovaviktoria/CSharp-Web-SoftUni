using System.Security.Claims;
using Library.Models.Entities.Book;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[Authorize]
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly ICategoryService _categoryService;

    public BookController(IBookService bookService, ICategoryService categoryService)
    {
        this._bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        this._categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    }

    public async Task<IActionResult> All()
    {
        var books = await this._bookService.All();
        return View(books);
    }

    public async Task<IActionResult> AddToCollection(int bookId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            await this._bookService.AddBookToUserCollection(userId, bookId);
        }
        catch (Exception e)
        {
            return this.BadRequest();
        }

        return RedirectToAction(nameof(All));
    }

    public async Task<IActionResult> RemoveFromCollection(int bookId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            await this._bookService.RemoveBookFromUserCollection(userId, bookId);
        }
        catch (Exception e)
        {
            return this.BadRequest();
        }

        return RedirectToAction(nameof(Mine));
    }

    public async Task<IActionResult> Mine()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var books = await this._bookService.GetUserBooks(userId);
        return View(books);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var categories = await this._categoryService.All();

        var bookFormModel = new BookInputModel()
        {
            Categories = categories
        };

        return View(bookFormModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(BookInputModel bookInputModel)
    {
        var validCategories = await this._categoryService.All();

        if (!validCategories.Any(c => c.Id == bookInputModel.CategoryId))
        {
            ModelState.AddModelError(nameof(bookInputModel.CategoryId), "Invalid category Id");
        }

        if (!ModelState.IsValid)
        {
            bookInputModel.Categories = validCategories;
            return View(bookInputModel);
        }

        await this._bookService.Add(bookInputModel);
        return RedirectToAction(nameof(All));
    }
}