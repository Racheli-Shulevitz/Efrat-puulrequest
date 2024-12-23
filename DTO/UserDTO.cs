using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO;
public record UserDTO(string Email, string FirstName, string? LastName, List<OrderDTO> Orders);
public record UserDTOPost(string Email, string FirstName, string? LastName, string Password);
