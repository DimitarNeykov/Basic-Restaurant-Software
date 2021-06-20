namespace Restaurant.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Restaurant.ViewModels.Orders;
    using Restaurant.ViewModels.Reports;

    public interface IOrdersService
    {
        Task CreateAsync(string waiterId, string tableId);

        OrderViewModel GetByTableId(string tableId);

        OrderViewModel GetById(string orderId);

        Task PayAsync(string orderId);

        Task AddDishToOrderAsync(string dishId, string orderId);

        Task RemoveDishFromOrder(string dishId, string orderId);

        Task Cancel(string orderId);

        IEnumerable<ReportViewModel> GetAllCanceledByRestaurant(string userId);

        IEnumerable<ReportViewModel> GetAllPaidByRestaurant(string userId);
    }
}
