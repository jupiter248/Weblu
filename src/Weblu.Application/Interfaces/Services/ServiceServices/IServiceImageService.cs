using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ServiceDtos.ServiceImageDtos;

namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceImageService
    {
        Task AddImageAsync(int serviceId, int imageId, AddServiceImageDto addServiceImageDto);
        Task DeleteImageAsync(int serviceId, int imageId);
    }
}