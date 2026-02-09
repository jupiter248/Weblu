using Weblu.Application.DTOs.Images.MediaDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Helpers
{
    public static class MediaManager
    {

        public static async Task<string> UploadMedia(string webRootPath, MediaUploaderDTO mediaUploaderDTO)
        {
            var media = mediaUploaderDTO.Media;
            if (media.Length < 0 || media == null)
            {
                throw new BadRequestException(CommonErrorCodes.MediaInvalid);
            }

            var uploadsFolder = Path.Combine(webRootPath, "uploads");
            if (!Path.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var mediaTypeFolder = Path.Combine(webRootPath, $"uploads/{mediaUploaderDTO.MediaType}");
            if (!Path.Exists(mediaTypeFolder))
            {
                Directory.CreateDirectory(mediaTypeFolder);
            }

            string mediaName = $"{Guid.NewGuid()}-{Path.GetFileName(media.FileName)}";
            string mediaPath = Path.Combine(webRootPath, $"uploads/{mediaUploaderDTO.MediaType}", mediaName);

            using (var stream = new FileStream(mediaPath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }
            return mediaName;
        }
        public static async Task DeleteMedia(string webRootPath, string mediaPath)
        {
            var fullPath = Path.Combine(webRootPath, mediaPath);
            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }
    }
}