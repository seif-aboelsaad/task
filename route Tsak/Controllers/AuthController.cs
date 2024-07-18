using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;
using route_Tsak.Models;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace route_Tsak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(NewUserDto newuser)
        {
            var user = new ApplicationUser()
            {
                UserName = newuser.Name,
                Email = newuser.Email,
                Address = newuser.Address,
            };
            var result = await _userManager.CreateAsync(user,newuser.Password);
            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            await _userManager.AddToRoleAsync(user, "Customer");

            return Ok(new { Message = "User Registered Successfully" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginmodel)
        {
            var user = await _userManager.FindByEmailAsync(loginmodel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginmodel.Password))
            {
                var authenticationClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Role, "Customer"),           
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                //Add role to claims
                foreach (var role in userRoles)
                {
                    authenticationClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                //Generate the token with claims 
                var jwtToken = GetToken(authenticationClaims);

                //return the token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),

                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authenticationClaims)
        {
            var authenticationSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("welcome to my account HOMIE TOMY CJ ALEX"));

            var Token = new JwtSecurityToken(

                    expires: DateTime.Now.AddDays(2).ToLocalTime(),
                    claims: authenticationClaims,
                    signingCredentials: new SigningCredentials(authenticationSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return Token;
        }
    }
}
