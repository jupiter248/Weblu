using AutoMapper;
using Weblu.Application.DTOs.Common.TagDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Tags;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Services.Common
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TagService(IUnitOfWork unitOfWork, ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<TagDTO> CreateAsync(CreateTagDTO createTagDTO)
        {
            Tag tag = _mapper.Map<Tag>(createTagDTO);

            _tagRepository.Add(tag);
            await _unitOfWork.CommitAsync();

            TagDTO tagDTO = _mapper.Map<TagDTO>(tag);
            return tagDTO;
        }

        public async Task DeleteAsync(int tagId)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);

            tag.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<TagDTO>> GetAllAsync(TagParameters tagParameters)
        {
            IReadOnlyList<Tag> tags = await _tagRepository.GetAllAsync(tagParameters);
            List<TagDTO> tagDTOs = _mapper.Map<List<TagDTO>>(tags);
            return tagDTOs;
        }

        public async Task<TagDTO> GetByIdAsync(int tagId)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);
            TagDTO tagDTO = _mapper.Map<TagDTO>(tag);
            return tagDTO;
        }

        public async Task<TagDTO> UpdateAsync(int tagId, UpdateTagDTO updateTagDTO)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);
            tag = _mapper.Map(updateTagDTO, tag);

            tag.MarkUpdated();
            _tagRepository.Update(tag);
            await _unitOfWork.CommitAsync();

            TagDTO tagDTO = _mapper.Map<TagDTO>(tag);
            return tagDTO;
        }
    }
}