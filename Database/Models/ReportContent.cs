using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class ReportContent
    {
        public long ReportId { get; set; }
        public int LanguageId { get; set; }
        public string Caption { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
