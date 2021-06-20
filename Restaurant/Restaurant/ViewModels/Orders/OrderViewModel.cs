namespace Restaurant.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.Linq;

    using Restaurant.DataModels;

    public class OrderViewModel
    {
        public string Id { get; set; }

        public string WaiterUsername { get; set; }

        public int TableNumber { get; set; }

        public string TableId { get; set; }

        public bool IsPay { get; set; }

        public ICollection<Dish> Dishes { get; set; }

        public decimal AllPrice => Dishes.Sum(x => x.Price);

        public int AllQuantity => Dishes.Count;
    }
}
