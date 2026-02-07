using System.ComponentModel.DataAnnotations.Schema;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Services;
using Weblu.Domain.Exceptions;

namespace Weblu.Domain.Entities.Services
{
    public class Service : BaseEntity
    {
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public string Description { get; set; } = default!;
        public required string ShortDescription { get; set; }
        public int BaseDurationInDays { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? ActivatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Feature> Features { get; set; } = new List<Feature>();
        public List<Method> Methods { get; set; } = new List<Method>();
        public List<ServiceImage> ServiceImages { get; set; } = new List<ServiceImage>();


        public void AddMethod(Method method)
        {
            if (Methods.Any(m => m.Id == method.Id))
            {
                throw new DomainException(ServiceErrorCodes.MethodAlreadyAddedToService, 409);
            }
            Methods.Add(method);
        }
        public void AddFeature(Feature feature)
        {
            if (Features.Any(f => f.Id == feature.Id))
            {
                throw new DomainException(ServiceErrorCodes.FeatureAlreadyAddedToService, 409);
            }
            Features.Add(feature);
        }
        public void AddImage(ServiceImage image)
        {
            if (ServiceImages.Any(p => p.ImageId == image.ImageId))
            {
                throw new DomainException(ServiceErrorCodes.ImageAlreadyAddedToService, 409);
            }
            if (ServiceImages.Any(p => p.IsThumbnail && image.IsThumbnail))
            {
                throw new DomainException(ServiceErrorCodes.ServiceHasThumbnailImage, 409);
            }
            ServiceImages.Add(image);
        }
        public void DeleteMethod(Method method)
        {
            if (!Methods.Any(c => c.Id == method.Id))
            {
                throw new DomainException(MethodErrorCodes.MethodNotFound, 404);
            }
            Methods.Remove(method);
        }
        public void DeleteFeature(Feature feature)
        {
            if (!Features.Any(c => c.Id == feature.Id))
            {
                throw new DomainException(MethodErrorCodes.MethodNotFound, 404);
            }
            Features.Remove(feature);
        }
        public void DeleteImage(ImageMedia imageMedia)
        {
            ServiceImage? serviceImage = ServiceImages.FirstOrDefault(i => i.ImageId == imageMedia.Id);
            if (serviceImage == null)
            {
                throw new DomainException(ImageErrorCodes.ImageNotFound, 404);
            }
            ServiceImages.Remove(serviceImage);
        }
        public void UpdateActivateStatus(bool isActive)
        {
            IsActive = isActive;
            if (isActive && ActivatedAt == DateTimeOffset.MinValue)
            {
                ActivatedAt = DateTimeOffset.Now;
            }
            else if (!isActive)
            {
                ActivatedAt = DateTimeOffset.MinValue; 
            }
        }
    }
}