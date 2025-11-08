using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Tickets
{
    public class TicketErrorCodes
    {
        public const string TicketNotFound = "TICKET_NOT_FOUND";
        public const string TicketDeleteForbidden = "TICKET_DELETE_FORBIDDEN";
        public const string TicketUpdateForbidden = "TICKET_UPDATE_FORBIDDEN";

    }
}