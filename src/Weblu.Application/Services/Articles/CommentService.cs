using AutoMapper;
using Weblu.Domain.Common.Models;
using Weblu.Application.Common.Models;
using Weblu.Application.DTOs.Articles.CommentDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Domain.Interfaces.Repositories;
using Weblu.Domain.Interfaces.Repositories.Articles;
using Weblu.Domain.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Articles.Comments;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Users;
using Weblu.Domain.Entities.Users;
using Weblu.Application.Common.Services;

namespace Weblu.Application.Services.Articles
{
    public class CommentService : BaseService, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;



        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository, IArticleRepository articleRepository) : base(userRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CommentDTO> CreateAsync(string userId, CreateCommentDTO createCommentDTO)
        {
            await EnsureUserExistsAsync(userId);

            Article article = await _articleRepository.GetByIdAsync(createCommentDTO.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            User user = await _userRepository.GetUserAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            if (createCommentDTO.ParentCommentId.HasValue)
            {
                if (!article.Comments.Any(c => c.Id == createCommentDTO.ParentCommentId))
                {
                    throw new NotFoundException(CommentErrorCodes.NotFound);
                }
            }

            Comment comment = _mapper.Map<Comment>(createCommentDTO);
            comment.Article = article;
            comment.UserId = user.Id;

            _commentRepository.Add(comment);
            await _unitOfWork.CommitAsync();

            CommentDTO commentDTO = _mapper.Map<CommentDTO>(comment);
            commentDTO.User = user;

            return commentDTO;
        }

        public async Task DeleteAsync(string userId, int commentId)
        {
            await EnsureUserExistsAsync(userId);


            Comment comment = await _commentRepository.GetByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
            bool isAdmin = await _userRepository.IsAdminAsync(userId);
            if (userId != comment.UserId && !isAdmin)
            {
                throw new UnauthorizedException(CommentErrorCodes.DeleteForbidden);
            }

            comment.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<CommentDTO>> GetAllAsync(CommentParameters commentParameters)
        {

            IReadOnlyList<Comment> comments = await _commentRepository.GetAllAsync(commentParameters);
            List<CommentDTO> commentDTOs = _mapper.Map<List<CommentDTO>>(comments);
            foreach (Comment comment in comments)
            {
                foreach (CommentDTO commentDTO in commentDTOs)
                {
                    if (commentDTO.Id == comment.Id)
                    {
                        User user = await _userRepository.GetUserAsync(comment.UserId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
                        break;
                    }
                }
            }
            return commentDTOs;
        }

        public async Task<PagedResponse<CommentDTO>> GetAllPagedAsync(CommentParameters commentParameters)
        {
            PagedList<Comment> comments = await _commentRepository.GetAllAsync(commentParameters);
            List<CommentDTO> commentDTOs = _mapper.Map<List<CommentDTO>>(comments);
            foreach (Comment comment in comments)
            {
                foreach (CommentDTO commentDTO in commentDTOs)
                {
                    if (commentDTO.Id == comment.Id)
                    {
                        commentDTO.User = await _userRepository.GetUserAsync(comment.UserId) ?? default!;
                        break;
                    }
                }
            }
            var pagedResponse = _mapper.Map<PagedResponse<CommentDTO>>(comments);
            pagedResponse.Items = commentDTOs;
            return pagedResponse;
        }

        public async Task<CommentDTO> GetByIdAsync(int commentId)
        {
            Comment comment = await _commentRepository.GetByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
            User user = await _userRepository.GetUserAsync(comment.UserId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            CommentDTO commentDTO = _mapper.Map<CommentDTO>(comment);
            commentDTO.User = user;
            return commentDTO;
        }

        public async Task<CommentDTO> EditAsync(string userId, int commentId, UpdateCommentDTO updateCommentDTO)
        {
            await EnsureUserExistsAsync(userId);

            Comment comment = await _commentRepository.GetByIdAsync(commentId) ?? throw new NotFoundException(CommentErrorCodes.NotFound);
            Article article = await _articleRepository.GetByIdAsync(comment.ArticleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);

            if (userId != comment.UserId)
            {
                throw new UnauthorizedException(CommentErrorCodes.UpdateForbidden);
            }
            if (!article.Comments.Any(c => c.Id == updateCommentDTO.ParentCommentId))
            {
                throw new NotFoundException(CommentErrorCodes.NotFound);
            }
            comment = _mapper.Map(updateCommentDTO, comment);

            comment.MarkUpdated();
            _commentRepository.Update(comment);
            await _unitOfWork.CommitAsync();

            User user = await _userRepository.GetUserAsync(comment.UserId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            CommentDTO commentDTO = _mapper.Map<CommentDTO>(comment);
            commentDTO.User = user;
            return commentDTO;
        }
    }
}