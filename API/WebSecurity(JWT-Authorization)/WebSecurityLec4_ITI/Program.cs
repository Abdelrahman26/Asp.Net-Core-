using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WebSecurityLec4_ITI.Data.Context;
using WebSecurityLec4_ITI.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region service
#region default Services
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Database
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDb")));
#endregion

#region Asp Identity
// Password Confiuration
// AddIdentity<TypeOfUser, TypeOfRole>
builder.Services.AddIdentity<Employee, IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;

        options.User.RequireUniqueEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    }
).AddEntityFrameworkStores<EmployeeContext>(); // Add Used DB Context or contexts
#endregion
#region Authentication 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "AuthenticationSchem1";
    // ChallengeScheme -> scheme that used if user not authenticated
    options.DefaultChallengeScheme = "AuthenticationSchem1";
    //options.DefaultSignInScheme = "AuthenticationSchem1" ---- used in MVC;
})
    .AddJwtBearer("AuthenticationSchem1", options =>
    {
        var keyFromConfig = builder.Configuration.GetValue<string>("SecretKey");
        var keyInBytes = Encoding.ASCII.GetBytes(keyFromConfig);
        var secretKey = new SymmetricSecurityKey(keyInBytes);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = secretKey,
            // To Avoid corrupt the token, assign both properties to false
            ValidateIssuer = false,
            ValidateAudience = false
        };
    })
    .AddJwtBearer("AuthenticationSchem2", options => { })
    .AddJwtBearer("AuthenticationSchem3", options => { })
    .AddJwtBearer("AuthenticationSchem4", options => { });
#endregion

#region Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagersOnly",
        policy => policy
        .RequireClaim(ClaimTypes.Role, "Manager", "CEO")
        .RequireClaim(ClaimTypes.NameIdentifier)); // just user need to have Nameidentifer with any value.
});
#endregion
#endregion

var app = builder.Build();

# region Middlewares
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion