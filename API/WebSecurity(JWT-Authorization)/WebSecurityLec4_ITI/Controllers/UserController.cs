using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using WebSecurityLec4_ITI.Data.Models;
using WebSecurityLec4_ITI.DTOs;

namespace WebSecurityLec4_ITI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // inject service from appsetting.json
        private readonly IConfiguration _configuration1;
        private readonly UserManager<Employee> _userManager;
        public UserController(IConfiguration configuration, UserManager<Employee> userManager)
        {
            _configuration1 = configuration;
            _userManager = userManager;
        }

        #region Static Login
        [HttpPost]
        [Route("static-login")]
        public ActionResult<TokenDTO> StaticLogin(LoginCredentials input)
        {

            if (input.Username != "Admin" && input.Password != "password")
                return Unauthorized();

            // Generate User Claims
            var userClaim = new List<Claim>
            {
                //new Claim(ClaimTypes.NameIdentifier, input.Username),
                new Claim("Name", "Abdelrahman"),
                new Claim("Date", DateTime.Now.ToString())
            };

            // Getting Secret Key
            var keyFromConfig = _configuration1.GetValue<string>("SecretKey");
            var keyInBytes = Encoding.ASCII.GetBytes(keyFromConfig);
            var secretKey = new SymmetricSecurityKey(keyInBytes);


            // Choosing Hashing Algorithm then add it with scretkey 
            var signingCredentials = new SigningCredentials(secretKey,
                SecurityAlgorithms.HmacSha256Signature);

            // Create Token 
            var jwt = new JwtSecurityToken(
                claims: userClaim,
                expires: DateTime.Now.AddMinutes(15),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            // Convert JWT Object to String
            var tokenHandler = new JwtSecurityTokenHandler();
            return new TokenDTO
            {
                Token = tokenHandler.WriteToken(jwt)
            };
            
        }
        #endregion

        #region Register
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterCredentials input)
        {
            var employee = new Employee
            {
                UserName = input.Username,
                Department = input.Department,
                Email = input.Email
            };
            // To Hash Password and Save it to database using AspNetCore.identity package
            // and validate user credentials internally also using same package
            // based on default options and overriding objects inside program.cs 
            var result = await _userManager.CreateAsync(employee, input.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var claims = new List<Claim>
            {
                 //Recommended for user manager to work properly
                new Claim(ClaimTypes.NameIdentifier, employee.Id),
                new Claim(ClaimTypes.Role, "Manager"),    
            };

            await _userManager.AddClaimsAsync(employee, claims);
            return NoContent();
        }
        #endregion

        #region Login 
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginCredentials input)
        {
            var employee = await _userManager.FindByNameAsync(input.Username);
            
            // Check Invalid Username
            if(employee is null)
            {
                return BadRequest(new { message = "User Not Found" });
            }

            // Check if user is locked
            var is_locked = await _userManager.IsLockedOutAsync(employee);
            if(is_locked)
            {
                return BadRequest(new { message = "You Are Looked Out" });
            }

            bool passwordCheckResult = await _userManager.CheckPasswordAsync(employee, input.Password);
            if (!passwordCheckResult)
            {
                await _userManager.AccessFailedAsync(employee);// Increase number of faild attempts, to lock out
                return Unauthorized();
                // in program.cs : options.Lockout.MaxFailedAccessAttempts = 3;
            }
            // retrive User Claims from DataBase
            var userClaim = await _userManager.GetClaimsAsync(employee);

            // Getting Secret Key
            var keyFromConfig = _configuration1.GetValue<string>("SecretKey");
            var keyInBytes = Encoding.ASCII.GetBytes(keyFromConfig);
            var secretKey = new SymmetricSecurityKey(keyInBytes);


            // Choosing Hashing Algorithm then add it with scretkey 
            var signingCredentials = new SigningCredentials(secretKey,
                SecurityAlgorithms.HmacSha256Signature);

            // Create Token 
            var jwt = new JwtSecurityToken(
                claims: userClaim,
                expires: DateTime.Now.AddMinutes(15),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            // Convert JWT Object to String
            var tokenHandler = new JwtSecurityTokenHandler();
            return new TokenDTO
            {
                Token = tokenHandler.WriteToken(jwt)
            };
        }
        #endregion
    }
}
