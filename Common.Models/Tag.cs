using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
    }
}
