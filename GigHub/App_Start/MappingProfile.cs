using AutoMapper;
using GigHub.Controllers.Api;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ApplicationUser, UserDto>();
            this.CreateMap<Genre, GenreDto>();
            this.CreateMap<Gig, GigDto>();
            this.CreateMap<Notification, NotificationDto>();
        }
    }
}