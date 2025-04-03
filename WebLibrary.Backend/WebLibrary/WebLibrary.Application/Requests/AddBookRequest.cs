using Microsoft.AspNetCore.Http;

namespace WebLibrary.Application.Requests
{
    public class AddBookRequest
    {
        public string ISBN { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public DateTime? BorrowedAt { get; set; }
        public DateTime? ReturnBy { get; set; }
        public Guid? BorrowedById { get; set; }
        public bool IsAvailable { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}