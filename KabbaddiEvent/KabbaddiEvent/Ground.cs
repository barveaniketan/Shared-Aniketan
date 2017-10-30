using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabbaddiEvent
{
    public class Ground
    {
        public int GroundId { get; set; }

        [DisplayName("Ground Name")]
        public string GroundName { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

    }
}
