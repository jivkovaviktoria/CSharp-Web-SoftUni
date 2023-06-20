using Homies.Models.Type;

namespace Homies.Data.Contracts.Services;
public interface ITypeService
{
    Task<IEnumerable<TypeViewModel>> GetManyAsync();
}