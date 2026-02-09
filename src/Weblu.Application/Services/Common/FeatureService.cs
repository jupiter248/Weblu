using AutoMapper;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.DTOs.Common.FeatureDTOs;
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
        public async Task<FeatureDTO> CreateAsync(CreateFeatureDTO createFeatureDTO)
        {
            Feature feature = _mapper.Map<Feature>(createFeatureDTO);
            _featureRepository.Add(feature);
            await _unitOfWork.CommitAsync();
            FeatureDTO featureDTO = _mapper.Map<FeatureDTO>(feature);
            return featureDTO;
        }
        public async Task DeleteAsync(int featureId)
        {
            Feature feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            feature.Delete();
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FeatureDTO>> GetAllAsync(FeatureParameters featureParameters)
        {
            IReadOnlyList<Feature> features = await _featureRepository.GetAllAsync(featureParameters);
            List<FeatureDTO> featureDTOs = _mapper.Map<List<FeatureDTO>>(features);
            return featureDTOs;
        }

        public async Task<FeatureDTO> GetByIdAsync(int featureId)
        {
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            FeatureDTO featureDTO = _mapper.Map<FeatureDTO>(feature);
            return featureDTO;
        }

        public async Task<FeatureDTO> UpdateAsync(int featureId, UpdateFeatureDTO updateFeatureDTO)
        {
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            feature = _mapper.Map(updateFeatureDTO, feature);

            feature.MarkUpdated();
            _featureRepository.Update(feature);
            await _unitOfWork.CommitAsync();
            FeatureDTO featureDTO = _mapper.Map<FeatureDTO>(feature);
            return featureDTO;
        }
    }
}