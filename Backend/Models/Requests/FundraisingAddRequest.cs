using Database.Enums;
using Database.Models;

namespace Backend.Models.Requests
{
    public class FundraisingAddRequest
    {
        public FundraisingActivityStatus ActivityStatus { get; set; }
        public FundraisingVisabilityStatus VisabilityStatus { get; set; }
        public IEnumerable<FundraisingContentAddRequest> Contents { get; init; }      
    }

    public class FundraisingContentAddRequest
    {
        public int LanguageId { get; set; }
        public string Capture { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
