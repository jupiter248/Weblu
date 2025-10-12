using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Common.Dtos;
using Weblu.Application.Exceptions;
using Weblu.Domain.Errors.Commons;

namespace Weblu.Application.Helpers
{
    public static class MediumManager
    {
        public static async Task UploadMedium(IWebHostEnvironment webHost, MediumUploaderDto mediumUploaderDto)
        {
            IFormFile medium = mediumUploaderDto.Medium;
            if (medium.Length < 0 || medium == null)
            {
                throw new BadRequestException(CommonErrorCodes.MediumInvalid);
            }

            var uploadsFolder = Path.Combine(webHost.WebRootPath, "uploads");
            if (!Path.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var mediumTypeFolder = Path.Combine(webHost.WebRootPath, $"uploads/{mediumUploaderDto.MediumType}");
            if (!Path.Exists(mediumTypeFolder))
            {
                Directory.CreateDirectory(mediumTypeFolder);
            }

            var mediumParentEntityTypeFolder = Path.Combine(webHost.WebRootPath, $"uploads/{mediumUploaderDto.MediumType}/{mediumUploaderDto.MediumParentEntityType}");
            if (!Path.Exists(mediumParentEntityTypeFolder))
            {
                Directory.CreateDirectory(mediumParentEntityTypeFolder);
            }

            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(medium.FileName)}";
            var filePath = Path.Combine(webHost.WebRootPath, $"uploads/{mediumUploaderDto.MediumType}/{mediumUploaderDto.MediumParentEntityType}", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await medium.CopyToAsync(stream);
            }
        }
        public static async Task DeleteMedium(IWebHostEnvironment webHost , string filePath)
        {
            var fullPath = Path.Combine(webHost.WebRootPath, filePath);
            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }
    }
}