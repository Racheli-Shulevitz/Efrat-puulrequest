using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;
using System.Reflection.Metadata.Ecma335;
//using (StreamReader reader = System.IO.File.OpenText("M:\\web - api\\OurStore\\OurStore\\users.txt")) ;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    IUserService userService;

    public UsersController(IUserService _userService)
    {
        userService = _userService;
    }

    string filePath = "M:\\web-api\\OurStore\\OurStore\\users.txt";
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        User checkUser = userService.getById(id);
        return checkUser!=null? Ok(checkUser): NoContent();
    }

    // POST api/<UsersController>
    [HttpPost]
    public ActionResult Post([FromBody] User user)
    {
        User newU = userService.addUser(user);
        if (newU == null)
            return BadRequest();
        return CreatedAtAction(nameof(Get), new { id = user.userId }, user);
    }

    [HttpPost]
    [Route("login")]
    public ActionResult<User> PostLogin([FromQuery] string email,string password)
    {
        User checkUser = userService.login(email,password);
        return checkUser != null ? Ok(checkUser) : NoContent();
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] User userToUpdate)
    {
        User newU= userService.updateUser(id,userToUpdate);
        return newU == null ? BadRequest() : Ok();
    }
    [HttpPost]
    [Route("password")]
    public IActionResult PostPassword([FromQuery] string password)
    {
        int checkPassword = userService.checkPassword(password);
        if (checkPassword < 3)
            return BadRequest(checkPassword);
        else
            return Ok(checkPassword);
    }
}

