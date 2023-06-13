using System.Linq.Expressions;
using Contacts.Data;
using Contacts.Data.Entities;
using Contacts.Models.Entities.Contact;
using Contacts.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Services;

public class ContactService : IContactService
{
    private readonly ContactsDbContext _dbContext;


    public ContactService(ContactsDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<ContactViewModel>> All()
    {
        var contacts = await this._dbContext.Contacts
            .Select(c => new ContactViewModel()
            {
                Id = c.Id,
                Address = c.Address,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Website = c.Website
            })
            .ToListAsync();
        
        return contacts;
    }

    public async Task<ContactViewModel> GetById(int contactId)
    {
        var contact = await this._dbContext.Contacts
            .Where(x => x.Id == contactId)
            .Select(c => new ContactViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Website = c.Website
            }).FirstOrDefaultAsync() ?? throw new ArgumentNullException(nameof(contactId), "Invalid contact id");

        return contact;
    }

    public async Task Update(int contactId, ContactViewModel contactInputModel)
    {
        var targetContact = await this._dbContext.Contacts.FindAsync(contactId);

        targetContact.FirstName = contactInputModel.FirstName;
        targetContact.LastName = contactInputModel.LastName;
        targetContact.Address = contactInputModel.Address;
        targetContact.Email = contactInputModel.Email;
        targetContact.Website = contactInputModel.Website;
        targetContact.PhoneNumber = contactInputModel.PhoneNumber;

        await this._dbContext.SaveChangesAsync();
    }

    public async Task AddToUserTeam(string userId, int contactId)
    {
        var contact = await this._dbContext.Contacts
                          .Include(c => c.ApplicationUsersContacts)
                          .FirstOrDefaultAsync(x => x.Id == contactId) ??
                      throw new ArgumentNullException(nameof(contactId), "Invalid contact id");
        
        if (contact.ApplicationUsersContacts.All(x => x.ApplicationUserId != userId))
        {
            contact.ApplicationUsersContacts.Add(new ApplicationUserContact(){ApplicationUserId = userId, ContactId = contactId});
            await this._dbContext.SaveChangesAsync();
        }
    }

    public async Task RemoveFromUserTeam(string userId, int contactId)
    {
        var contact = await this._dbContext.Contacts
                          .Include(c => c.ApplicationUsersContacts)
                          .FirstOrDefaultAsync(x => x.ApplicationUsersContacts.Any(uc => uc.ContactId == contactId)) ??
                      throw new ArgumentNullException(nameof(contactId), "Invalid contact id");

        var applicationUserContact = contact.ApplicationUsersContacts.FirstOrDefault(x => x.ApplicationUserId == userId);
        if (applicationUserContact is not null)
        {
            contact.ApplicationUsersContacts.Remove(applicationUserContact);
            await this._dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ContactViewModel>> GetByUser(string userId)
    {
        var contacts = await this._dbContext.Contacts
            .Include(c => c.ApplicationUsersContacts)
            .Where(c => c.ApplicationUsersContacts.Any(uc => uc.ApplicationUserId == userId))
            .Select(x => new ContactViewModel()
            {
                Address = x.Address,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                Website = x.Website
            }).ToListAsync();

        return contacts;
    }

    public async Task Add(ContactInputModel contactInputModel)
    {
        var contact = new Contact()
        {
            FirstName = contactInputModel.FirstName,
            LastName = contactInputModel.LastName,
            Email = contactInputModel.Email,
            PhoneNumber = contactInputModel.PhoneNumber,
            Address = contactInputModel.Address,
            Website = contactInputModel.Website
        };

        await this._dbContext.AddAsync(contact);
        await this._dbContext.SaveChangesAsync();
    }
}