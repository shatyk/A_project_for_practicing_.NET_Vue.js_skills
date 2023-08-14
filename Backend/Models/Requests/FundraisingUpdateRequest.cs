using Database.Enums;

namespace Backend.Models.Requests
{
    public class FundraisingUpdateRequest
    {
        public long Id { get; set; }
        public FundraisingActivityStatus ActivityStatus { get; set; }
        public FundraisingVisabilityStatus VisabilityStatus { get; set; }
        public IEnumerable<FundraisingContentUpdateRequest> Contents { get; init; }

        public FundraisingUpdateRequest()
        {
            Contents = new HashSet<FundraisingContentUpdateRequest>();
        }
    }

    public class FundraisingContentUpdateRequest
    {
        public long FundraisingId { get; set; }
        public int LanguageId { get; set; }
        public string Caption { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
