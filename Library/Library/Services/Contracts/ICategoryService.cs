using Library.Data.Entities;
using Library.Models.Entities;
using Library.Models.Entities.Category;

namespace Library.Services.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> All();
}