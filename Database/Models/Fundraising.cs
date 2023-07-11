using Database.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Fundraising
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public FundraisingActivityStatus ActivityStatus { get; set; }
        public FundraisingVisabilityStatus VisabilityStatus { get; set; }

        public IEnumerable<Report> Reports { get; init; }
        public IEnumerable<FundraisingContent> Contents { get; init; }

        public Fundraising()
        {
            Reports = new HashSet<Report>();
            Contents = new HashSet<FundraisingContent>();
        }
    }
}
