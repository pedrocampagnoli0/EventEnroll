using System;
using System.Reflection.PortableExecutable;
using EventEnroll.Models;
using EventEnroll.Services.EventService;
using Microsoft.AspNetCore.Mvc;

namespace EventEnroll.Controllers
{
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
    }
}
