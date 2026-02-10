namespace Weblu.Application.DTOs.Tickets.TicketMessageDTOs
{
    public class EditTicketMessageDTO
    {
        public int? ParentMessageId { get; set; }
        public string Message { get; set; } = default!;
    }
}