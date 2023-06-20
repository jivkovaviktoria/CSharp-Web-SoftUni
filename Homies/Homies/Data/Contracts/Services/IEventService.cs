using System.Linq.Expressions;
using Homies.Data.Entities;
using Homies.Models.Event;

namespace Homies.Data.Contracts.Services;

public interface IEventService
{
    Task<IEnumerable<EventViewModel>> GetManyAsync(Expression<Func<Event, bool>>? filter);
    Task<Event> GetAsync(Expression<Func<Event, bool>> filter);
    Task CreateAsync(string userId, EventInputModel eventModel);
    Task UpdateAsync(EventInputModel eventModel);
    Task Leave(string userId, int id);
    Task Join(string userId, int id);
}