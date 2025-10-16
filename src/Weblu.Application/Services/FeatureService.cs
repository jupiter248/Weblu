using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Features;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;

namespace Weblu.Application.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FeatureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FeatureDto> AddFeatureAsync(AddFeatureDto addFeatureDto)
        {
            Feature feature = _mapper.Map<Feature>(addFeatureDto);
            await _unitOfWork.Features.AddFeatureAsync(feature);
            await _unitOfWork.CommitAsync();
            FeatureDto featureDto = _mapper.Map<FeatureDto>(feature);
            return featureDto;
        }

        public async Task DeleteFeatureAsync(int featureId)
        {
            Feature feature = await _unitOfWork.Features.GetFeatureByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            _unitOfWork.Features.DeleteFeature(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FeatureDto>> GetAllFeaturesAsync(FeatureParameters featureParameters)
        {
            List<Feature> features = await _unitOfWork.Features.GetAllFeaturesAsync(featureParameters);
            List<FeatureDto> featureDtos = _mapper.Map<List<FeatureDto>>(features);
            return featureDtos;
        }

        public async Task<FeatureDto> GetFeatureByIdAsync(int featureId)
        {
            Feature? feature = await _unitOfWork.Features.GetFeatureByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            FeatureDto featureDto = _mapper.Map<FeatureDto>(feature);
            return featureDto;
        }

        public async Task<FeatureDto> UpdateFeatureAsync(int featureId, UpdateFeatureDto updateFeatureDto)
        {
            Feature? feature = await _unitOfWork.Features.GetFeatureByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            feature = _mapper.Map(updateFeatureDto, feature);
            feature.UpdatedAt = DateTimeOffset.Now;
            _unitOfWork.Features.UpdateFeature(feature);
            await _unitOfWork.CommitAsync();
            FeatureDto featureDto = _mapper.Map<FeatureDto>(feature);
            return featureDto;
        }
    }
}