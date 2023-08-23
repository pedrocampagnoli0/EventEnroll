using EventEnroll.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventEnroll.Services.EventService
{
    public class EventService : IEventService
    {
        private static List<Event> events = new List<Event> {
            new Event(),
            new Event{ Id = 1, Title = "Pedro's birthday", Date = DateTime.Now}
        };
        private readonly IMapper _mapper;
        public EventService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetEventDto>>> AddEvent(AddEventDto newEvent)
        {
            var serviceResponse = new ServiceResponse<List<GetEventDto>>();
            var eventVar = _mapper.Map<Event>(newEvent);
            eventVar.Id = events.Max(c => c.Id) + 1;
            events.Add(_mapper.Map<Event>(newEvent));
            serviceResponse.Data = events.Select(c => _mapper.Map<GetEventDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetEventDto>>> GetAllEvents()
        {
            var serviceResponse = new ServiceResponse<List<GetEventDto>>();
            serviceResponse.Data = events.Select(c => _mapper.Map<GetEventDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetEventDto>> GetEventById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEventDto>();
            var eventVar = events.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetEventDto>(eventVar);
            return serviceResponse;
        }
    }
}
