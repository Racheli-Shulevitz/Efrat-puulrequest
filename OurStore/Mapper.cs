using AutoMapper;
using Entities;
using DTO;
namespace OurStore;

public class Mapper:Profile
{
        public Mapper()
        {
        CreateMap<Category, CategoryDTO>();
        CreateMap<Product, ProductDTO>();
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderItem, OrderItemDTO>();
        CreateMap<OrderItemDTO,OrderItem > ();
        CreateMap<User, UserDTO>();
        CreateMap<OrderDTOPost, Order>();
        CreateMap<UserDTOPost, User>();
        CreateMap<User, UserDTOPost>();

    }
}
