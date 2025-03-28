using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Mappings;

public class NotificationMappingProfile: Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<NotificationDto, Notification>();

        CreateMap<Notification, NotificationDto>();
    }
}