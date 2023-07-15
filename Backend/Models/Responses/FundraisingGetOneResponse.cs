using Database.Enums;
using Database.Models;

namespace Backend.Models.Responses
{
    public class FundraisingGetOneResponse
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public FundraisingActivityStatus ActivityStatus { get; set; }
        public FundraisingVisabilityStatus VisabilityStatus { get; set; }
        public IEnumerable<FundraisingContentResponse> Contents { get; init; }

        public FundraisingGetOneResponse()
        {
            Contents = new HashSet<FundraisingContentResponse>();
        }
    }

    public class FundraisingContentResponse
    {       
        public string Capture { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int LanguageId { get; set; }
    }
}
