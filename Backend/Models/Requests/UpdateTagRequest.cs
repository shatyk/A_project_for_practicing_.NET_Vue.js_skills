namespace Backend.Models.Requests
{
    public class UpdateTagRequest
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
    }
}
