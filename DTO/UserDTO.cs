using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO;
public record UserDTO([EmailAddress] string Email, [StringLength(20, ErrorMessage = "LastName can be betwenn 2 till 20 letters", MinimumLength = 2)] string FirstName, string? LastName, List<OrderDTO> Orders);
public record UserDTOPost([EmailAddress] string Email, [StringLength(20, ErrorMessage = "FirstName can be betwenn 2 till 20 letters", MinimumLength = 2)] string FirstName, string? LastName, string Password);
