using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabbaddiEvent
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        [ForeignKey("TeamA")]
        public int? TeamAId { get; set; }

        [DisplayName("Team A")]
        public virtual Team TeamA { get; set; }

        [ForeignKey("TeamB")]
        public int? TeamBId { get; set; }

        [DisplayName("Team B")]
        public virtual Team TeamB { get; set; }

        [DisplayName("Date")]
        public DateTime Day { get; set; }

        public int GroundId { get; set; }

        [DisplayName("Venue")]
        public virtual Ground PlayGround { get; set; }

        public bool IsFirst { get; set; }
    }
}
