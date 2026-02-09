namespace Weblu.Application.Dtos.Common.FeatureDtos
{
    public class FeatureDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
    }
}