using AutoMapper;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Dtos.Common.FeatureDtos;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;

namespace Weblu.Application.Services.Common
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
        public async Task<FeatureDto> CreateAsync(CreateFeatureDto createFeatureDto)
        {
            Feature feature = _mapper.Map<Feature>(createFeatureDto);
            _featureRepository.Add(feature);
            await _unitOfWork.CommitAsync();
            FeatureDto featureDto = _mapper.Map<FeatureDto>(feature);
            return featureDto;
        }
        public async Task DeleteAsync(int featureId)
        {
            Feature feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            feature.Delete();
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FeatureDto>> GetAllAsync(FeatureParameters featureParameters)
        {
            IReadOnlyList<Feature> features = await _featureRepository.GetAllAsync(featureParameters);
            List<FeatureDto> featureDtos = _mapper.Map<List<FeatureDto>>(features);
            return featureDtos;
        }

        public async Task<FeatureDto> GetByIdAsync(int featureId)
        {
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            FeatureDto featureDto = _mapper.Map<FeatureDto>(feature);
            return featureDto;
        }

        public async Task<FeatureDto> UpdateAsync(int featureId, UpdateFeatureDto updateFeatureDto)
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