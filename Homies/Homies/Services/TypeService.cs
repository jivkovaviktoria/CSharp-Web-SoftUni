using Homies.Data;
using Homies.Data.Contracts.Services;
using Homies.Models.Type;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services;

public class TypeService : ITypeService
{
    private readonly HomiesDbContext _context;

    public TypeService(HomiesDbContext context)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<IEnumerable<TypeViewModel>> GetManyAsync()
    {
        var result = await this._context.Types
            .Select(x => new TypeViewModel()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
        return result;
    }
}