using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.CommentDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Comments;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;



        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository, IArticleRepository articleRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommentDto> AddCommentAsync(string userId, AddCommentDto addCommentDto)
        {
            Comment comment = _mapper.Map<Comment>(addCommentDto);
            Article article = await _articleRepository.GetArticleByIdAsync(addCommentDto.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            CommentUserDto commentUserDto = await _userRepository.GetUserForCommentAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);

            if (!article.Comments.Any(c => c.Id == addCommentDto.ParentCommentId))
            {
                throw new NotFoundException(CommentErrorCodes.NotFound);
            }

            comment.Article = article;
            comment.UserId = commentUserDto.UserId;

            await _commentRepository.AddCommentAsync(comment);
            await _unitOfWork.CommitAsync();

            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            commentDto.User = commentUserDto;

            return commentDto;
        }

        public async Task DeleteCommentAsync(string userId, int commentId)
        {
            Comment comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
            bool userExists = await _userRepository.UserExistsAsync(userId);
            bool isAdmin = await _userRepository.IsAdminAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            if (userId != comment.UserId && !isAdmin)
            {
                throw new UnauthorizedException(CommentErrorCodes.DeleteForbidden);
            }

            _commentRepository.DeleteComment(comment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<CommentDto>> GetAllCommentsAsync(CommentParameters commentParameters)
        {
            IReadOnlyList<Comment> comments = await _commentRepository.GetAllCommentsAsync(commentParameters);
            List<CommentDto> commentDtos = _mapper.Map<List<CommentDto>>(comments);
            foreach (Comment comment in comments)
            {
                foreach (CommentDto commentDto in commentDtos)
                {
                    if (commentDto.Id == comment.Id)
                    {
                        commentDto.User = await _userRepository.GetUserForCommentAsync(comment.UserId) ?? default!;
                        break;
                    }
                }
            }
            return commentDtos;
        }

        public async Task<CommentDto> GetCommentByIdAsync(int commentId)
        {
            Comment comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
            CommentUserDto commentUserDto = await _userRepository.GetUserForCommentAsync(comment.UserId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            commentDto.User = commentUserDto;
            return commentDto;
        }

        public async Task<CommentDto> UpdateCommentAsync(string userId, int commentId, UpdateCommentDTo updateCommentDTo)
        {
            Comment comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
            Article article = await _articleRepository.GetArticleByIdAsync(comment.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            if (userId != comment.UserId)
            {
                throw new UnauthorizedException(CommentErrorCodes.UpdateForbidden);
            }
            if (!article.Comments.Any(c => c.Id == updateCommentDTo.ParentCommentId))
            {
                throw new NotFoundException(CommentErrorCodes.NotFound);
            }
            comment = _mapper.Map(updateCommentDTo, comment);
            _commentRepository.UpdateComment(comment);
            await _unitOfWork.CommitAsync();

            CommentUserDto commentUserDto = await _userRepository.GetUserForCommentAsync(comment.UserId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            commentDto.User = commentUserDto;
            return commentDto;
        }
    }
}