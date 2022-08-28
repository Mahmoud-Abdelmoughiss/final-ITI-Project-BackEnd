using EcommerseApplication.Models;
using EcommerseApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerseApplication.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _useManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Context _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration, Context context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _useManager = userManager;
            _roleManager = roleManager;
            _appDbContext = context;
            _configuration = configuration;

        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = await _useManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return Ok(new Response { Status = "Error", Message = "User already exists!" });
            }
            var newuser = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
            };
            var result = await _useManager.CreateAsync(newuser, model.Password);
            if (!result.Succeeded)
                return Ok(new Response { Status = "Error", Message = "we Can not create With current Data" });

            await _useManager.AddToRoleAsync(newuser, "User");
            await _appDbContext.users.AddAsync(new User { 
                IdentityId = newuser.Id, UserName = model.Username, birthDate = model.birthDate,
                FirstName = model.FirstName, LastName = model.LastName, Phone = model.Phone
            });
            await _appDbContext.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _useManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            return Ok(new Response { Status = "Error", Message = "User already exists!" });
            var newuser = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
            };
            var result = await _useManager.CreateAsync(newuser, model.Password);
            if (!result.Succeeded)
                return Ok(new Response { Status = "Error", Message = "we Can not create With current Data" });

            await _useManager.AddToRoleAsync(newuser, "User");
            await _useManager.AddToRoleAsync(newuser, "Admin");//tarek
            await _appDbContext.users.AddAsync(new User
            {
                IdentityId = newuser.Id,
                UserName = model.Username,
                birthDate = model.birthDate,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone
            });
            await _appDbContext.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });

        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(14),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _useManager.FindByEmailAsync(model.Email);
            if (user != null && await _useManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _useManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [HttpGet]
        [Route("GetLoginsData")]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var userName = User?.Identity?.Name;
            var userId = User?.FindFirstValue(ClaimTypes.Sid);
            List<Claim> roleClaims = User?.FindAll(ClaimTypes.Role).ToList();
            var roles = new List<string>();

            foreach (var role in roleClaims)
            {
                roles.Add(role.Value);
            }
            return Ok(new { roles,userName,userId });
        }
        

       

    }
}
