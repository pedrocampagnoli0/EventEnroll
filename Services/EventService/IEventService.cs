using EventEnroll.Models;

namespace EventEnroll.Services.EventService
{
    public interface IEventService
    {
        Task<ServiceResponse<List<Event>>> GetAllEvents();
        Task<ServiceResponse<Event>> GetEventById(int id);
        Task<ServiceResponse<List<Event>>> AddEvent(Event newEvent);
    }
}
