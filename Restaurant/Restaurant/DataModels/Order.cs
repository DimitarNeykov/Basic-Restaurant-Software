namespace Restaurant.DataModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ICollection<DishOrder> DishOrders { get; set; }

        public bool IsPay { get; set; }

        public string WaiterId { get; set; }

        public IdentityUser Waiter { get; set; }

        public string TableId { get; set; }

        public Table Table { get; set; }
    }
}
