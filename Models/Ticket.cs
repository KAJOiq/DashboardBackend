using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Enums;

namespace Ticket.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Guid CustomerId {set; get;} 
        public int DepartmentId {set; get;}
        public string Title { get; set; } = string.Empty;
        public string Description {get; set;} = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string Photo_url { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Pending;
        public TicketPriority Priority { get; set; } = TicketPriority.Normal;
        public string AssignorId { get; set; } = string.Empty;
        public string AssignId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }
    }
}