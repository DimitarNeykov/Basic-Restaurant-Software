namespace Restaurant.ViewModels.Dishes
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class DishCreateInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Dish Type")]
        public string DishesTypeId { get; set; }

        public IEnumerable<DishTypeDropDownViewModel> DishTypes { get; set; }
    }
}
