using System.Linq.Expressions;
using Homies.Data;
using Homies.Data.Contracts.Services;
using Homies.Data.Entities;
using Homies.Models.Event;
using Homies.Models.Type;
using Homies.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services;

public class EventService : IEventService
{
    private readonly HomiesDbContext _context;

    public EventService(HomiesDbContext context)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<IEnumerable<EventViewModel>> GetManyAsync(Expression<Func<Event, bool>>? filter = null)
    {
        var result = await this._context.Events.Filter(filter)
            .Select(x => new EventViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Start = x.Start,
                Type = x.Type.Name,
                Organiser = x.Organiser.UserName
            })
            .ToListAsync();
        return result;
    }

    public async Task<Event> GetAsync(Expression<Func<Event, bool>> filter)
    {
        var result = await this._context.Events.Filter(filter)
            .Include(x => x.Organiser)
            .Include(x => x.Type)
            
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task CreateAsync(string userId, EventInputModel eventModel)
    {
        var eventEntity = new Event()
        {
            Name = eventModel.Name,
            Description = eventModel.Description,
            Start = DateTime.Parse(eventModel.Start),
            End = DateTime.Parse(eventModel.End),
            TypeId = eventModel.TypeId,
            CreatedOn = DateTime.Now,
            OrganiserId = userId
        };
        
        this._context.Events.Add(eventEntity);
        await this._context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventInputModel eventModel)
    {
        var targetEvent = await this._context.Events.FindAsync(eventModel.Id);

        targetEvent.Name = eventModel.Name;
        targetEvent.Description = eventModel.Description;
        targetEvent.Start = DateTime.Parse(eventModel.Start);
        targetEvent.End = DateTime.Parse(eventModel.End);
        targetEvent.TypeId = eventModel.TypeId;

        await this._context.SaveChangesAsync();
    }

    public async Task Leave(string userId, int id)
    {
        var target = this._context
            .Events
            .Include(x => x.EventsParticipants).FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException(nameof(id));

        var eventParticipant = target.EventsParticipants.FirstOrDefault(x => x.EventId == id);
        if (eventParticipant is not null)
        {
            target.EventsParticipants.Remove(eventParticipant);
            await this._context.SaveChangesAsync();
        }
    }

    public async Task Join(string userId, int id)
    {
        var target = this._context
            .Events
            .Include(x => x.EventsParticipants).FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException(nameof(id));
        
        if (target.EventsParticipants.All(x => x.HelperId != userId))
        {
            target.EventsParticipants.Add(new EventParticipant(){HelperId = userId, EventId = id});
            await this._context.SaveChangesAsync();
        }
    }

}