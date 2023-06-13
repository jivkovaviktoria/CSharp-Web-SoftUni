using Contacts.Data.Contracts;

namespace Contacts.Data.Common;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
}