
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seat_Reservation.Models;
using System.Configuration;
using System.Security.Claims;
using System.IO;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace Seat_Reservation.Controllers;




[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{

    private readonly ApplicationDbContext _context;     //db context

        private readonly IConfiguration configuration;      //IConfiguration injecting
private readonly IMapper _mapper;       //auto mapper injecting



public UserController(ApplicationDbContext context ,IConfiguration configuration,IMapper mapper){
    _context=context;
    this.configuration=configuration;
    _mapper=mapper;
}

//Login Api
[HttpPost]
[Route("Login")]

public IActionResult Login(LoginDto loginDto){
var user=_context.Users.FirstOrDefault(x => x.Email==loginDto.Email && x.Password==loginDto.Password);
if(user!=null){
    var claims=new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub,configuration["Jwt:Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        new Claim("UserId",user.User_Id.ToString()),
        new Claim("Email",user.Email.ToString())    
};

        var key=new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var signIn=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var Token=new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims, 
                expires : DateTime.UtcNow.AddMinutes(05),
                signingCredentials:signIn
        );

        string tokenValue=new JwtSecurityTokenHandler().WriteToken(Token);
        return Ok(new {Token =tokenValue,User=user});

}

return NoContent();


}

//Registration Api
  [HttpPost("{Registration}")]

public IActionResult RegisterUser([FromBody]User Model){
     _context.Add(Model);
     _context.SaveChanges();
    return Ok();
}

[HttpGet("{Email}")]

public IActionResult Password_Change([FromRoute] string Email){
    var user=_context.Users.FirstOrDefault(x => x.Email==Email);
    if(user!=null){
      
       return Ok(user);

    }

    return Ok();

    
}
[HttpGet]
public IActionResult getall(){
    var all=_context.Users.ToList();
    return Ok(all);
}


}
