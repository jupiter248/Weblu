using AutoMapper;
using Weblu.Application.Dtos.Common.TagDtos;
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
        public async Task<TagDto> CreateAsync(CreateTagDto createTagDto)
        {
            Tag tag = _mapper.Map<Tag>(createTagDto);

            _tagRepository.Add(tag);
            await _unitOfWork.CommitAsync();

            TagDto tagDto = _mapper.Map<TagDto>(tag);
            return tagDto;
        }

        public async Task DeleteAsync(int tagId)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);

            tag.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<TagDto>> GetAllAsync(TagParameters tagParameters)
        {
            IReadOnlyList<Tag> tags = await _tagRepository.GetAllAsync(tagParameters);
            List<TagDto> tagDtos = _mapper.Map<List<TagDto>>(tags);
            return tagDtos;
        }

        public async Task<TagDto> GetByIdAsync(int tagId)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);
            TagDto tagDto = _mapper.Map<TagDto>(tag);
            return tagDto;
        }

        public async Task<TagDto> UpdateAsync(int tagId, UpdateTagDto updateTagDto)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);
            tag = _mapper.Map(updateTagDto, tag);

            tag.MarkUpdated();
            _tagRepository.Update(tag);
            await _unitOfWork.CommitAsync();

            TagDto tagDto = _mapper.Map<TagDto>(tag);
            return tagDto;
        }
    }
}