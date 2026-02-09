using AutoMapper;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Articles.CommentDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Articles.Comments;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Services.Articles
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
        public async Task<CommentDto> CreateAsync(string userId, CreateCommentDto createCommentDto)
        {
            Comment comment = _mapper.Map<Comment>(createCommentDto);
            Article article = await _articleRepository.GetByIdAsync(createCommentDto.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            CommentUserDto commentUserDto = await _userRepository.GetUserForCommentAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            if (createCommentDto.ParentCommentId.HasValue)
            {
                if (!article.Comments.Any(c => c.Id == createCommentDto.ParentCommentId))
                {
                    throw new NotFoundException(CommentErrorCodes.NotFound);
                }
            }
            comment.Article = article;
            comment.UserId = commentUserDto.UserId;

            _commentRepository.Add(comment);
            await _unitOfWork.CommitAsync();

            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            commentDto.User = commentUserDto;

            return commentDto;
        }

        public async Task DeleteAsync(string userId, int commentId)
        {
            Comment comment = await _commentRepository.GetByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
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

            comment.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<CommentDto>> GetAllAsync(CommentParameters commentParameters)
        {
            IReadOnlyList<Comment> comments = await _commentRepository.GetAllAsync(commentParameters);
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

        public async Task<PagedResponse<CommentDto>> GetAllPagedAsync(CommentParameters commentParameters)
        {
            PagedList<Comment> comments = await _commentRepository.GetAllAsync(commentParameters);
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
            var pagedResponse = _mapper.Map<PagedResponse<CommentDto>>(comments);
            pagedResponse.Items = commentDtos;
            return pagedResponse;
        }

        public async Task<CommentDto> GetByIdAsync(int commentId)
        {
            Comment comment = await _commentRepository.GetByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
            CommentUserDto commentUserDto = await _userRepository.GetUserForCommentAsync(comment.UserId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            commentDto.User = commentUserDto;
            return commentDto;
        }

        public async Task<CommentDto> EditAsync(string userId, int commentId, UpdateCommentDto updateCommentDto)
        {
            Comment comment = await _commentRepository.GetByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
            Article article = await _articleRepository.GetByIdAsync(comment.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            if (userId != comment.UserId)
            {
                throw new UnauthorizedException(CommentErrorCodes.UpdateForbidden);
            }
            if (!article.Comments.Any(c => c.Id == updateCommentDto.ParentCommentId))
            {
                throw new NotFoundException(CommentErrorCodes.NotFound);
            }
            comment = _mapper.Map(updateCommentDto, comment);
            
            comment.MarkUpdated();
            _commentRepository.Update(comment);
            await _unitOfWork.CommitAsync();

            CommentUserDto commentUserDto = await _userRepository.GetUserForCommentAsync(comment.UserId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            commentDto.User = commentUserDto;
            return commentDto;
        }
    }
}