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
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeatureRepository _featureRepository;
        private readonly IMapper _mapper;
        public FeatureService(IUnitOfWork unitOfWork, IMapper mapper, IFeatureRepository featureRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _featureRepository = featureRepository;
        }
        public async Task<FeatureDto> AddFeatureAsync(AddFeatureDto addFeatureDto)
        {
            Feature feature = _mapper.Map<Feature>(addFeatureDto);
            _featureRepository.Add(feature);
            await _unitOfWork.CommitAsync();
            FeatureDto featureDto = _mapper.Map<FeatureDto>(feature);
            return featureDto;
        }

        public async Task DeleteFeatureAsync(int featureId)
        {
            Feature feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            _featureRepository.Delete(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FeatureDto>> GetAllFeaturesAsync(FeatureParameters featureParameters)
        {
            IReadOnlyList<Feature> features = await _featureRepository.GetAllAsync(featureParameters);
            List<FeatureDto> featureDtos = _mapper.Map<List<FeatureDto>>(features);
            return featureDtos;
        }

        public async Task<FeatureDto> GetFeatureByIdAsync(int featureId)
        {
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            FeatureDto featureDto = _mapper.Map<FeatureDto>(feature);
            return featureDto;
        }

        public async Task<FeatureDto> UpdateFeatureAsync(int featureId, UpdateFeatureDto updateFeatureDto)
        {
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            feature = _mapper.Map(updateFeatureDto, feature);
            _featureRepository.Update(feature);
            await _unitOfWork.CommitAsync();
            FeatureDto featureDto = _mapper.Map<FeatureDto>(feature);
            return featureDto;
        }
    }
}