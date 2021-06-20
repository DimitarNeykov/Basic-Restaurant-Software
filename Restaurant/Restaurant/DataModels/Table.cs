namespace Restaurant.DataModels
{
    using System;
    using System.Collections.Generic;

    public class Table : BaseDeletableModel<string>
    {
        public Table()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
        }

        public int Number { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantInfo Restaurant { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
