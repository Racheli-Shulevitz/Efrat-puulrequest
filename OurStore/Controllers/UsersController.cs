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
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        User checkUser = userService.getById(id);
        return checkUser!=null? Ok(checkUser): NoContent();
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] User user)
    {
        var newU = await userService.addUser(user);
        if (newU!= null)
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        return BadRequest();
       
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<User>> PostLogin([FromQuery] string email,string password)
    {
        User checkUser = await userService.login(email,password);
        return checkUser != null ? Ok(checkUser) : NoContent();
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] User userToUpdate)
    {
        User newU= await userService.updateUser(id,userToUpdate);
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

