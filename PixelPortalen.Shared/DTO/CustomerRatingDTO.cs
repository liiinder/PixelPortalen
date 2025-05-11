namespace PixelPortalen.Shared.DTO
{
    public class CustomerRatingDTO
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}