using System.Security.Claims;
using Contacts.Data.Entities;
using Contacts.Models.Entities.Contact;
using Contacts.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers;

[Authorize]
public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var contacts = await this._contactService.All();
        return View(contacts);
    }

    [HttpGet]
    public IActionResult Add()
    {
        var contactInputModel = new ContactInputModel();
        return View(contactInputModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ContactInputModel contactInputModel)
    { 
        if (!ModelState.IsValid) return View(contactInputModel);

        await this._contactService.Add(contactInputModel);
        return RedirectToAction(nameof(All));
    }

    public async Task<IActionResult> Edit(int contactId)
    {
        var contact = await this._contactService.GetById(contactId);
        if (contact is null) return NotFound();
        return View(contact);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ContactViewModel contactViewModel)
    {
        await this._contactService.Update(contactViewModel.Id, contactViewModel);
        var newContact = await this._contactService.GetById(contactViewModel.Id);
        return View(newContact);
    }

    public async Task<IActionResult> Team()
    {
        var userId = this.GetUserId();
        var contacts = await this._contactService.GetByUser(userId);
        return View(contacts);
    }

    public async Task<IActionResult> AddToTeam(int contactId)
    {
        var userId = this.GetUserId();
        await this._contactService.AddToUserTeam(userId, contactId);
        return RedirectToAction(nameof(All));
    }

    public async Task<IActionResult> RemoveFromTeam(int contactId)
    {
        var userId = this.GetUserId();
        await this._contactService.RemoveFromUserTeam(userId, contactId);
        return RedirectToAction(nameof(Team));
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
}