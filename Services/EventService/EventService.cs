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
        private readonly UserManager<IdentityUser> _userManager;
        public EventService(IMapper mapper,IHttpContextAccessor httpContextAccessor, DataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        private string GetUserId() => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        public async Task<ServiceResponse<List<GetEventDto>>> AddEvent(AddEventDto newEvent)
        {
            var serviceResponse = new ServiceResponse<List<GetEventDto>>();
            var eventVar = _mapper.Map<Event>(newEvent);
            eventVar.CreatorId = GetUserId();
            //attaches the attendees to the event, and relates the names to the database list of users
            eventVar.Attendees = new List<IdentityUser>();
            foreach (var attendee in newEvent.Attendees)
            {
                var user = await _userManager.FindByNameAsync(attendee); // Use UserManager to find users
                if (user is null)
                    throw new Exception($"User '{attendee}' not found.");

                eventVar.Attendees.Add(user);
            }

            _context.Events.Add(eventVar);
            await _context.SaveChangesAsync();

            serviceResponse.Data = 
                await _context.Events
                .Where(c => c.CreatorId == GetUserId())
                .Select(c => _mapper.Map<GetEventDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEventDto>>> DeleteEvent(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetEventDto>>();
            try
            {
                var eventVar = await _context.Events
                    .FirstOrDefaultAsync(c => c.EventId == id && c.CreatorId! == GetUserId());
                if (eventVar is null)
                    throw new Exception($"Event with Id '{id}' not found.");

                _context.Events.Remove(eventVar);

                await _context.SaveChangesAsync();

                serviceResponse.Data = 
                    await _context.Events
                    .Where(c => c.CreatorId! == GetUserId())
                    .Select(c => _mapper.Map<GetEventDto>(c)).ToListAsync(); 
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
            var dbEvents = await _context.Events
                .Include(e => e.Attendees) // load attendees
                .Where(e => e.CreatorId == GetUserId())
                .ToListAsync();

            serviceResponse.Data = dbEvents.Select(e => _mapper.Map<GetEventDto>(e)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetEventDto>> GetEventById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEventDto>();
            var dbEvent = await _context.Events
                .Include(e => e.Attendees) // load attendees
                .FirstOrDefaultAsync(e => e.EventId == id && e.CreatorId == GetUserId());

            serviceResponse.Data = _mapper.Map<GetEventDto>(dbEvent);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventDto>> UpdateEvent(UpdateEventDto updatedEvent)
        {
            var serviceResponse = new ServiceResponse<GetEventDto>();
            try
            {
                var eventVar = await _context.Events
                    .Include(c => c.Creator)
                    .Include(c => c.Attendees) // Include attendees for updating
                    .FirstOrDefaultAsync(c => c.EventId == updatedEvent.EventId);

                if (eventVar is null || eventVar.CreatorId != GetUserId())
                    throw new Exception($"Event with Id '{updatedEvent.EventId}' not found.");

                // Update properties from updatedEvent
                _mapper.Map(updatedEvent, eventVar);
                eventVar.Title = updatedEvent.Title;
                eventVar.Description = updatedEvent.Description;
                eventVar.Date = updatedEvent.Date;
                var usertmp = await _userManager.FindByNameAsync(updatedEvent.CreatorName);
                // Check if Creator user exists, if it doesnt, throw exception
                if (usertmp is null)
                    throw new Exception($"Creator '{updatedEvent.CreatorName}' not found.");
                eventVar.CreatorId = usertmp.Id;

                

                // Update Attendees
                eventVar.Attendees.Clear(); // Clear existing attendees
                foreach (var attendee in updatedEvent.Attendees)
                {
                    var user = await _userManager.FindByNameAsync(attendee);
                    if (user is null)
                        throw new Exception($"User '{attendee}' not found.");

                    eventVar.Attendees.Add(user);
                }

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
