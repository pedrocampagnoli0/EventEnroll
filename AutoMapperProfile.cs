namespace EventEnroll
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Event, GetEventDto>();
            CreateMap<AddEventDto, Event>();
        }
    }
}
