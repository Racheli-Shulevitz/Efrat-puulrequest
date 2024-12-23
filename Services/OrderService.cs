using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class OrderService : IOrderService
{
    IOrderRepository orderRepository;
    public OrderService(IOrderRepository _orderRepository)
    {
        orderRepository = _orderRepository;
    }
    public async Task<Order> addOrder(Order order)
    {
        return await orderRepository.addOrder(order);
    }
    public async Task<Order> getById(int id)
    {
        return await orderRepository.getById(id);
    }

}
