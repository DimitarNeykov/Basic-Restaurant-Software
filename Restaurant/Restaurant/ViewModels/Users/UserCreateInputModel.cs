namespace Restaurant.ViewModels.Users
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UserCreateInputModel
    {
        [Required(ErrorMessage = "The field is required!")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "The field requires 10 digits!")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [MinLength(3, ErrorMessage = "The field requires more than 3 characters!")]
        [MaxLength(30, ErrorMessage = "The field must not be more than 30 characters!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
        [MinLength(10, ErrorMessage = "The field requires more than 10 characters!")]
        [MaxLength(50, ErrorMessage = "The field must not be more than 50 characters!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The field is required!")]
        public string Role { get; set; }

        public string RestaurantId { get; set; }
    }
}
