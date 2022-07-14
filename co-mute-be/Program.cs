using Microsoft.EntityFrameworkCore;
using co_mute_be.Database;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

SetupCors(builder.Services,MyAllowSpecificOrigins);

// Add services to the container.
builder.Services.AddControllers();

//Register db instances
RegisterDb(builder.Services);

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

#region Setup
static void RegisterDb(IServiceCollection services)
{
    services.AddDbContext<DataContext>(opt =>
    {
        opt.UseInMemoryDatabase("CoMute");
    });
}

static void SetupCors(IServiceCollection services, string policyName)
{
    services.AddCors(options =>
    {
        options.AddPolicy(name: policyName,
                          policy =>
                          {
                              policy.AllowAnyHeader();
                              policy.AllowAnyMethod();
                              policy.WithOrigins("http://localhost:4200", "https://localhost:4200");
                          });
    });
}
#endregion


