using Database.Enums;

namespace Backend.Models.Responses
{
    public class ReportGetAllResponse
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReportVisabilityStatus VisabilityStatus { get; set; }
        public string ContentUaCapture { get; set; } = null!;
        public string FundraisingContentUaCapture { get; set; } = null!;
    }
}
