using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Users;
using RestaurantReservation.API.Services;


namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthController(JwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }


    [HttpPost("login")]
    public IActionResult Login(User requestUser)
    {
        var token = _tokenGenerator.GenerateToken(requestUser);

        if (token == null)
            return Unauthorized("Invalid username or password");

        return Ok(new { token });
    }

}
