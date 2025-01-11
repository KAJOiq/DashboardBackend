using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Dtos.Tickets
{
    public class TicketQueryDto
    {
        public int CurrentPage { set; get; } = 1;
        public int PageSize { set; get; } = 10;
    }
}