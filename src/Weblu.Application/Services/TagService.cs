using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.TagDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Tags;
using Weblu.Domain.Errors.Tags;

namespace Weblu.Application.Services
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
        public async Task<TagDto> AddTagAsync(AddTagDto addTagDto)
        {
            Tag tag = _mapper.Map<Tag>(addTagDto);

            _tagRepository.Add(tag);
            await _unitOfWork.CommitAsync();

            TagDto tagDto = _mapper.Map<TagDto>(tag);
            return tagDto;
        }

        public async Task DeleteTagAsync(int tagId)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);

            _tagRepository.Delete(tag);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<TagDto>> GetAllTagsAsync(TagParameters tagParameters)
        {
            IReadOnlyList<Tag> tags = await _tagRepository.GetAllAsync(tagParameters);
            List<TagDto> tagDtos = _mapper.Map<List<TagDto>>(tags);
            return tagDtos;
        }

        public async Task<TagDto> GetTagByIdAsync(int tagId)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);
            TagDto tagDto = _mapper.Map<TagDto>(tag);
            return tagDto;
        }

        public async Task<TagDto> UpdateTagAsync(int tagId, UpdateTagDto updateTagDto)
        {
            Tag tag = await _tagRepository.GetByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.NotFound);
            tag = _mapper.Map(updateTagDto, tag);

            _tagRepository.Update(tag);
            await _unitOfWork.CommitAsync();

            TagDto tagDto = _mapper.Map<TagDto>(tag);
            return tagDto;
        }
    }
}