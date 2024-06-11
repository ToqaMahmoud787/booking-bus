using AutoMapper;
using Bus.Core.Models;
using BusTraveller.Dtos;

namespace BusTraveller.Helpers
{
	public class MappingProfile:Profile
	{
        public MappingProfile()
        {
			CreateMap<Role, UserRoleDto>();
			CreateMap<RegisterDto, User>();
			CreateMap<DestinationDto, Destination>();
			CreateMap<AppointmentDto, Appointment>();
            CreateMap<RequestDto, Request>();
            CreateMap<requestDtotraveller, Request>();
            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<TravellerHistorySearchDto, TravellerHistorySearch>().ReverseMap();
            CreateMap<TravellerHistorySearch, TravellerHistorySearchDto>();
            CreateMap<Destination, DestinationDto>();
            CreateMap<Appointment, AppointmentDto>();

            CreateMap<User, UserRoleDto>();

        }
    }
}
