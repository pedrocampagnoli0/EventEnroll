using EventEnroll.Dtos.Event;
using EventEnroll.Models;

namespace EventEnroll.Services.EventService
{
    public interface IEventService
    {
        Task<ServiceResponse<List<GetEventDto>>> GetAllEvents();
        Task<ServiceResponse<GetEventDto>> GetEventById(int id);
        Task<ServiceResponse<List<GetEventDto>>> AddEvent(AddEventDto newEvent);
    }
}
