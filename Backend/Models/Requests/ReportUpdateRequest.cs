using Database.Enums;

namespace Backend.Models.Requests
{
    public class ReportUpdateRequest
    {
        public long Id { get; set; }
        public FundraisingVisabilityStatus VisabilityStatus { get; set; }
        public IEnumerable<ReportContentUpdateRequest> Contents { get; init; }
        public IEnumerable<int> TagsId { get; set; }
        public long? FundraisingId { get; set; }
        public ReportUpdateRequest()
        {
            TagsId = new HashSet<int>();
            Contents = new HashSet<ReportContentUpdateRequest>();
        }
    }

    public class ReportContentUpdateRequest
    {
        public long ReportId { get; set; }
        public int LanguageId { get; set; }
        public string Capture { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
