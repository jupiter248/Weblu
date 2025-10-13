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
    public static class MediaManager
    {
        public static async Task UploadMedia(IWebHostEnvironment webHost, MediaUploaderDto mediaUploaderDto)
        {
            IFormFile media = mediaUploaderDto.Media;
            if (media.Length < 0 || media == null)
            {
                throw new BadRequestException(CommonErrorCodes.MediaInvalid);
            }

            var uploadsFolder = Path.Combine(webHost.WebRootPath, "uploads");
            if (!Path.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var mediaTypeFolder = Path.Combine(webHost.WebRootPath, $"uploads/{mediaUploaderDto.MediaType}");
            if (!Path.Exists(mediaTypeFolder))
            {
                Directory.CreateDirectory(mediaTypeFolder);
            }

            var mediaParentEntityTypeFolder = Path.Combine(webHost.WebRootPath, $"uploads/{mediaUploaderDto.MediaType}/{mediaUploaderDto.MediaParentEntityType}");
            if (!Path.Exists(mediaParentEntityTypeFolder))
            {
                Directory.CreateDirectory(mediaParentEntityTypeFolder);
            }

            var mediaName = $"{Guid.NewGuid()}-{Path.GetFileName(media.FileName)}";
            var mediaPath = Path.Combine(webHost.WebRootPath, $"uploads/{mediaUploaderDto.MediaType}/{mediaUploaderDto.MediaParentEntityType}", mediaName);

            using (var stream = new FileStream(mediaPath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
        }
        public static async Task DeleteMedia(IWebHostEnvironment webHost, string mediaPath)
        {
            var fullPath = Path.Combine(webHost.WebRootPath, mediaPath);
            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }
    }
}