using System.ComponentModel.DataAnnotations;

namespace Resturant_api.Model.DTO
{
    public class AddMenuRequestDto
    {
        [MinLength(3, ErrorMessage ="Food name has been minimum of 3 characters")]
        public required string FoodName { get; set; }
        public required string Category { get; set; }
        public required string FoodType { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

    }
}
