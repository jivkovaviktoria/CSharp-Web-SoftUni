using Library.Data.Contracts;

namespace Library.Data.Common;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
}