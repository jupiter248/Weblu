namespace Weblu.Application.DTOs.Images.ProfileDTOs
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public string? AltText { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long Size { get; set; }
        public required string OwnerId { get; set; }
        public required string OwnerType { get; set; }
        public bool IsMain { get; set; }
        public required string AddedAt { get; set; }
    }
}