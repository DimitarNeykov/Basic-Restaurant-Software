namespace Restaurant.DataModels
{
    using System;
    using System.Collections.Generic;

    public class Dish : BaseDeletableModel<string>
    {
        public Dish()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DishOrders = new HashSet<DishOrder>();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string DishesTypeId { get; set; }

        public DishesType DishesType { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantInfo Restaurant { get; set; }

        public ICollection<DishOrder> DishOrders { get; set; }
    }
}
