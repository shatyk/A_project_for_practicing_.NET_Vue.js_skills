using Database.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Report
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReportVisabilityStatus VisabilityStatus { get; set; }
        public long? FundraisingId { get; set; }

        public Fundraising? Fundraising { get; set; }
        public IEnumerable<ReportTag> ReportTags { get; init; }
        public IEnumerable<ReportContent> Contents { get; init; }

        public Report()
        {
            ReportTags = new HashSet<ReportTag>();
            Contents = new HashSet<ReportContent>();
        }
    }
}
