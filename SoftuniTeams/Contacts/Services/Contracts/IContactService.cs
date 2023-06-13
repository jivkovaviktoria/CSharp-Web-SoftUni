using Contacts.Models.Entities.Contact;

namespace Contacts.Services.Contracts;

public interface IContactService
{
    Task<IEnumerable<ContactViewModel>> All();
    Task AddToUserTeam(string userId, int contactId);
    Task RemoveFromUserTeam(string userId, int contactId);
    Task<IEnumerable<ContactViewModel>> GetByUser(string userId);
    Task Add(ContactInputModel contactInputModel);
    Task<ContactViewModel> GetById(int contactId);
    Task Update(int contactId, ContactViewModel contactInputModel);
}