using Database.Enums;

namespace Backend.Models.Responses
{
    public class ReportGetAllResponse
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReportVisabilityStatus VisabilityStatus { get; set; }
        public string ContentUaCaption { get; set; } = null!;
        public string FundraisingContentUaCaption { get; set; } = null!;
    }
}
