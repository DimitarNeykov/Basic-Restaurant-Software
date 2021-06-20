namespace Restaurant.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Restaurant.Data;
    using Restaurant.DataModels;
    using Restaurant.ViewModels.Orders;
    using Restaurant.ViewModels.Reports;

    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext dbContext;

        public OrdersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddDishToOrderAsync(string dishId, string orderId)
        {
            var dishOrder = new DishOrder
            {
                DishId = dishId,
                OrderId = orderId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.dbContext.DishOrders.AddAsync(dishOrder);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(string waiterId, string tableId)
        {
            var order = new Order
            {
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                WaiterId = waiterId,
                TableId = tableId,
            };

            await this.dbContext.Orders.AddAsync(order);
            await this.dbContext.SaveChangesAsync();
        }

        public OrderViewModel GetByTableId(string tableId)
        {
            var order = this.dbContext.Orders
                .Where(x => x.TableId == tableId && x.IsPay == false && x.IsDelete == false)
                .Include(x=>x.DishOrders)
                .ThenInclude(x=>x.Dish)
                .ThenInclude(x=>x.DishesType)
                .Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    Dishes = x.DishOrders.Where(x=>x.IsDelete == false).Select(x => x.Dish).ToList(),
                    IsPay = x.IsPay,
                    TableNumber = x.Table.Number,
                    WaiterUsername = x.Waiter.UserName,
                })
                .First();

            return order;
        }

        public OrderViewModel GetById(string orderId)
        {
            var order = this.dbContext.Orders
                .Where(x => x.Id == orderId && x.IsPay == false && x.IsDelete == false)
                .Select(x => new OrderViewModel
                {
                    TableId = x.TableId,
                })
                .First();

            return order;
        }

        public async Task PayAsync(string orderId)
        {
            var order = this.dbContext.Orders.FirstOrDefault(x => x.Id == orderId);
            order.IsPay = true;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveDishFromOrder(string dishId, string orderId)
        {
            var dishOrder = this.dbContext.DishOrders.Where(x=>x.IsDelete == false).FirstOrDefault(x => x.OrderId == orderId && x.DishId == dishId);

            dishOrder.IsDelete = true;
            dishOrder.DeletedOn = DateTime.UtcNow;
            dishOrder.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task Cancel(string orderId)
        {
            var order = this.dbContext.Orders.Where(x => x.IsDelete == false && x.IsPay == false).FirstOrDefault(x => x.Id == orderId);

            order.IsDelete = true;
            order.DeletedOn = DateTime.UtcNow;
            order.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<ReportViewModel> GetAllCanceledByRestaurant(string userId)
        {
            var orders = this.dbContext.Orders
                .Where(x => x.Table.Restaurant.Users.Any(x => x.Id == userId) && x.IsDelete == true)
                .OrderByDescending(x=>x.CreatedOn)
                .Select(x => new ReportViewModel
                {
                    TableNumber = x.Table.Number,
                    CreatedOn = x.CreatedOn,
                    Price = x.DishOrders.Sum(x=>x.Dish.Price),
                    Username = x.Waiter.UserName,
                    Dishes = x.DishOrders.Select(x=> new ViewModels.Reports.DishViewModel
                    {
                        Price = x.Dish.Price,
                        DishType = x.Dish.DishesType.Name,
                        Name = x.Dish.Name
                    }).ToList()
                })
                .ToList();

            return orders;
        }

        public IEnumerable<ReportViewModel> GetAllPaidByRestaurant(string userId)
        {
            var orders = this.dbContext.Orders
                .Where(x => x.Table.Restaurant.Users.Any(x => x.Id == userId) && x.IsDelete == false && x.IsPay == true)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new ReportViewModel
                {
                    TableNumber = x.Table.Number,
                    CreatedOn = x.CreatedOn,
                    Price = x.DishOrders.Sum(x => x.Dish.Price),
                    Username = x.Waiter.UserName,
                    Dishes = x.DishOrders.Select(x => new ViewModels.Reports.DishViewModel
                    {
                        Price = x.Dish.Price,
                        DishType = x.Dish.DishesType.Name,
                        Name = x.Dish.Name
                    }).ToList()
                })
                .ToList();

            return orders;
        }
    }
}
