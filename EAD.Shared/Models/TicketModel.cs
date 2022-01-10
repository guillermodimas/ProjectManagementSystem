using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.Shared.Models
{
    public class TicketModel
    {
        public int? TicketID { get; set; }
        public TicketStatuses Status { get; set; }

        [Required(ErrorMessage = "Ticket description cannot be blank")]

        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }

        public int AssignedProjectID { get; set; }
        public Guid AssignedUserID { get; set; }
    }

    public enum TicketStatuses:int
    {
        Unassigned,
        Todo,
        Started,
        Completed
    }
}
