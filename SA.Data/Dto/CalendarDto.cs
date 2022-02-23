using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Dto
{
    public class CalendarDto
    {
        public DateTime ForDay { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
