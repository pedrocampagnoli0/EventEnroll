using System;
using System.Reflection.PortableExecutable;
using EventEnroll.Models;
using EventEnroll.Services.EventService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventEnroll.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {

        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetEventDto>>>> Get()
        {
            return Ok(await _eventService.GetAllEvents());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetEventDto>>> GetSingle(int id)
        {
            return Ok(await _eventService.GetEventById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetEventDto>>>> AddEvent(AddEventDto newEvent)
        {
            return Ok(await _eventService.AddEvent(newEvent));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetEventDto>>>> UpdateEvent(UpdateEventDto updatedEvent)
        {
            var response = await _eventService.UpdateEvent(updatedEvent);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetEventDto>>> DeleteEvent(int id)
        {
            var response = await _eventService.DeleteEvent(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok();
        }
    }
}
