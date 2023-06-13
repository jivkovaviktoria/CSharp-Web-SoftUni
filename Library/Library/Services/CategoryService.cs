using Library.Data;
using Library.Data.Entities;
using Library.Models.Entities;
using Library.Models.Entities.Category;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class CategoryService : ICategoryService
{
    private readonly LibraryDbContext _dbContext;

    public CategoryService(LibraryDbContext dbContext)
    {
        this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<IEnumerable<CategoryViewModel>> All()
    {
        var categories = await this._dbContext.Categories
            .Select(x => new CategoryViewModel(){Id = x.Id, Name = x.Name})
            .ToListAsync();
        
        return categories;
    }
}