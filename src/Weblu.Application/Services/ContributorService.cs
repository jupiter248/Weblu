using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Common.Dtos;
using Weblu.Application.Dtos.ContributorDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.Contributors;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Services
{
    public class ContributorService : IContributorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public ContributorService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
        }
        public async Task<ContributorDto> AddContributorAsync(AddContributorDto addContributorDto)
        {
            Contributor newContributor = _mapper.Map<Contributor>(addContributorDto);

            await _unitOfWork.Contributors.AddContributorAsync(newContributor);
            await _unitOfWork.CommitAsync();

            ContributorDto contributorDto = _mapper.Map<ContributorDto>(newContributor);
            return contributorDto;
        }

        public async Task DeleteContributorAsync(int contributorId)
        {
            Contributor contributor = await _unitOfWork.Contributors.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (!string.IsNullOrEmpty(contributor.ProfileImageUrl))
            {
                await MediaManager.DeleteMedia(_webHost, contributor.ProfileImageUrl);
            }

            _unitOfWork.Contributors.DeleteContributor(contributor);
            await _unitOfWork.CommitAsync();

        }

        public async Task DeleteContributorProfileAsync(int contributorId)
        {
            Contributor contributor = await _unitOfWork.Contributors.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (string.IsNullOrEmpty(contributor.ProfileImageUrl))
            {
                throw new BadRequestException(ContributorErrorCodes.ContributorProfileIsEmpty);
            }
            else
            await MediaManager.DeleteMedia(_webHost, contributor.ProfileImageUrl);

            contributor.ProfileImageUrl = null;
            contributor.ProfileImageAltText = null;

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ContributorDto>> GetAllContributorsAsync(ContributorParameters contributorParameters)
        {
            IReadOnlyList<Contributor> contributors = await _unitOfWork.Contributors.GetAllContributorsAsync(contributorParameters);
            List<ContributorDto> contributorDtos = _mapper.Map<List<ContributorDto>>(contributors);
            return contributorDtos;
        }

        public async Task<ContributorDto> GetContributorByIdAsync(int contributorId)
        {
            Contributor contributor = await _unitOfWork.Contributors.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            ContributorDto contributorDto = _mapper.Map<ContributorDto>(contributor);
            return contributorDto;
        }

        public async Task<ContributorDto> UpdateContributorAsync(int currentContributorId, UpdateContributorDto updateContributorDto)
        {
            Contributor contributor = await _unitOfWork.Contributors.GetContributorByIdAsync(currentContributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            contributor = _mapper.Map(updateContributorDto, contributor);

            _unitOfWork.Contributors.UpdateContributor(contributor);
            await _unitOfWork.CommitAsync();

            ContributorDto contributorDto = _mapper.Map<ContributorDto>(contributor);
            return contributorDto;
        }

        public async Task<ContributorDto> UpdateProfileImageContributorAsync(int currentContributorId, UpdateProfileImageContributorDto updateProfileImage)
        {
            Contributor contributor = await _unitOfWork.Contributors.GetContributorByIdAsync(currentContributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (updateProfileImage.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(contributor.ProfileImageUrl))
            {
                await MediaManager.DeleteMedia(_webHost, contributor.ProfileImageUrl);
            }
            IFormFile image = updateProfileImage.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHost,
                    new MediaUploaderDto
                    {
                        Media = image,
                        MediaType = MediaType.profile
                    }
            );

            contributor.ProfileImageUrl = $"uploads/{MediaType.profile}/{imageName}";
            contributor.ProfileImageAltText = updateProfileImage.AltText;

            _unitOfWork.Contributors.UpdateContributor(contributor);
            await _unitOfWork.CommitAsync();

            ContributorDto contributorDto = _mapper.Map<ContributorDto>(contributor);
            return contributorDto;
        }
    }
}