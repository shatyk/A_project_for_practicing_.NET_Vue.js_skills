using Database.Enums;

namespace Backend.Models.Responses
{
    public class FundraisingGetAllResponse
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public FundraisingActivityStatus ActivityStatus { get; set; }
        public FundraisingVisabilityStatus VisabilityStatus { get; set; }
        public string ContentUaCaption { get; set; } = null!;
    }
}
