using AutoMapper;
using Weblu.Application.DTOs.Articles.CommentDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Articles.Comments;

namespace Weblu.Application.Mappers.Articles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>()
                    .ForMember(dest => dest.IsEdited, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<UpdateCommentDTO, Comment>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now))
                    .ForMember(dest => dest.IsUpdated, opt => opt.MapFrom(src => true));
        }
    }
}