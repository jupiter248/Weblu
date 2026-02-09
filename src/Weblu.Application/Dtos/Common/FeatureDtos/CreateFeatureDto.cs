namespace Weblu.Application.Dtos.Common.FeatureDtos
{
    public class CreateFeatureDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}