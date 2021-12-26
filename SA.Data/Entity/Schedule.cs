using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Entity
{
    public class Schedule : Base
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }
    }
}
