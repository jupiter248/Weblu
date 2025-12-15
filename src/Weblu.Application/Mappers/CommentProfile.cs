using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.CommentDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Comments;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Mappers
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