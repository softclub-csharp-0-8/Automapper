using AutoMapper;
using Domain.Entities;

namespace Infrastructure.AutomapperProfiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Book, GetBookDto>()
        .ForMember(dest=>dest.PublishedDate,opt=>opt.MapFrom(src=>src.PubDate))
        .ForMember(dest=>dest.PublisherName,opt=>opt.MapFrom(src=>src.Publisher.Name));

        CreateMap<AddBookDto, Book>();
        CreateMap<Author, AuthorBaseDto>();
        CreateMap<Book, GetBookDto>();
        CreateMap<Book, AddBookDto>();
        
        CreateMap<IGrouping<Book,Author>, GetBookDto>()
            .ForMember(dest=>dest.AuthorNames,opt=>opt.MapFrom(src=>src.ToList()));
        
    }
}