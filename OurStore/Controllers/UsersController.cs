using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using DTO;
//using (StreamReader reader = System.IO.File.OpenText("M:\\web - api\\OurStore\\OurStore\\users.txt")) ;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> logger;
    IUserService userService;
    IMapper mapper;

    public UsersController(IUserService _userService,IMapper _mapper, ILogger<UsersController> _logger)
    {
        userService = _userService;
        mapper = _mapper;
        logger = _logger;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> Get(int id)
    {
        User checkUser = await userService.getById(id);
        UserDTO userDTO = mapper.Map<User, UserDTO>(checkUser);
        return userDTO != null? Ok(userDTO) : NoContent();
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] UserDTOPost user)
    {
       User newU = await userService.addUser(mapper.Map<UserDTOPost, User>(user));
        if (newU!= null)
            return CreatedAtAction(nameof(Get), new { id = newU.Id }, mapper.Map<User, UserDTO>(newU));
        return BadRequest();
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<User>> PostLogin([FromQuery] string email,string password)
    {
        logger.LogCritical($"login attempted with User Name , {email} and password {password}.");
        User checkUser = await userService.login(email,password);
        return checkUser != null ? Ok(checkUser) : NoContent();
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UserDTOPost userToUpdate)
    {
        User newU = await userService.updateUser(id, mapper.Map<UserDTOPost, User>(userToUpdate));
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

