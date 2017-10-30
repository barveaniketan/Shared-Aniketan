using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabbaddiEvent
{
    public class Team
    {
        public int TeamId { get; set; }

        [DisplayName("Team Name")]
        public string TeamName { get; set; }

        [DisplayName("Team City")]
        public string City { get; set; }

        public int GroundId { get; set; }

        [DisplayName("Home Ground")]
        public virtual Ground HomeGround { get; set; }
    }
}
