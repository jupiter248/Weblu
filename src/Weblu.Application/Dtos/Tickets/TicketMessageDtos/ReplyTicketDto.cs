namespace Weblu.Application.DTOs.Tickets.TicketMessageDTOs
{
    public class ReplyTicketDTO
    {
        public int? ParentMessageId { get; set; }
        public string Message { get; set; } = default!;
    }
}