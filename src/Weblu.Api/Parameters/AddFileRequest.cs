using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Api.Parameters
{
    public class AddFileRequest
    {
        public string FileName { get; set; } = default!;
        public IFormFile File { get; set; } = default!;
        public string AltText { get; set; } = default!;
    }
}