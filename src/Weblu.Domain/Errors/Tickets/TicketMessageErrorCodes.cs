namespace Weblu.Domain.Errors.Tickets
{
    public class TicketMessageErrorCodes
    {
        public const string TicketMessageNotFound = "TICKET_MESSAGE_NOT_FOUND";
        public const string TicketMessageDeleteForbidden = "TICKET_MESSAGE_DELETE_FORBIDDEN";
        public const string TicketMessageUpdateForbidden = "TICKET_MESSAGE_UPDATE_FORBIDDEN";
        public const string TicketMessageReplyForbidden = "TICKET_MESSAGE_REPLY_FORBIDDEN";
        public const string MessageRequired = "TICKET_MESSAGE_MASSAGE_REQUIRED";
        public const string MessageMaxLength = "TICKET_MESSAGE_MESSAGE_MAX_LENGTH";
    }
}