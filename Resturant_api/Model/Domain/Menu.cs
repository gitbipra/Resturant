namespace Resturant_api.Model.Domain
{
    public class Menu
    {
        public Guid MenuId { get; set; }
        public required string FoodName { get; set; }
        public required string Category { get; set; } //(Starter, Main Course, Dessert)
        public required string FoodType { get; set; } //"Veg" or "Non-Veg"
        public required decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now; // Auto-set timestamp
    }
}
