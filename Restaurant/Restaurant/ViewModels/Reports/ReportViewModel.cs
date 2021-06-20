namespace Restaurant.ViewModels.Reports
{
    using System;
    using System.Collections.Generic;

    public class ReportViewModel
    {
        public decimal Price { get; set; }

        public string Username { get; set; }

        public DateTime CreatedOn { get; set; }

        public int TableNumber { get; set; }

        public IEnumerable<DishViewModel> Dishes { get; set; }
    }
}
