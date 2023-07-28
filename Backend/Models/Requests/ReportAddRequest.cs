using Database.Enums;

namespace Backend.Models.Requests
{
    public class ReportAddRequest
    {
        public FundraisingVisabilityStatus VisabilityStatus { get; set; }
        public IEnumerable<ReportContentAddRequest> Contents { get; init; }
        public IEnumerable<int> TagsId { get; set; }
        public long FundraisingId { get; set; }

        public ReportAddRequest()
        {
            TagsId = new HashSet<int>();
            Contents = new HashSet<ReportContentAddRequest>();
        }
    }
    public class ReportContentAddRequest
    {
        public int LanguageId { get; set; }
        public string Capture { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
