namespace Restaurant.ViewModels.Restaurants
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class RestaurantEditInputModel
    {
        public string Id { get; set; }

        [MinLength(3, ErrorMessage = "Field required minimal length of 3 characters!")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Address { get; set; }

        [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
