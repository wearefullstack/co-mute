using Microsoft.EntityFrameworkCore;
using co_mute_be.Database;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.WithOrigins("http://localhost:4200", "https://localhost:4200");
                      });
});


// Add services to the container.

builder.Services.AddControllers();

//Register db instances
RegisterDb(builder.Services);
//builder.Services.AddDbContext<CarPoolOppContext>(opt =>
//{
//    opt.UseInMemoryDatabase("CarPoolOpps");
//});

//builder.Services.AddSingleton

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();



app.Run();

static void RegisterDb(IServiceCollection services)
{
    services.AddDbContext<CarPoolOppContext>(opt =>
    {
        opt.UseInMemoryDatabase("CarPoolOpps");
    });

    services.AddDbContext<UserContext>(opt =>
    {
        opt.UseInMemoryDatabase("Users");
    });
}
