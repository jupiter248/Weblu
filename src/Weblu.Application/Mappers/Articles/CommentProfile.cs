using AutoMapper;
using Weblu.Application.Dtos.Articles.CommentDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Articles.Comments;

namespace Weblu.Application.Mappers.Articles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<AddCommentDto, Comment>();
            CreateMap<UpdateCommentDTo, Comment>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now))
                    .ForMember(dest => dest.IsEdited, opt => opt.MapFrom(src => true));
        }
    }
}