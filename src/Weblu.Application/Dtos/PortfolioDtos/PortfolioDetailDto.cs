using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Dtos.MethodDtos;

namespace Weblu.Application.Dtos.PortfolioDtos
{
    public class    PortfolioDetailDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public required string Slug { get; set; }
        public string? GithubUrl { get; set; }
        public string? LiveUrl { get; set; }
        public bool IsActive { get; set; }
        public string? ActivatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        public int PortfolioCategoryId { get; set; }
        public required string PortfolioCategoryName { get; set; }
        public List<MethodDto>? Methods { get; set; }
        public List<FeatureDto>? Features { get; set; }
        public List<ImageDto>? Images { get; set; }
    }
}