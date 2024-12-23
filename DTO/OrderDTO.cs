using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO;
public record OrderDTO(int OrderId, DateTime OrderDate, double OrderSum, string UserFirstName, List<OrderItemDTO> OrderItems);
public record OrderDTOPost(DateTime OrderDate, double OrderSum, int UserId, List<OrderItemDTO> OrderItems);
