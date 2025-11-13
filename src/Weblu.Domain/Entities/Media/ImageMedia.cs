using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Services;

namespace Weblu.Domain.Entities.Media
{
    public class ImageMedia : Media
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<ServiceImage> ServiceImages { get; set; } = new List<ServiceImage>();
        public List<PortfolioImage> PortfolioImages { get; set; } = new List<PortfolioImage>();
    }
}