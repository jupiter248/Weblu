namespace Weblu.Application.DTOs.Portfolios.PortfolioCategoryDTOs
{
    public class PortfolioCategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
    }
}