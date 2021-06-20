namespace Restaurant.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Restaurant.ViewModels.Tables;
    using Restaurant.Data;
    using Restaurant.DataModels;

    public class TablesService : ITablesService
    {
        private readonly ApplicationDbContext dbContext;

        public TablesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CheckForOpenOrder(string tableId)
        {
            var table = this.dbContext.Tables.Include(x=>x.Orders).FirstOrDefault(x => x.Id == tableId);

            return table.Orders.Any(x => x.IsPay == false && x.IsDelete == false);
        }

        public async Task RemoveAsync(string tableId)
        {
            var table = this.dbContext.Tables.FirstOrDefault(x => x.Id == tableId);

            table.IsDelete = true;
            table.DeletedOn = DateTime.UtcNow;
            table.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(string userId, int number)
        {
            var restaurantId = this.dbContext.Restaurants
                .FirstOrDefault(x => x.Users.Any(x => x.Id == userId)).Id;

            var table = new Table
            {
                Number = number,
                RestaurantId = restaurantId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.dbContext.Tables.AddAsync(table);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<TableViewModel> GetAllByRestaurantId(string userId)
        {
            var restaurantId = this.dbContext.Restaurants
                .FirstOrDefault(x => x.Users.Any(x => x.Id == userId)).Id;

            var tables = this.dbContext.Tables
                .Where(x => x.RestaurantId == restaurantId && x.IsDelete == false)
                .OrderBy(x=>x.Number)
                .Select(x => new TableViewModel
                {
                    Number = x.Number,
                    Id = x.Id,
                    CheckForOpenOrder = x.Orders.Any(x => x.IsPay == false && x.IsDelete == false)
        })
                .ToList();

            return tables;
        }
    }
}
