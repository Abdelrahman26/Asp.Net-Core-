using Hospital.BL;
using Hospital.DAL;
using Hospital.DAL.Repositories.Patients;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Service
#region Default
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Repos
builder.Services.AddScoped<IDoctorsRepo, DoctorRepo>();
builder.Services.AddScoped<IPatientsRepo, PatientsRepo>();
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
#endregion

#region Managers
builder.Services.AddScoped<IDoctorManager, DoctorManager>();
#endregion

#region Database
var connectionString = builder.Configuration.GetConnectionString("HospitalDb");
builder.Services.AddDbContext<HospitalContext>(options => options.UseSqlServer(connectionString));
#endregion

#endregion

var app = builder.Build();

#region middleWares
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion