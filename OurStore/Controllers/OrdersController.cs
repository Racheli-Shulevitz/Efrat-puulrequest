using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService OrderService;
        IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper _mapper)
        {
            OrderService = orderService;
            mapper = _mapper;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            Order order = await OrderService.getById(id);
            OrderDTO orderDTO = mapper.Map<Order, OrderDTO>(order);
            return orderDTO != null ? Ok(orderDTO) : BadRequest();
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderDTOPost order)
        {
            Order? newOrder = await OrderService.addOrder(mapper.Map<OrderDTOPost, Order>(order));
            OrderDTO newOrderDto = mapper.Map<Order, OrderDTO>(newOrder);
            if (newOrderDto != null)
                return CreatedAtAction(nameof(Get), new { id = newOrderDto.OrderId }, newOrderDto);
            return BadRequest();
        }
    }
}
