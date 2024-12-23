using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> getById(int id);
        Task<Order> addOrder(Order order);
    }
}