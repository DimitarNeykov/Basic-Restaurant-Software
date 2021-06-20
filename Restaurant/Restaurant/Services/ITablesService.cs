namespace Restaurant.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Restaurant.ViewModels.Tables;

    public interface ITablesService
    {
        Task CreateAsync(string userId, int number);

        IEnumerable<TableViewModel> GetAllByRestaurantId(string userId);

        bool CheckForOpenOrder(string tableId);

        Task RemoveAsync(string tableId);
    }
}
