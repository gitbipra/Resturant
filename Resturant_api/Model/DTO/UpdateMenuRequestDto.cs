namespace Resturant_api.Model.DTO
{
    public class UpdateMenuRequestDto
    {
        public required string FoodName { get; set; }
        public required string Category { get; set; }
        public required string FoodType { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
