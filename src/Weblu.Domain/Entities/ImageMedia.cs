using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Entities
{
    public class ImageMedia : Media
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}