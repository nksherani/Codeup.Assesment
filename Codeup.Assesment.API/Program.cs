using Codeup.Assesment.Data;
using Codeup.Assesment.Data.Repository;
using Codeup.Assesment.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<OrdersDbContext>(options =>
       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScoped<OrderManagementService>();


var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    // added this just to make project portable, on production system migrations should be used instead.
    dataContext.Database.EnsureCreated();
}

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
