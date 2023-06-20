using Homies.Data.Contracts;

namespace Homies.Data.Common;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
}