namespace Restaurant.DataModels
{
    using System;

    public class DishOrder : BaseDeletableModel<string>
    {
        public DishOrder()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string DishId { get; set; }

        public Dish Dish { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }
    }
}
