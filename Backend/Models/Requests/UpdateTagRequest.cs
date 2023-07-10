namespace Backend.Models.Requests
{
    public class UpdateTagRequest
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
    }
}
