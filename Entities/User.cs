using System.ComponentModel.DataAnnotations;

namespace Entities;
public class User
{
    public int userId { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [StringLength(15,ErrorMessage = "FirstName can be up to 15 letters")]
    public string FirstName { get; set; }
    [StringLength(15, ErrorMessage = "LastName can be up to 15 letters")]
    public string LastName { get; set; }
}
