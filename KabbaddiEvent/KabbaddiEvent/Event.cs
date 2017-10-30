using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabbaddiEvent
{
    public class Event
    {
        public int EventId { get; set; }
    
        [DisplayName("Kabaddi Event Title")]
        public string EventName { get; set; }

        [DisplayName("Event Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("Total number of Teams")]
        public int NumberOfTeams { get; set; }
    }
}
