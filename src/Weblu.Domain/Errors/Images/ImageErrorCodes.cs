using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Images
{
    public class ImageErrorCodes
    {
        public const string ImageNotFound = "IMAGE_NOT_FOUND";
        public const string ImageFileInvalid = "MEDIUM_FILE_INVALID";
        public const string ImageAltTextMaxLength = "MEDIUM_ALT_TEXT_LENGTH";
        public const string ImageAltTextRequired = "MEDIUM_ALT_TEXT_REQUIRED";
        public const string IconAltTextMaxLength = "ICON_ALT_TEXT_LENGTH";
        public const string IconAltTextRequired = "ICON_ALT_TEXT_REQUIRED";
        public const string ImageFileRequired = "MEDIUM_FILE_REQUIRED";
        public const string IconFileRequired = "ICON_FILE_REQUIRED";
        public const string ImageFileSize = "MEDIUM_FILE_SIZE";
        public const string IconFileSize = "ICON_FILE_SIZE";

    }
}