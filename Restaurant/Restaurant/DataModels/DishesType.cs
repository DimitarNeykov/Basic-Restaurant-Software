namespace Restaurant.DataModels
{
    using System;
    using System.Collections.Generic;

    public class DishesType : BaseDeletableModel<string>
    {
        public DishesType()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Dishes = new HashSet<Dish>();
        }

        public string Name { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantInfo Restaurant { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
