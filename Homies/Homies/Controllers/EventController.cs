using System.Security.Claims;
using Homies.Data;
using Homies.Data.Contracts.Services;
using Homies.Models.Event;
using Homies.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homies.Controllers;

[Authorize]
public class EventController : Controller
{
    private readonly IEventService _eventService;
    private readonly ITypeService _typeService;

    public EventController(IEventService eventService, ITypeService typeService)
    {
        this._eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        this._typeService = typeService ?? throw new ArgumentNullException(nameof(typeService));
    }
    
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await this._eventService.GetManyAsync(null);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new EventInputModel();
        model.Types = await this._typeService.GetManyAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(EventInputModel eventInputModel)
    {
        if (!ModelState.IsValid)
        {
            eventInputModel.Types = await this._typeService.GetManyAsync();
            return View(eventInputModel);
        }

        var userId = this.GetUserId();
        await this._eventService.CreateAsync(userId, eventInputModel);
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var eventEntity = await this._eventService.GetAsync(x => x.Id ==id);
        if(eventEntity is null) ModelState.AddModelError(string.Empty, "Invalid event id");

        var model = new EventInputModel()
        {
            Id = eventEntity.Id,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            Start = eventEntity.Start.ToString(),
            End = eventEntity.End.ToString(),
            Types = await this._typeService.GetManyAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EventInputModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await this._eventService.UpdateAsync(model);
        return RedirectToAction(nameof(All));
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var result = await this._eventService.GetAsync(x => x.Id == id);
        var model = new EventFullViewModel()
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            CreatedOn = result.CreatedOn,
            End = result.End,
            Start = result.Start,
            Organiser = result.Organiser.UserName,
            Type = result.Type.Name
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Leave(int id)
    {
        var userId = this.GetUserId();

        try
        {
            await this._eventService.Leave(userId, id);
        }
        catch (Exception e)
        {
            return this.BadRequest();
        }

        return RedirectToAction(nameof(Joined));
    }

    [HttpGet]
    public async Task<IActionResult> Joined()
    {
        var userId = this.GetUserId();
        var result = await this._eventService.GetManyAsync(x => x.EventsParticipants.Any(x => x.HelperId == userId));
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Join(int id)
    {
        var userId = this.GetUserId();

        try
        {
            await this._eventService.Join(userId, id);
        }
        catch (Exception e)
        {
            return this.BadRequest();
        }

        return RedirectToAction(nameof(Joined));
    }
    
    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
}