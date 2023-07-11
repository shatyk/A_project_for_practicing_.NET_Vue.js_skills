using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;

        public IEnumerable<ReportTag> ReportTags { get; init; }

        public Tag()
        {
            ReportTags = new HashSet<ReportTag>();
        }
    }
}
