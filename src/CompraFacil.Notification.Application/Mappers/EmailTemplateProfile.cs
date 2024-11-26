using AutoMapper;
using CompraFacil.Notification.Application.Handlers.v1.CreateTemplate;
using CompraFacil.Notification.Domain.Entities;

namespace CompraFacil.Notification.Application.Mappers;

public sealed class EmailTemplateProfile : Profile
{
    public EmailTemplateProfile()
    {
        _ = CreateMap<CreateEmailTemplateCommand, EmailTemplate>()
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Payload!.Subject))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Payload!.Content));


        _ = CreateMap<EmailTemplate, CreateEmailTemplateResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
    }

}
