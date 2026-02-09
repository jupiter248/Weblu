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
        public string Title { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public int BaseDurationInDays { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }
        // Publishing info
        public bool IsPublished { get; private set; }
        public DateTimeOffset? PublishedAt { get; private set; }
        // Relationships
        public List<Feature> Features { get; set; } = new();
        public List<Method> Methods { get; set; } = new();
        public List<ServiceImage> ServiceImages { get; set; } = new();

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
        public void RemoveMethod(Method method)
        {
            if (!Methods.Any(c => c.Id == method.Id))
            {
                throw new DomainException(MethodErrorCodes.MethodNotFound, 404);
            }
            Methods.Remove(method);
        }
        public void RemoveFeature(Feature feature)
        {
            if (!Features.Any(c => c.Id == feature.Id))
            {
                throw new DomainException(MethodErrorCodes.MethodNotFound, 404);
            }
            Features.Remove(feature);
        }
        public void RemoveImage(ImageMedia imageMedia)
        {
            ServiceImage? serviceImage = ServiceImages.FirstOrDefault(i => i.ImageId == imageMedia.Id);
            if (serviceImage == null)
            {
                throw new DomainException(ImageErrorCodes.ImageNotFound, 404);
            }
            ServiceImages.Remove(serviceImage);
        }
        public void Publish()
        {
            if (IsPublished) throw new DomainException(ServiceErrorCodes.AlreadyPublished, 409);
            IsPublished = true;
            PublishedAt = DateTimeOffset.Now;
        }
        public void Unpublish()
        {
            if (!IsPublished) throw new DomainException(ServiceErrorCodes.DidNotPublish, 409);
            IsPublished = false;
            PublishedAt = null;
        }
    }
}