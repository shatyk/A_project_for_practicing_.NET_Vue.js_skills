using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class ReportTag
    {
        public int Id { get; set; }
        public long ReportId { get; set; }
        public int TagId { get; set; }

        public Report? Report { get; set; }
        public Tag? Tag { get; set; }
    }
}
