namespace Restaurant.ViewModels.Restaurants
{
    using System;
    using System.Collections.Generic;

    public class RestaurantViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}
