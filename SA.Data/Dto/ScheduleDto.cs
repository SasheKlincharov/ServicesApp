using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Data.Dto
{
    public class ScheduleDto
    {
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
