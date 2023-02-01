using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebSecurityLec4_ITI.Data.Models;

namespace WebSecurityLec4_ITI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    private readonly UserManager<Employee> _userManager;
    public DataController(UserManager<Employee> userManager)
    {
        _userManager = userManager;
    }
    [HttpGet]
    [Authorize]
    [Route("Secured-data")]
    public ActionResult GetSecuredData()
    {
        return Ok(new 
        { 
            Name = "Abdelrahman Abdelmoneam" 
        });
    }

    [HttpGet]
    [Authorize(Policy ="ManagersOnly")]
    [Route("Secured-management-data")]
    public ActionResult GetDataFromManager()
    {
        return Ok(new
        {
            Name = "Secured management data"
        });
    }

    [HttpGet]
    [Authorize]
    [Route("user-details")]
    public async Task <ActionResult> GetLoggedInUserDetails()
    {
        // User is c# object of token.

        //var userIdClaim = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
        //var userId = userIdClaim.Value;
        //var employee = await _userManager.FindByIdAsync(userId);

        // This Methode to work, (id) has to be inside (nameIdentifier), otherwise return null
        var employee = await _userManager.GetUserAsync(User);
        return Ok(new       
        {
            Id = employee.Id,
            Department = employee.Department
        });
    }
}
