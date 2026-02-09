namespace Weblu.Application.DTOs.Tickets.TicketMessageDTOs
{
    public class TicketMessageDTO
    {
        public int Id { get; set; }
        public string Message { get; set; } = default!;
        public bool IsFromAdmin { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
        public required string SenderId { get; set; }
        public int TicketId { get; set; }
    }
}