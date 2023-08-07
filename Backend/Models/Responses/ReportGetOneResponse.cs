using Database.Enums;

namespace Backend.Models.Responses
{
    public class ReportGetOneResponse
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReportVisabilityStatus VisabilityStatus { get; set; }
        public IEnumerable<ReportContentResponse> Contents { get; init; }
        public long? FundraisingId { get; set; }
        public IEnumerable<int> TagsId { get; set; }

        public ReportGetOneResponse()
        {
            Contents = new HashSet<ReportContentResponse>();
            TagsId = new HashSet<int>();
        }
    }

    public class ReportContentResponse
    {
        public string Capture { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int LanguageId { get; set; }
    }
}
