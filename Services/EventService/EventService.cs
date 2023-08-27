using EventEnroll.Data;
using EventEnroll.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventEnroll.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EventService(IMapper mapper,IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId() => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        public async Task<ServiceResponse<List<GetEventDto>>> AddEvent(AddEventDto newEvent)
        {
            var serviceResponse = new ServiceResponse<List<GetEventDto>>();
            var eventVar = _mapper.Map<Event>(newEvent);

            _context.Events.Add(eventVar);
            await _context.SaveChangesAsync();

            serviceResponse.Data = 
                await _context.Events.Select(c => _mapper.Map<GetEventDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEventDto>>> DeleteEvent(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetEventDto>>();
            try
            {
                var eventVar = await _context.Events.FirstOrDefaultAsync(c => c.EventId == id);
                if (eventVar is null)
                    throw new Exception($"Event with Id '{id}' not found.");

                _context.Events.Remove(eventVar);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Events.Select(c => _mapper.Map<GetEventDto>(c)).ToListAsync(); 
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEventDto>>> GetAllEvents()
        {
            var serviceResponse = new ServiceResponse<List<GetEventDto>>();
            var dbEvents = await _context.Events.Where(c => c.CreatorId == GetUserId()).ToListAsync();
            serviceResponse.Data = dbEvents.Select(c => _mapper.Map<GetEventDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetEventDto>> GetEventById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEventDto>();
            var dbEvent = await _context.Events.FirstOrDefaultAsync(c => c.EventId == id);
            serviceResponse.Data = _mapper.Map<GetEventDto>(dbEvent);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventDto>> UpdateEvent(UpdateEventDto updatedEvent)
        {

            var serviceResponse = new ServiceResponse<GetEventDto>();
            try
            {
                var eventVar = await _context.Events.FirstOrDefaultAsync(c => c.EventId == updatedEvent.EventId);
                if (eventVar is null)
                    throw new Exception($"Event with Id '{updatedEvent.EventId}' not found.");

                _mapper.Map(updatedEvent, eventVar);

                eventVar.Title = updatedEvent.Title;
                eventVar.Description = updatedEvent.Description;
                eventVar.Date = updatedEvent.Date;
                eventVar.Attendees = updatedEvent.Attendees;
                eventVar.CreatorId = updatedEvent.CreatorId;
                eventVar.Creator = updatedEvent.Creator;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetEventDto>(eventVar);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
