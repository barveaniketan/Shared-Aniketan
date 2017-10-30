using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProKabbadi.Models
{
    public class ProKabbadiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ProKabbadiContext() : base("name=ProKabbadiContext")
        {
        }

        public System.Data.Entity.DbSet<KabbaddiEvent.Ground> Grounds { get; set; }

        public System.Data.Entity.DbSet<KabbaddiEvent.Team> Teams { get; set; }

        public System.Data.Entity.DbSet<KabbaddiEvent.Schedule> Schedules { get; set; }

        public System.Data.Entity.DbSet<KabbaddiEvent.Event> Events { get; set; }
    }
}
