using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Dtos;
using TaskManagement.Models;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<User> userManager,IConfiguration configuration)
        {
            UserManager = userManager;
            Configuration = configuration;
        }

        public UserManager<User> UserManager { get; }
        public IConfiguration Configuration { get; }

        [HttpPost("register")]

        public async Task<IActionResult> Registration(RegisterUserDto registerUserDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {

                    Email = registerUserDto.email,
                    UserName = registerUserDto.name

                };
                IdentityResult result = await UserManager.CreateAsync(user, registerUserDto.password);
                if (result.Succeeded)
                {
                    return Ok("account added succesfully !");
                }
                string errorStr = "";
                foreach (var err in result.Errors) {
                    errorStr += err;
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);

        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {

            if (ModelState.IsValid)
            {
               var user= await UserManager.FindByNameAsync(loginUserDto.UserName);
                if (user != null)
                {
                   bool isFound=  await UserManager.CheckPasswordAsync(user , loginUserDto.Password);
                    if (isFound)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name , loginUserDto.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier,  (user.Id).ToString()));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await UserManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));

                        }
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]));
                        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
 

                        JwtSecurityToken Token = new JwtSecurityToken(
                            issuer: Configuration["JWT:Issuer"],
                            audience: Configuration["JWT:Audiance"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCredentials
 
                            );
                        return Ok( new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(Token) ,
                            expiration= Token.ValidTo
                        });
                    }
                }

            }
            return Unauthorized();
        }
    }
}
