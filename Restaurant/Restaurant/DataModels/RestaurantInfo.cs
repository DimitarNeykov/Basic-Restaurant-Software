namespace Restaurant.DataModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class RestaurantInfo : BaseDeletableModel<string>
    {
        public RestaurantInfo()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tables = new HashSet<Table>();
            this.Users = new HashSet<IdentityUser>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Table> Tables { get; set; }

        public ICollection<IdentityUser> Users { get; set; }
    }
}
